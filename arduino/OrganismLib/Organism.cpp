/*
 * Organism.cpp - library for recognizing Emile organisms.
 * Created by Sebastian Carpenter 28 February 2021
 */

#include "Arduino.h"
#include "Organism.h"

Organism::Organism(int a, int b, int c, int d, int e) {
  pinMode(a, INPUT_PULLUP);
  pinMode(b, INPUT_PULLUP);
  pinMode(c, INPUT_PULLUP);
  pinMode(d, INPUT_PULLUP);
  pinMode(e, INPUT_PULLUP);
  _pins[0] = a;
  _pins[1] = b;
  _pins[2] = c;
  _pins[3] = d;
  _pins[4] = e;
}

int Organism::read() {
  int reading = 0;
  for (int i = 0; i < 5; ++i) {
    reading |= (1 - digitalRead(_pins[i])) << i;
  }
  return reading - 1;
}

String Organism::orgName(int o) {
  String names[16] = {
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
  if (o >= 0 && o < 16)
    return names[o];
  return "ERR";
}
