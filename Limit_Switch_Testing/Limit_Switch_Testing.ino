void setup() {
  // put your setup code here, to run once:

  Serial.begin(115200);
  pinMode(11, INPUT); // Shoulder
  pinMode(12, INPUT); // Elbow
  

}

void loop() {
  // put your main code here, to run repeatedly:

  if(digitalRead(11) == HIGH){
    Serial.println("11 pushed");
  }
  if(digitalRead(12) == HIGH){
    Serial.println("12 pushed");
    
    }

}
