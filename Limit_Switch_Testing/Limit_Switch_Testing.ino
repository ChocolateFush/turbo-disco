#include <Servo.h>

Servo myServo1;

void setup() {
  // put your setup code here, to run once:

  Serial.begin(115200);
  myServo1.attach(9);
  

}

void loop() {
  // put your main code here, to run repeatedly:
  
  myServo1.write(90);

}
