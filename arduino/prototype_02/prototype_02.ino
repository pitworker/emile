// the setup function runs once when you press reset or power the board
#define BUTTON 53

#define P1_A   51
#define P1_B   49
#define P1_C   47
#define P1_D   45
#define P1_E   43

#define P2_A   41
#define P2_B   39
#define P2_C   37
#define P2_D   35
#define P2_E   33

#define BL_MOD 22
#define BL_CTS 24
#define BL_TXO 26
#define BL_RXI 28
#define BL_RTS 30

void setup() {
  // initialize digital pin 13 as an output.
  pinMode(13, OUTPUT);
  pinMode(BUTTON, INPUT_PULLUP);
}

// the loop function runs over and over again forever
void loop() {
  Serial.println(digitalRead(BUTTON));
  if (digitalRead(BUTTON) == LOW) {
    digitalWrite(13, HIGH);
  } else {
    digitalWrite(13, LOW);
  }
}
