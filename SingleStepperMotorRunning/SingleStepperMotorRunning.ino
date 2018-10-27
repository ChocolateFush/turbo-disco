// Code for running a stepper
int enaPin = 7;
int optoPin = 6;
int dirPin = 5;
int pulPin = 4;

void setup() {

  pinMode(enaPin, OUTPUT);
  pinMode(optoPin, OUTPUT);
  pinMode(dirPin, OUTPUT);
  pinMode(pulPin, OUTPUT);

  digitalWrite(enaPin, HIGH);
  digitalWrite(optoPin, HIGH);
  digitalWrite(dirPin, HIGH);

}

void loop() {

  digitalWrite(pulPin, HIGH);
  delayMicroseconds(1500);
  digitalWrite(pulPin, LOW);
  delayMicroseconds(1500);
  
  
}
