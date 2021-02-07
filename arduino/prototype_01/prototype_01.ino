#include <Wire.h>
#include <Adafruit_NeoPixel.h>

#define A_PIN    13
#define B_PIN    12
#define C_PIN    11
#define D_PIN    10
#define LED_PIN   8

#define SUB_A     1
#define SUB_B     2
#define SUB_C     3
#define SUB_NONE  0
#define SUB_ERR  -1

#define L LOW
#define H HIGH

// LED strip
Adafruit_NeoPixel led(1, LED_PIN, NEO_GRB + NEO_KHZ800);

const int PIN_A[]= {0,0,0,0};
const int PIN_B[] = {1,0,0,1};
const int PIN_C[] = {1,0,1,1};
const int PIN_NONE[] = {1,1,1,1};

void setup() {
  Serial.begin(9600);

  pinMode(A_PIN,INPUT_PULLUP);
  pinMode(B_PIN,INPUT_PULLUP);
  pinMode(C_PIN,INPUT_PULLUP);
  pinMode(D_PIN,INPUT_PULLUP);
  
  led.begin();
  led.setBrightness(50); // Sets the brightness to ~1/5 of max
  led.setPixelColor(0, led.Color(0,0,0));
  led.show();
  
  Serial.println("setup complete");
}

void loop() {
  //Serial.println("loop");
  delay(10);

  int lightCheck = checkLight();

  if(lightCheck == SUB_NONE) {
    led.setPixelColor(0,led.Color(0,0,0));
    Serial.println("SUB_NONE");
  }
  else if(lightCheck == SUB_A) {
    led.setPixelColor(0,led.Color(255,0,255));
    Serial.println("SUB_A");
  }
  else if(lightCheck == SUB_B) {
    led.setPixelColor(0,led.Color(0,255,255));
    Serial.println("SUB_B");
  }
  else if(lightCheck == SUB_C) {
    led.setPixelColor(0,led.Color(255,255,0));
    Serial.println("SUB_C");
  }
  else {
    led.setPixelColor(0,led.Color(255,0,0));
    Serial.println("registration error");
  }

  led.show();
}

bool compareState(int stateA[], int stateB[]) {
  return (stateA[0] == stateB[0]) && (stateA[1] == stateB[1]) && (stateA[2] == stateB[2]) && (stateA[3] == stateB[3]);
}

int checkLight() {
  int state[] = {digitalRead(A_PIN),digitalRead(B_PIN),digitalRead(C_PIN),digitalRead(D_PIN)};
  bool trip = false;

  if(compareState(state,PIN_A)) {
    return SUB_A;
  }
  else if(compareState(state,PIN_B)) {
    return SUB_B;
  }
  else if(compareState(state,PIN_C)) {
    return SUB_C;
  }
  else if(compareState(state,PIN_NONE)) {
    return SUB_NONE;
  }
  else {
    return SUB_ERR;
  }
}
