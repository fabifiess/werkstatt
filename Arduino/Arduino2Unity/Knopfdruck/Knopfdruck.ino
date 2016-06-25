int btn = 3;

void setup() {
  Serial.begin(9600);
  pinMode(btn, INPUT);
}

void loop() {
  int btnState = digitalRead(btn);
  if(btnState == HIGH){
    Serial.println("1");
  }else{
    Serial.println("0");
  }
  delay(50);
}
