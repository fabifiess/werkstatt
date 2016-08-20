int sensorPin = A0;   
int sensorValue = 0;  // variable to store the value coming from the sensor
int initVal = 0;
void setup() {
  Serial.begin(9600);
  initVal = analogRead(sensorPin);
  Serial.print("Init: ");
  Serial.println(initVal);
}

void loop() {
  // read the value from the sensor:
  //Serial.println(analogRead(sensorPin));
  sensorValue = analogRead(sensorPin) - initVal;

    Serial.println(sensorValue);


  delay(100);
}
