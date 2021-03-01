/* 
 *  Organism.h - Library for identifying pins of different organisms.
 *  Created by Sebastian Carpenter, 28 February 2021.
 */

#ifndef Organism_h
#define Organism_h

#include "Arduino.h"

class Organism {
public:
  Organism(int a, int b, int c, int d, int e);
  int read();
  String readName();
  static const int ERR = -1;

private:
  int _pins[5];
};

#endif
