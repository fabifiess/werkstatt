int btn = 3;
int prevVal = 0; 

void setup() {
  Serial.begin(9600);
  pinMode(btn, INPUT);
}

void loop() {
  int val = digitalRead(btn);
  if(val != prevVal){
    Serial.println(val);
    delay(100);
    prevVal = val;
  } 
  delay(100);
}
