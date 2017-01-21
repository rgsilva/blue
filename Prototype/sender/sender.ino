#include <RH_ASK.h>
#include <SPI.h>
#include <TimeLib.h>

// Program configuration
#define LOOP_DELAY 5000
#define MAX_WATER_RUNNING_TIME 30

// Radio configuration
#define PACKET_REPEAT 5
#define PACKET_REPEAT_DELAY 100

// Beeper configuration
#define BEEP_SHORT 50
#define BEEP_LONG 500
#define BEEP_LONG_REPEAT 5

// Pins
#define SENSOR_PIN 2
#define BEEPER_PIN 3

// -----------------------------------------------------------------------------

// Macros
#define totalSeconds() (hour()*3600 + minute()*60 + second())
#define resetTime() setTime(00,00,00,01,01,2017)

// Radio driver
// RH_ASK(speed = 2000, rxPin = 11, txPin = 12, pttPin = 10, pttInverted = false)
RH_ASK driver = RH_ASK(1000, 11, 4, 10, false);

// Data packet structure
typedef struct {
  byte seq;
  char unused[3]; // Compliance with ESP's compiler.
  float value;
} DataPacket;

// Next sequence counter
byte nextSeq = 1;

void setup() {
  // Initialize the serial port
  Serial.begin(115200);

  // Initialize the sensor pin and attach an interrupt to it.
  pinMode(SENSOR_PIN, INPUT_PULLUP);
  attachInterrupt(digitalPinToInterrupt(SENSOR_PIN), dataReceived, RISING);

  // Initialize the beeper pin.
  pinMode(BEEPER_PIN, OUTPUT);

  // Initialize the radio driver.
  if (!driver.init()) {
    Serial.println("Radio driver initialization failed!");
    while (true) { beep(BEEP_SHORT); }
  }

  // Reset the time.
  resetTime();

  // Beep twice to indice successful boot.
  beep(BEEP_SHORT);
  beep(BEEP_SHORT);
}

// Sensor counter and last counter.
unsigned int counter = 0;
unsigned int lastCounter = 0;

// Sensor interrupt callback.
void dataReceived() {
  counter++;
}

void loop() {
  if (counter > 0) {
    // If the last counter was zero, reset the time: we begin counting the time here.
    if (lastCounter == 0) {
      resetTime();
    }

    // Copy the counter and releases the main variable.
    lastCounter = counter;
    counter = 0;

    // Build the data packet.
    DataPacket packet;
    packet.seq = nextSeq++;
    packet.value = (lastCounter/450.0);

    // Send the data packet multiple times.
    for (int i = 0; i < PACKET_REPEAT; i++) {
      driver.send((uint8_t*)&packet, sizeof(packet));
      driver.waitPacketSent();
      delay(PACKET_REPEAT_DELAY);
    }

    // Debug information.
    Serial.print("Sent: seq=");
    Serial.print(String(packet.seq));
    Serial.print(", value=");
    Serial.println(String(packet.value));

    // Detected for water running for too long.
    if (totalSeconds() > MAX_WATER_RUNNING_TIME) {
      Serial.println("*** Max water running time triggered ***");

      // Audio notification.
      for (int b = 0; b < BEEP_LONG_REPEAT; b++) {
        beep(BEEP_LONG);
      }

      // Reset the time.
      resetTime();
    }

    // Wait a while before looping again.
    delay(LOOP_DELAY);
  } else {
    // If the counter was 0, let's reset the lastCounter as well.
    lastCounter = 0;
  }
}

void beep(unsigned short delayms){
  analogWrite(BEEPER_PIN, 20); // 20?
  delay(delayms);
  analogWrite(BEEPER_PIN, 0);
  delay(delayms);
} 
