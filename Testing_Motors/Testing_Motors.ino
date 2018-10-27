void setup() {
  // put your setup code here, to run once:

  pinMode(2, OUTPUT); // Elbow
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT); // Shoulder
  pinMode(7, OUTPUT);
  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);

  digitalWrite(2, HIGH);
  digitalWrite(3, HIGH);
  digitalWrite(4, HIGH);
  digitalWrite(5, HIGH);
  digitalWrite(6, HIGH);
  digitalWrite(7, HIGH);
  digitalWrite(8, HIGH);
  digitalWrite(9, HIGH);

}

void loop() {
  // put your main code here, to run repeatedly:

  delayMicroseconds(700);

  digitalWrite(6, LOW);
  
  delayMicroseconds(700);

  digitalWrite(6, HIGH);
  

}
