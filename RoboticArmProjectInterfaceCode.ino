#define TOTAL_MOTOR_COUNT 1

#define INITIAL_STEP_DELAY 5000

enum AccelerationModes{
  Linear = 0,
  NonLinear
  };
  
enum AccelerationStates{
  STOP = 0,
  ACCEL,
  RUN,
  DECEL
  };

class Motor{
  public:
  long motorNumber;
  long enablePin;
  long optoPin;
  long directionPin;
  long pulsePin;
  int currentPosition;
  int targetPosition;
  float currentStepDelay;
  float targetStepDelay;
  AccelerationModes accelerationMode;
  long lastStepTimeMicroSeconds;
  bool stepPinHigh; 
  long directionInt; 
  long halfwayToTarget;
  bool rampUp;
  long stepsPerRevolution;
  long stepsInRampUp;
  AccelerationStates accelerationState;

  public: Motor(int mN, int eP, int oP, int dP, int pP, int sPR){
    motorNumber = mN; // The number of motor relative to distance from shoulder joint
    enablePin = eP; // The enable pin for the motor driver
    optoPin = oP; // The opto pin for the motor driver
    directionPin = dP; // The direction pin for the motor driver
    pulsePin = pP; // The pulse pin for the motor driver
    currentPosition = 0; // The current position relative to the predefined rest position
    targetPosition = 0; // The target position relative to the predefined rest position.
    currentStepDelay = 10000; // The current step delay of the motor between half pulses.
    targetStepDelay = 100; // The target step delay of the motor between half pulses.
    accelerationMode = Linear; // The mode of acceleration with Linear being the default.
    lastStepTimeMicroSeconds = 0; // The last time that a step was completed.
    halfwayToTarget = 0;
    stepsInRampUp = 0;    
    stepPinHigh = false;
    directionInt = 1;
    rampUp = true;
    stepsPerRevolution = sPR;
    accelerationState = STOP;
    pinMode(enablePin, OUTPUT);
    pinMode(optoPin, OUTPUT);
    pinMode(directionPin, OUTPUT);
    pinMode(pulsePin, OUTPUT);

    digitalWrite(enablePin, HIGH);
    digitalWrite(optoPin, HIGH);
    digitalWrite(directionPin, HIGH);
    digitalWrite(pulsePin, LOW);
  }

  void UpdateMotorPosition(){

    if(currentPosition == targetPosition){
      accelerationState = STOP;
      return;  
    }

    // TODO - Ramping down doesnt seem to work. Make a flow chart of the important decisions. Checking using excel spreadsheet.
      if((micros() - lastStepTimeMicroSeconds) >= (long)currentStepDelay){
        lastStepTimeMicroSeconds = micros();
        if(stepPinHigh){

            switch(accelerationState){
              case ACCEL:
                stepsInRampUp++;
                currentStepDelay = (float)currentStepDelay - 2.0 * (float)currentStepDelay / (4.0 * stepsInRampUp + 1.0);
             
                if(currentStepDelay <= targetStepDelay){
                  currentStepDelay = targetStepDelay;
                  if(abs(currentPosition - targetPosition) < halfwayToTarget){
                      accelerationState = DECEL;
                    }else{
                      accelerationState = RUN;
                      }
                  }
                  
                  if(abs(currentPosition - targetPosition) < halfwayToTarget){
                      accelerationState = DECEL;
                    }
              break;
              case RUN:
              if(abs(currentPosition - targetPosition) < stepsInRampUp){
                  accelerationState = DECEL;
                }
              break;
              case DECEL:
              stepsInRampUp--;
              currentStepDelay = (float)currentStepDelay/(1.0 - 2.0/(4.0 * stepsInRampUp + 1.0));
              break;
              case STOP:
              
              break;
              }
          
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

enum States{
  Calibration = 0,
  WaitingForCommand,
  InterpretingNewCommand,
  CheckingSoftLimits,
  MovingToPosition,
  ErrorEncountered
  };

States currentState = 0;
String currentCommand = "";
/*REMOVE COMMENTS Motor allMotors[6] = {
    Motor(),
    Motor(),
    Motor(),
    Motor(),
    Motor(),
    Motor(),
  };
  */

// StepperMotor0Definitions
#define M0mN 0
#define M0eP 7
#define M0oP 6
#define M0dP 5
#define M0pP 4
#define M0sPR 400

Motor allMotors[1] = {
  Motor(M0mN, M0eP, M0oP, M0dP, M0pP, M0sPR)
  };

void DecodeCommandString(){

  int currentMotorNumber = 0;
  for(int i = 0; i < currentCommand.length(); i++){
    
      if(currentCommand[i] == 'M'){
        currentMotorNumber = (int)currentCommand[i+1] - 48;
        
        Serial.print("M: ");
        Serial.println(currentMotorNumber);
        
      }else if(currentCommand[i] == 'T' && currentCommand[i+1] == 'P'){
        for(int j = i + 2; j < currentCommand.length(); j++){
          if(currentCommand[j] > 58){
              allMotors[currentMotorNumber].targetPosition = (long)(currentCommand.substring(i+2, j).toFloat()*allMotors[currentMotorNumber].stepsPerRevolution/360.0);
            break;  
          }  
        }

        Serial.print("TP: ");
        Serial.println(allMotors[currentMotorNumber].targetPosition);
        
      }else if(currentCommand[i] == 'T' && currentCommand[i+1] == 'S'){
        for(int j = i + 2; j < currentCommand.length(); j++){
          if(currentCommand[j] > 58){
              allMotors[currentMotorNumber].targetStepDelay = (long)(currentCommand.substring(i+2, j).toInt());
            break;  
          }  
        }
        allMotors[currentMotorNumber].currentStepDelay = 5000;
        
        Serial.print("TS: ");
        Serial.println(allMotors[currentMotorNumber].targetStepDelay);
        
      }else if(currentCommand[i] == 'A'){
        allMotors[currentMotorNumber].accelerationMode = (int)currentCommand[i+1] - 48;
        Serial.print("A: ");
        Serial.println(allMotors[currentMotorNumber].accelerationMode);
      }
      
  }
  
}

bool AllMotorsAtTarget(){
  for(int i = 0; i < TOTAL_MOTOR_COUNT; i++){
    if(allMotors[i].currentPosition != allMotors[i].targetPosition){
      return false;  
    }  
  }
  return true;
}

void SetDirection(){
    for(int i = 0; i < TOTAL_MOTOR_COUNT; i++){
        if(allMotors[i].targetPosition > allMotors[i].currentPosition){
          digitalWrite(allMotors[i].directionPin, HIGH);
          allMotors[i].directionInt = 1;
          }
          else{
            digitalWrite(allMotors[i].directionPin, LOW);
            allMotors[i].directionInt = -1;
          }
      }
  }

  void CalculateRamps(){
      for(int i = 0; i < TOTAL_MOTOR_COUNT; i++){
        allMotors[i].halfwayToTarget = abs(allMotors[i].currentPosition - allMotors[i].targetPosition)/2;
        allMotors[i].currentStepDelay = INITIAL_STEP_DELAY;
        allMotors[i].stepsInRampUp = 0;
        allMotors[i].accelerationState = ACCEL;
      }
  }


void setup() {
  Serial.begin(115200);
  // put your setup code here, to run once:

}


void loop() {

  switch(currentState){

    case Calibration:
    currentState = WaitingForCommand;
    break;
    
    case WaitingForCommand:

      //Serial.println("STATE: WaitingForCommand");
  
      if(Serial.available() > 0){
        currentCommand = Serial.readString();
        currentState = InterpretingNewCommand;
      }
      
      break;
    
    case InterpretingNewCommand:
      //Serial.println("STATE: InterpretingNewCommand");

      DecodeCommandString();
      SetDirection();
      CalculateRamps();
    
      currentState = CheckingSoftLimits;
    break;
    case CheckingSoftLimits:
      //Serial.println("STATE: CheckingSoftLimits");
    currentState = ErrorEncountered
    ;
    currentState = MovingToPosition;
    break;
    case MovingToPosition:

      for(int i = 0; i < TOTAL_MOTOR_COUNT; i++){

        allMotors[i].UpdateMotorPosition();
          
      }
      //Serial.println("STATE: MovingToPosition");

      if(AllMotorsAtTarget()){
        currentState = WaitingForCommand;
      }
    break;
    case ErrorEncountered:
      //Serial.println("STATE: ErrorEncountered");
    currentState = WaitingForCommand;
    break;
    
    
  }

}
