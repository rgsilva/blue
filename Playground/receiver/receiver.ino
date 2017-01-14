#include <RH_ASK.h>
#include <SPI.h> // Not actualy used but needed to compile

RH_ASK driver = RH_ASK(2000, 11, 12, 10, false);

void setup()
{
  Serial.begin(115200);

  if (!driver.init()) {
    Serial.println("init failed");
  }
}
void loop()
{
  uint8_t buf[10];
  uint8_t buflen = sizeof(buf);

  uint8_t recvSize = buflen;
  if (driver.recv(buf, &recvSize)) // Non-blocking
  {
    Serial.print("Received: ");
    buf[recvSize] = 0;
    Serial.println((char*)buf);
  }
}

