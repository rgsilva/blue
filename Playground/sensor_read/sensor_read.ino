int DATA_PIN = 2;

unsigned int counter = 0;

void setup() {
  Serial.begin(115200);

  pinMode(DATA_PIN, INPUT_PULLUP);
  attachInterrupt(digitalPinToInterrupt(DATA_PIN), dataReceived, RISING);
}

void dataReceived() {
  counter++;
}

void loop() {
  if (counter != 0) {
    Serial.print(counter/450.0); Serial.println(" L/h");
    counter = 0;
  }
  delay(1000);
}
