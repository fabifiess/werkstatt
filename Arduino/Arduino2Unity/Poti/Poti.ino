int sensorPin = A0;  
int prevVal = 0; 

void setup() {
  Serial.begin(9600);
}

void loop() {
  int val = analogRead(sensorPin);
  if(val != prevVal && val != prevVal - 1 && val != prevVal + 1){
    Serial.println(val);
    delay(100);
    prevVal = val;
  } 
}
