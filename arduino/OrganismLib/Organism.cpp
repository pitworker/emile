#include "Arduino.h"
#include "Organism.h"

Organism::Organism(int a, int b, int c, int d, int e) {
  pinMode(a, INPUT_PULLUP);
  pinMode(b, INPUT_PULLUP);
  pinMode(c, INPUT_PULLUP);
  pinMode(d, INPUT_PULLUP);
  pinMode(e, INPUT_PULLUP);
  _pins = {a,b,c,d,e};
}

int Organism::read() {
  int reading;
  for (int i = 0; i < 5; ++i) {
    reading |= digitalRead(_pins[i]) << i;
  }
  return reading - 1;
}
