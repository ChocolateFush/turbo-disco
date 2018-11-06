// Code for running a stepper
int dirPin = 7;
int pulPin = 6;

void setup() {

  pinMode(dirPin, OUTPUT);
  pinMode(pulPin, OUTPUT);

  digitalWrite(dirPin, LOW);

}

void loop() {

  digitalWrite(pulPin, HIGH);
  delayMicroseconds(10000);
  digitalWrite(pulPin, LOW);
  delayMicroseconds(10000);
  
  
}
