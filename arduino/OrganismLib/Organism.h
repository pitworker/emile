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
  const int ERR = -1;
  const char *ORGS[16] = {
    "Carnivore 1", //0
    "Detritivore", //1
    "Omnivore",    //2
    "Grazer",      //3
    "Insect",      //4
    "Shroom",      //5
    "Mold",        //6
    "Tree",        //7
    "Flower",      //8
    "Grass",       //9
    "Legume",      //10
    "Vine",        //11
    "Lichen",      //12
    "Fern",        //13
    "Carnivore 2", //14
    "Mollusk"      //15
  };

private:
  int _pins[5];
}

#endif
