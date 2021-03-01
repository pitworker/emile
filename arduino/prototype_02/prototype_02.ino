#include <Organism.h>

// the setup function runs once when you press reset or power the board
#define BUTTON  2

#define P1_A    3
#define P1_B    4
#define P1_C    5
#define P1_D    6
#define P1_E    7

#define P2_A    8
#define P2_B    9
#define P2_C   10
#define P2_D   11
#define P2_E   12

#define BL_MOD 22
#define BL_CTS 24
#define BL_TXO 26
#define BL_RXI 28
#define BL_RTS 30

#define LED    13

Organism p1(P1_A, P1_B, P1_C, P1_D, P1_E);
Organism p2(P2_A, P2_B, P2_C, P2_D, P2_E);


void setup() {
  // initialize digital pin 13 as an output.
  Serial.begin(9600);
  pinMode(13, OUTPUT);
  pinMode(BUTTON, INPUT_PULLUP);
}

// the loop function runs over and over again forever
void loop() {
  Serial.print("p1: ");
  Serial.print(p1.orgName(p1.read()));
  Serial.print(", ");
  Serial.println(p1.read(), BIN);

  Serial.print("p2: ");
  Serial.print(p2.orgName(p2.read()));
  Serial.print(", ");
  Serial.println(p2.read(), BIN);
  
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
  } else {
    digitalWrite(13, LOW);
  }
}
