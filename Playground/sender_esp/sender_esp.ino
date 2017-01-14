#include <RH_ASK.h>
#include <SPI.h>

// RH_ASK(speed = 2000, rxPin = 11, txPin = 12, pttPin = 10, pttInverted = false)
RH_ASK driver = RH_ASK(2000, D1, D2, D0, false);

void setup() {
  Serial.begin(115200);

  if (!driver.init()) {
    Serial.println("init failed");
  }
}

unsigned int counter = 0;

void loop() {
  String str = String(counter++);
  char buffer[10] = {0};

  str.toCharArray(buffer, sizeof(buffer));
  driver.send((uint8_t*)buffer, strlen(buffer));
  driver.waitPacketSent();

  Serial.print("Sent: ");
  Serial.println(str);

  delay(500);
}
