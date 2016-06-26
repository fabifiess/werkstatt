int sensorPin = A0;   
int sensorValue = 0;  // variable to store the value coming from the sensor
int initVal = 0;
void setup() {
  Serial.begin(9600);
  initVal = analogRead(sensorPin);
}

void loop() {
  // read the value from the sensor:
  //Serial.println(analogRead(sensorPin));
  sensorValue = analogRead(sensorPin) - initVal;
  if((sensorValue)>= 5){
    Serial.println(sensorValue);
  }
  if((sensorValue)>= 11){
    Serial.println("Trigger !!");
  }
}
