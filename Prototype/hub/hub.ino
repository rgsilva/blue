#include <RH_ASK.h>
#include <SPI.h>

#include <ESP8266WiFi.h>
#include <EEPROM.h>

#define WMHUB_VERSION "WMHUB 0.1/PROTOTYPE"
#define MAX_WIFI_WAITING_COUNTER 30

#define SERVER_HOST "water.rgsilva.com"
#define SERVER_PORT 80

#define EEPROM_WIFI_NAME_START 4
#define EEPROM_WIFI_NAME_LENGTH 32

#define EEPROM_WIFI_PASS_START (EEPROM_WIFI_NAME_START + EEPROM_WIFI_NAME_LENGTH)
#define EEPROM_WIFI_PASS_LENGTH 32

typedef struct {
  byte seq;
  float value;
} DataPacket;

byte lastSeq = 0;

// Radio driver.
// RH_ASK(speed, rxPin, txPin, pttPin, pttInverted)
RH_ASK driver = RH_ASK(4000, D2, D1, D1, false);

// Wireless configuration.
char wifiName[EEPROM_WIFI_NAME_LENGTH] = {0};
char wifiPass[EEPROM_WIFI_PASS_LENGTH] = {0};

void setup() {
  // Serial port.
  Serial.begin(115200);

  // RF.
  if (!driver.init()) {
    Serial.println("init failed");
  }

  // EEPROM.
  EEPROM.begin(512);
  if (!isEepromReady()) {
    Serial.println("Warning: EEPROM isn't ready, writing zeros...");
    clearEeprom();
  } else {
    readConfig();
  }

  // Wifi.
  WiFi.mode(WIFI_STA);
  WiFi.setAutoConnect(false);

  // If the wifi is configured, try to connect to it already.
  if (wifiName[0] != 0) {
    wifiConnect();
  }
}

void loop() {
  // First we deal with radio stuff.
  DataPacket packet;
  uint8_t recvSize = sizeof(packet);

  if (driver.recv((uint8_t*)&packet, &recvSize)) // Non-blocking
  {
    if (packet.seq != lastSeq) {
      lastSeq = packet.seq;

      Serial.print("! size=");
      Serial.print(String((int)sizeof(packet)));
      Serial.print(", seq=");
      Serial.print(String(packet.seq));
      Serial.print(", value=");
      Serial.println(String(packet.value));
    }

    // If connected, send the data to the server.
    if (WiFi.status() == WL_CONNECTED) {
      wifiSendData(String(packet.value));
    }
  }

  // Then we deal with serial stuff.
  if (Serial.available() > 0) {
    String command = Serial.readString();

    switch (command[0]) {
      // DEBUG: enable serial debug mode.
      case '1':
        Serial.setDebugOutput(true);
        Serial.println("~ OK");
        break;

      // DEBUG: print wifi status.
      case '2':
        Serial.print("~ ");
        Serial.print(WiFi.status() == WL_CONNECTED ? "connected " : "disconnected ");
        Serial.println(WiFi.localIP());
        break;

      // DEBUG: clear EEPROM.
      case '3':
        clearEeprom();
        Serial.println("~ OK");
        break;

      // DEBUG: send test data.
      case '4':
        if (wifiSendData("987.789")) {
          Serial.println("~ OK");
        } else {
          Serial.println("~ ERR");
        }
        break;        

      // Get info.
      case '?':
        Serial.print("~ ");
        Serial.println(WMHUB_VERSION);
        break;

      // Read config.
      case 'R':
        cmdReadConfig();
        break;

      // Write config.
      case 'W':
        cmdWriteConfig();
        break;

      // Scan networks.
      case 'S':
        cmdWifiScan();
        break;

      // Test connection to a to network
      case 'T':
        command.remove(0, 2);
        cmdTestWifiNetwork(command);
        break;

      // Unknown command.
      default:
        Serial.println("~ unknown command");
    }
  }
}

// ----------------------------------------------------------------------------------------------------------

void cmdReadConfig() {
  if (!isEepromReady()) {
    clearEeprom();
  } else {
    readConfig();
  }

  Serial.print("~ ");
  Serial.print(wifiName);
  Serial.print(';');
  Serial.println(wifiPass);
}

void cmdWriteConfig() {
  writeConfig();
  Serial.println("~ OK");
}

void cmdWifiScan() {
  // Switch to station mode and disconnect.
  if (WiFi.isConnected()) {
    WiFi.disconnect();
  }
  delay(100);

  // Scan for networks.
  unsigned int count = WiFi.scanNetworks();

  // Write the networks.
  Serial.print("~ ");
  for (unsigned int i = 0; i < count; i++) {
    Serial.print(WiFi.SSID(i));
    Serial.print(';');
  }
  Serial.println("");
}

void cmdTestWifiNetwork(String wifiStr) {
  unsigned int splitter = wifiStr.indexOf(';');
  String name = wifiStr.substring(0, splitter);
  String password = wifiStr.substring(splitter + 1);

  // Copy to global char strings the values (bugs, bugs everywhere).
  strcpy(wifiName, name.c_str());
  strcpy(wifiPass, password.c_str());

  if (wifiConnect()) {
    Serial.print("~ OK ");
    Serial.println(WiFi.localIP());
  } else {
    Serial.println("~ ERR");
  }
}

// ----------------------------------------------------------------------------------------------------------
// EEPROM tools

bool isEepromReady() {
  return (
    EEPROM.read(0) == (byte)'W' &&
    EEPROM.read(1) == (byte)'M' &&
    EEPROM.read(2) == (byte)'J' &&
    EEPROM.read(3) == (byte)'U'
  );
}

void clearEeprom() {
  EEPROM.write(0, 'W'); EEPROM.write(1, 'M');
  EEPROM.write(2, 'J'); EEPROM.write(3, 'U');

  for (unsigned int i = 4; i < 512; i++) {
    EEPROM.write(i, 0);
  }

  EEPROM.commit();
}

void readConfig() {  
  // Read the network name.
  for (unsigned int i = 0; i < EEPROM_WIFI_NAME_LENGTH; i++) {
    wifiName[i] = EEPROM.read(EEPROM_WIFI_NAME_START + i);
  }

  // Read the network password.
  for (unsigned int i = 0; i < EEPROM_WIFI_PASS_LENGTH; i++) {
    wifiPass[i] = EEPROM.read(EEPROM_WIFI_PASS_START + i);
  }
}

void writeConfig() {
  // Write the network name.
  for (unsigned int i = 0; i < EEPROM_WIFI_NAME_LENGTH; i++) {
    EEPROM.write(EEPROM_WIFI_NAME_START + i, wifiName[i]);
  }

  // Write the network password.
  for (unsigned int i = 0; i < EEPROM_WIFI_PASS_LENGTH; i++) {
    EEPROM.write(EEPROM_WIFI_PASS_START + i, wifiPass[i]);
  }

  EEPROM.commit();
}

// ----------------------------------------------------------------------------------------------------------
// Wifi tools

bool wifiConnect() {
  // Disconnect if connected.
  if (WiFi.status() == WL_CONNECTED) {
    WiFi.disconnect();
  }

  // Try to connect.  
  WiFi.begin(wifiName, wifiPass);

  unsigned int counter = 0;
  while ((WiFi.status() != WL_CONNECTED) && (counter < MAX_WIFI_WAITING_COUNTER)) {
    counter++;
    delay(1000);
  }

  return (WiFi.status() == WL_CONNECTED);
}

bool wifiSendData(String value) {
  WiFiClient client;

  if (client.connect(SERVER_HOST, SERVER_PORT)) {
    // Build the request.
    String request = 
      "GET /?value=" + value + " HTTP/1.1\r\n" +
      "Host: " + String(SERVER_HOST) + "\r\n" +
      "Connection: close\r\n" +
      "\r\n";

    // Send the request
    client.print(request);

    while (client.available()) {
      String line = client.readStringUntil('\n');

      if (line.startsWith("OK ")) {
        return true;
      } else if (line.startsWith("ERR ")) {
        return false;
      }
    }
  }

  return false;
}

