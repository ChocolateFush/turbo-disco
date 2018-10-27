/*
  Calibration Routine
*/

int debounceArray[10] = {0,0,0,0,0,0,0,0,0,0};

bool DebouncedHigh(int pinNumber){

bool returnValue = true;

for(int i = 0; i < 10; i++){
    if(debounceArray[i] == 0){
      returnValue = false;
      }
}

for(int i = 0; i < 9; i++){
    debounceArray[i] = debounceArray[i+1];
  }

  debounceArray[9] = 0;

  return returnValue;
  
}

enum CalibrationStates{
  ShoulderCalibration = 0,
  ElbowCalibration,
  Paused
  };

  CalibrationStates currentState = ShoulderCalibration;

  unsigned long prevMicro = 0;

void setup() {
  // put your setup code here, to run once:

  Serial.begin(115200);

  pinMode(11, INPUT); // Shoulder
  pinMode(12, INPUT); // Elbow
  
  pinMode(2, OUTPUT); // Elbow
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT); // Shoulder
  pinMode(7, OUTPUT);
  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);

  digitalWrite(2, HIGH);
  digitalWrite(3, LOW);
  digitalWrite(4, HIGH);
  digitalWrite(5, HIGH);
  digitalWrite(6, HIGH);
  digitalWrite(7, HIGH);
  digitalWrite(8, HIGH);
  digitalWrite(9, HIGH);

  prevMicro = micros();

}

void loop() {
  // put your main code here, to run repeatedly:

  switch(currentState){

case ShoulderCalibration:
Serial.println("Shoulder");

if(digitalRead(11) == HIGH){
    debounceArray[9] = 1;
  }

if(DebouncedHigh(11) == HIGH){
  currentState = ElbowCalibration;
for(int i = 0; i < 10; i++){
debounceArray[i] = 0;
}
  
  }else{
    if((micros() - prevMicro) >= 10000){
        prevMicro = micros();
        if(digitalRead(6) == HIGH){
          digitalWrite(6, LOW);
          }else{
            digitalWrite(6, HIGH);
            }
      }
    }

break;

case ElbowCalibration:
Serial.println("Elbow");

if(digitalRead(12) == HIGH){
    debounceArray[9] = 1;
  }

if(DebouncedHigh(12) == HIGH){
  currentState = Paused;
  }else{
    if((micros() - prevMicro) >= 10000){
        prevMicro = micros();
        if(digitalRead(2) == HIGH){
          digitalWrite(2, LOW);
          }else{
            digitalWrite(2, HIGH);
            }
      }
    }


break;
Serial.println("Paused");

case Paused:

break;
    
  }

}
