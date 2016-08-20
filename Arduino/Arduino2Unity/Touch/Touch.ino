
boolean toggle = false;
void setup() {
  Serial.begin(9600);
}

void loop() {

  if(toggle == false){
    toggle = true;
    Serial.println(1);
  }
  else{
    toggle = false;
    Serial.println(0);    
  }
  
  delay(4000);
}
