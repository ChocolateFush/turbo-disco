#include <Servo.h>

#define M1dP 7
#define M1pP 6
#define M1dC 1
#define M2dP 3
#define M2pP 2
#define M2dC -1
#define M3dP 5
#define M3pP 4
#define M3dC 1
#define M4sCP 10
#define M5sCP 9
#define M6sCP 11


enum States{
  WaitForNewCommands = 0,
  Home,
  CalculateNextMove,
  MoveToPose
  };

  enum MotorType{
    STEPPER = 0,
    SERVO
  };

class Motor{
  public:
  MotorType motorType;
  int directionPin; // Set in setup
  int pulsePin; // Set in setup
  int currentPosition = 0; //Set in Homing
  int targetPosition = 0; 
  float currentStepDelay = 10000;
  float minStepDelay = 10000; // Set in Homing
  bool stepPinHigh = false; 
  long directionInt = 1; 
  long stepsPerRevolution = 400; // Set in Homing
  long lastStepTimeMicroSeconds;
  int directionCorrector; // Set in setup
  int servoControlPin;
  Servo servoMotor;
  
  public: Motor(int dP, int pP, MotorType mt, int dC){
    directionPin = dP; // The direction pin for the motor driver
    pulsePin = pP; // The pulse pin for the motor driver
    motorType = mt;
    directionCorrector = dC;
    pinMode(directionPin, OUTPUT);
    pinMode(pulsePin, OUTPUT);
    digitalWrite(directionPin, HIGH);
    digitalWrite(pulsePin, LOW);
  }
  
  public: Motor(int mSCP, MotorType mt){
    servoControlPin = mSCP;
    motorType = mt;
    minStepDelay = 50;
  }

  void init(){
    servoMotor.attach(servoControlPin);  
  }

  void Home(){

      if(directionInt*directionCorrector == 1){
         digitalWrite(directionPin, HIGH);
      }else{
        digitalWrite(directionPin, LOW);  
      }

     if((micros() - lastStepTimeMicroSeconds) >= (long)currentStepDelay){
        
        lastStepTimeMicroSeconds = micros();
        
        if(stepPinHigh){
            digitalWrite(pulsePin, LOW);
            stepPinHigh = false;
            
        }else{
            digitalWrite(pulsePin, HIGH);
            stepPinHigh = true;  
        }
      }
  }

  void UpdateMotorPosition(){

    if(currentPosition == targetPosition){
      return;  
    }

     if((micros() - lastStepTimeMicroSeconds) >= (long)currentStepDelay){
        
        lastStepTimeMicroSeconds = micros();
        if(motorType == SERVO && stepPinHigh){
          currentPosition += directionInt;
          servoMotor.write(currentPosition);
          stepPinHigh = false;
          return;  
        }else if(motorType == SERVO && !stepPinHigh){
          stepPinHigh = true;  
          return;
        }
        if(stepPinHigh){
            currentPosition += directionInt;
            digitalWrite(pulsePin, LOW);
            stepPinHigh = false;
            
        }else{
            digitalWrite(pulsePin, HIGH);
            stepPinHigh = true;  
        }
      }
  }
  
  };

States currentState = 0;
String currentCommand = "";

Motor allMotors[6] = {
    Motor(M1dP, M1pP, STEPPER, M1dC),
    Motor(M2dP, M2pP, STEPPER, M2dC),
    Motor(M3dP, M3pP, STEPPER, M3dC),
    Motor(M4sCP, SERVO),
    Motor(M5sCP, SERVO),
    Motor(M6sCP, SERVO)
  };

int commandCounter = 0;

int allStoredCommands[25][7];

States InterpretCommandStrings(){

  commandCounter = 0;

  currentCommand = Serial.readString();
  
  States returnState = WaitForNewCommands;
  
  if(currentCommand[0] == 'H'){
    
      int startIndex = 0;
      int endIndex = 0;
      int delimiterCounter = 0;
      
      for(int i = 1; i < currentCommand.length(); i++){
        
       if(currentCommand[i] == '/'){
        startIndex = i+1;
        endIndex = startIndex;
          for(int j = i+1; j < currentCommand.length(); j++){
            
            if(currentCommand[j] == '/'){
                endIndex = j;
                break;
              }  
          }
          
            allStoredCommands[commandCounter][delimiterCounter] = currentCommand.substring(startIndex, endIndex).toInt();
            delimiterCounter++;

            if(delimiterCounter >= 7){
              currentCommand = currentCommand.substring(endIndex);
              break;
              }
          
          i = endIndex - 1;
            
        }
      }
      
      returnState = Home;

      
  }else if(currentCommand[0] == 'M'){
    
      int startIndex = 0;
      int endIndex = 0;
      int delimiterCounter = 0;
      
      delimiterCounter = 0;
      startIndex = 0;
      endIndex = 0;
      
      for(int i = 1; i < currentCommand.length(); i++){
        
       if(currentCommand[i] == '/'){
        startIndex = i+1;
        endIndex = startIndex;
          for(int j = i+1; j < currentCommand.length(); j++){
            
            if(currentCommand[j] == '/'){
                endIndex = j;
                break;
              }  
          }
          if(delimiterCounter <= 6){
            allStoredCommands[commandCounter][delimiterCounter] = currentCommand.substring(startIndex, endIndex).toInt();
            delimiterCounter++;
          }
          else if(delimiterCounter == 7){
            commandCounter++;
            delimiterCounter = 0;
            }
          i = endIndex - 1;
            if(commandCounter >= 25){
              currentCommand = currentCommand.substring(endIndex - 1);
              break;
            }
        }
      }
      returnState = CalculateNextMove;
  }

  return returnState;
    
}

bool AllMotorsAtTarget(){
  for(int i = 0; i < 6; i++){
    if(allMotors[i].currentPosition != allMotors[i].targetPosition){
      return false;  
    }  
  }
  return true;
}

void SetDirection(){
    for(int i = 0; i < 6; i++){
        if(allMotors[i].targetPosition > allMotors[i].currentPosition){
          allMotors[i].directionInt = 1;
          }
          else{
            allMotors[i].directionInt = -1;
          }

          if(allMotors[i].directionInt*allMotors[i].directionCorrector == 1){
            digitalWrite(allMotors[i].directionPin, HIGH);
          }else{
            digitalWrite(allMotors[i].directionPin, LOW);
          }
            
      }
  }

int debounceArray[2][10] = {{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}};

bool DebouncedHigh(int pinNumber){

if(digitalRead(pinNumber) == HIGH){
  debounceArray[pinNumber-12][9] = 1;
}

bool returnValue = true;

for(int i = 0; i < 10; i++){
    if(debounceArray[pinNumber-12][i] == 0){
      returnValue = false;
      }
}

for(int i = 0; i < 9; i++){
    debounceArray[pinNumber-12][i] = debounceArray[pinNumber-12][i+1];
  }

  debounceArray[pinNumber-12][9] = 0;

  return returnValue;
  
}

bool AllHomed(){
  bool bothHigh = true;

  allMotors[1].directionInt = 1;
  allMotors[2].directionInt = -1;
  
  if(!DebouncedHigh(12)){
    bothHigh = false;
    allMotors[2].Home();
  }  
  if(!DebouncedHigh(13)){
    bothHigh = false;
    allMotors[1].Home();
  }  
  if(bothHigh){
    for(int i = 0; i < 6; i++){
      allMotors[i].stepsPerRevolution = allStoredCommands[0][i];  
    }

    allMotors[0].minStepDelay = 500;
    allMotors[1].minStepDelay = 1000;
    allMotors[2].minStepDelay = 1000;

    Serial.println("Load more");
    currentCommand = "";
    return true;
  }
  return false;
}
  
void setup() {
  Serial.begin(115200);
  Serial.setTimeout(500);
  // put your setup code here, to run once:  

  allMotors[3].init();
  allMotors[4].init();
  allMotors[5].init();
}

int currentStoredCommand = 0;

void loop() {

      long maxTotalTime = 0;
      long currentTotalTime = 0;

  switch(currentState){
    case WaitForNewCommands:

      if(Serial.available() > 0 || (currentCommand.length() > 0)){
        currentState = InterpretCommandStrings();
      }
      
    break;
    
    case Home:

      allMotors[0].currentStepDelay = 10000;
      allMotors[1].currentStepDelay = 10000;
      allMotors[2].currentStepDelay = 10000;

      if(AllHomed()){
        currentState = WaitForNewCommands; 
        allMotors[0].currentPosition = 0; 
        allMotors[1].currentPosition = 130; 
        allMotors[2].currentPosition = -420; 
        for(int i = 3; i < 6; i++){
            allMotors[i].servoMotor.write(90);
            allMotors[i].currentPosition = 90;
          }
      }
         
    break;
    
    case CalculateNextMove:

      for(int i = 0; i < 6; i++){
        allMotors[i].targetPosition = allStoredCommands[currentStoredCommand][i];  
      }

      maxTotalTime = 0;

      for(int i = 0; i < 6; i++){
        currentTotalTime = allMotors[i].minStepDelay * abs(allMotors[i].currentPosition - allMotors[i].targetPosition);
        if(currentTotalTime > maxTotalTime){
          maxTotalTime = currentTotalTime;
        }
      }

      for(int i = 0; i < 6; i++){
         
        if(allMotors[i].targetPosition != allMotors[i].currentPosition){
          allMotors[i].currentStepDelay = (maxTotalTime* allStoredCommands[currentStoredCommand][6]) / (abs(allMotors[i].currentPosition - allMotors[i].targetPosition));
        }
      }

      SetDirection();
      
      currentState = MoveToPose;
      
    break;
    
    case MoveToPose:
    
      if(AllMotorsAtTarget()){

        if(currentStoredCommand == (commandCounter - 1)){
          Serial.println("Load more");  
        }
      
        currentStoredCommand++;

        if(currentStoredCommand >= commandCounter){
          currentState = WaitForNewCommands;
          currentStoredCommand = 0;
          commandCounter = 0;
        }else{
          currentState = CalculateNextMove;
        }
      }else{

         for(int i = 0; i < 6; i++){
            allMotors[i].UpdateMotorPosition();
          }
        
      }
    break;
    
  }

}
