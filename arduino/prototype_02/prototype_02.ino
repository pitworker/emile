/*
 * Author: Sebastian Carpenter
 * Date: 4 March 2021
 * Note: Bluetooth keyboard code based on hidkeyboard example by Adafruit.
 */

#include <Arduino.h>

#include <Adafruit_BLE.h>
#include <Adafruit_BluefruitLE_UART.h>

#include <Organism.h>

// BLE PRESETS
#define BUFSIZE                      128
#define VERBOSE_MODE                true
#define FACTORYRESET_ENABLE            0
#define MINIMUM_FIRMWARE_VERSION "0.6.6"

// BLE PINS
#define BLUEFRUIT_HWSERIAL_NAME Serial1
#define BLUEFRUIT_UART_MODE_PIN      -1

// ORGANISM PINS
#define P1_A 33
#define P1_B 35
#define P1_C 37
#define P1_D 39
#define P1_E 41

#define P2_A 43
#define P2_B 45
#define P2_C 47
#define P2_D 49
#define P2_E 51

// MISC PINS
#define BUTTON 53
#define LED    13

// BLE SETUP
Adafruit_BluefruitLE_UART ble(BLUEFRUIT_HWSERIAL_NAME, BLUEFRUIT_UART_MODE_PIN);

// ORGANISM SETUP
Organism p1(P1_A, P1_B, P1_C, P1_D, P1_E);
Organism p2(P2_A, P2_B, P2_C, P2_D, P2_E);

//Error helper for BLE
void error(const __FlashStringHelper*err) {
  Serial.println(err);
  while (1);
}

void initBLE() {
  // Initialize BLE Module
  Serial.print(F("Initializing Bluetooth: "));

  if (!ble.begin(VERBOSE_MODE)) {
    error(F("Couldn't find Bluefruit, make sure it's in CMD mode and check wiring..."));
  }
  Serial.println(F("OK!"));

  if (FACTORYRESET_ENABLE) {
    // Perform a factory reset to make sure everything is in a known state
    Serial.println(F("Performing a factory reset: "));
    if (!ble.factoryReset()) {
      error(F("Couldn't reset"));
    }
  }

  // Disable echo from BLE
  ble.echo(false);

  Serial.println("Requesting BLE Info:");
  ble.info();

  // Change the device name to make it easier to find
  Serial.println(F("Setting device name to 'Emile':"));
  if (!ble.sendCommandCheckOK(F("AT+GAPDEVNAME=Emile"))) {
    error(F("Could not set device name"));
  }

  // Enable HID Service
  Serial.println(F("Enable HID Service (including keyboard):"));
  if (ble.isVersionAtLeast(MINIMUM_FIRMWARE_VERSION) && !ble.sendCommandCheckOK(F("AT+BleHIDEn=On"))) {
      error(F("Could not enable keyboard"));
  } else if (!ble.sendCommandCheckOK(F("AT+BleKeyboardEn=On"))) {
    error(F("Could not enable keyboard"));
  }

  // Add or remove service requires a reset
  Serial.println(F("Performing a SW reset (service changes require a reset):"));
  if (!ble.reset()) {
    error(F("Couldn't reset"));
  }

  Serial.println(F("\nEmile is ready to pair\n"));
}

void setup() {
  while (!Serial);
  delay(500);
  
  Serial.begin(115200);
  Serial.println(F("EMILE :0"));
  Serial.println(F("--------"));
  
  initBLE();
  pinMode(13, OUTPUT);
  pinMode(BUTTON, INPUT_PULLUP);
}

// the loop function runs over and over again forever
void loop() {
  int p1Val = p1.read();
  int p2Val = p2.read();

  String p1Name = p1.readName();
  String p2Name = p2.readName();

  String p1Char = String(p1Val >= 0 ? (char)(p1Val + 97) : '0');
  String p2Char = String(p2Val >= 0 ? (char)(p2Val + 97) : '0');
  
  /*Serial.print("p1: ");
  Serial.print(p1.readName());
  Serial.print(", ");
  Serial.println(p1.read(), p1.read() >= 0 ? BIN : DEC);

  Serial.print("p2: ");
  Serial.print(p2.readName());
  Serial.print(", ");
  Serial.println(p2.read(), p2.read() >= 0 ? BIN : DEC);
  */
  /*Serial.print(digitalRead(P1_A));
  Serial.print(", ");
  Serial.print(digitalRead(P1_B));
  Serial.print(", ");
  Serial.print(digitalRead(P1_C));
  Serial.print(", ");
  Serial.print(digitalRead(P1_D));
  Serial.print(", ");
  Serial.println(digitalRead(P1_E));*/
  if (digitalRead(BUTTON) == LOW) {
    digitalWrite(13, HIGH);
    
    Serial.print("p1: ");
    Serial.print(p1.readName());
    Serial.print(", ");
    Serial.println(p1.read(), p1.read() >= 0 ? BIN : DEC);

    Serial.print("p2: ");
    Serial.print(p2.readName());
    Serial.print(", ");
    Serial.println(p2.read(), p2.read() >= 0 ? BIN : DEC);

    String pStr = String("<" + p1Char + "," + p2Char + ">");

    Serial.print("\nSending Data: ");
    Serial.println(pStr);

    ble.print("AT+BleKeyboard=");
    ble.println(pStr);

    if (ble.waitForOK()) {
      Serial.println(F("OK!"));
    } else {
      Serial.println(F("FAILED!"));
    }
  } else {
    digitalWrite(13, LOW);
  }
}
