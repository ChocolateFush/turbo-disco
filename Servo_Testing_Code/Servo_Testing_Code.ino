#include "Servo.h"

Servo myServo2;
Servo myServo3;
Servo myServo4;

void setup() {
  // put your setup code here, to run once:

  Serial.begin(115200);
  myServo2.attach(2);
  myServo3.attach(3);
  myServo4.attach(4);
  delay(100);
  
  myServo2.write(90);
  myServo3.write(90);
  myServo4.write(90);

}

void loop() {
  // put your main code here, to run repeatedly:

  if(Serial.available() > 0){

    String currentCommand = Serial.readString();
    int i = 0;
    int val2 = 90;
    int val3 = 90;
    int val4 = 90;
    int start = i;
    for(i; i < currentCommand.length(); i++){
      if(currentCommand[i] == '/'){
          val2 = currentCommand.substring(start, i).toInt();
          i++;
          break;
      }  
    }
    start = i;
    
    for(i; i < currentCommand.length(); i++){
      if(currentCommand[i] == '/'){
          val3 = currentCommand.substring(start, i).toInt();
          i++;
          break;
      }  
    }
    
    val4 = currentCommand.substring(i, currentCommand.length()).toInt();
    
    Serial.print(val2);
    Serial.print(" ");
    Serial.print(val3);
    Serial.print(" ");
    Serial.println(val4);

    myServo2.write(val2);
    myServo3.write(val3);
    myServo4.write(val4);
    
  }
  
}
