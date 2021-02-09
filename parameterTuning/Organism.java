/**
 * Stores all characteristics of an organism.
 * @author Sebastian Carpenter
 * @version 0.0
 */

import java.util.Arrays;

public class Organism {
  private String name;
  private int id;
  private Input[] input;
  private int[] output;
  private int repRate;
  private int lifeSpan;
  private int conRate;

  /**
   * Constructs a new Organism with all parameters explicitly defined.
   * @param name the name of the organism
   * @param id the id of the organism
   * @param input a String containing of ids and conjunctions of inputs
   * @param output the array of output materials
   * @param repRate the rate at which the organism reproduces
   * @param lifeSpan the length for which the organism lives
   * @param conRate the rate at which the organism consumes its inputs
   * @requires id is a positive number
   * @requires input and output are not empty
   * @requires repRate, lifeSpan, conRate are positive numbers
   */
  public Organism (String name, int id,
                   Input[] input, int[] output,
                   int repRate, int lifeSpan, int conRate) {
    this.name = name;
    this.id = id;
    this.input = input;
    this.output = output;
    this.repRate = repRate;
    this.lifeSpan = lifeSpan;
    this.conRate = conRate;
  }

  /**
   * Constructs a new Organism with the characteristics of the input organism.
   * @param org the input organism
   * @requires the input organism is a valid organism
   */
  public Organism (Organism org) {
    this.name = org.getName();
    this.id = org.getID();
    this.input = org.getInput();
    this.output = org.getOutput();
    this.repRate = org.getRepRate();
    this.lifeSpan = org.getLifeSpan();
    this.conRate = org.getConRate();
  }

  /**
   * Returns the name of the organism
   * @return String the name of the organism.
   * @ensures nothing is altered.
   */
  public String getName () {
    return this.name;
  }

  /**
   * Returns the id of the organism
   * @return int the id of the organism.
   * @ensures nothing is altered.
   */
  public int getID () {
    return this.id;
  }

  /**
   * Returns the input array of the organism
   * @return Input[] the array of inputs for the organism.
   * @ensures nothing is altered.
   */
  public Input[] getInput () {
    return this.input;
  }

  /**
   * Returns the output array of the organism
   * @return int[] the array of output ids for the organism.
   * @ensures nothing is altered.
   */
  public int[] getOutput () {
    return this.output;
  }

  /**
   * Returns the reproduction rate of the organism
   * @return int the reproduction rate of the organism.
   * @ensures nothing is altered.
   */
  public int getRepRate () {
    return this.repRate;
  }

  /**
   * Returns the lifespan of the organism
   * @return int the lifespan of the organism.
   * @ensures nothing is altered.
   */
  public int getLifeSpan () {
    return this.lifeSpan;
  }

  /**
   * Returns the consumption rate of the organism
   * @return int the consumption rate of the organism.
   * @ensures nothing is altered.
   */
  public int getConRate () {
    return this.conRate;
  }

  /**
   * Sets the name of the organism
   * @param name the new name of the organism
   * @ensures the name is set to the input, nothing else altered
   */
  public void setName (String name) {
    this.name = name;
  }

  /**
   * Sets the id of the organism if valid
   * @param id the new id of the organism
   * @return int the new id, -1 if invalid
   * @ensures the id has been set to a valid id, nothing else altered
   */
  public int setID (int id) {
    if (id > 0) {
      this.id = id;
      return this.id;
    }
    return -1;
  }

  /**
   * Sets the input array of the organism if valid
   * @param input the new input array for the organism
   * @return int the length of the new array, -1 if invalid
   * @ensures the input array has been set to a valid array, 
   *          nothing else altered
   */
  public int setInput (Input[] input) {
    if (input.length > 0) {
      this.input = input;
      return this.input.length;
    }
    return -1;
  }

  /**
   * Sets the output array of the organism if valid
   * @param output the new output array of the organism
   * @return int the length of the new output array, -1 if invalid
   * @ensures output array has been set to a valid array, nothing else altered
   */
  public int setOutput (int[] output) {
    if (output.length > 0) {
      this.output = output;
      return this.output.length;
    }
    return -1;
  }

  /**
   * Sets the reproduction rate of the organism, returns -1 if invalid
   * @param repRate the new reproduction rate of the organism
   * @return int the new reproduction rate, -1 if invalid
   * @ensures the reproduction rate has been set to a valid number, 
   *          nothing else altered
   */
  public int setRepRate (int repRate) {
    if (repRate > 0) {
      this.repRate = repRate;
      return this.repRate;
    }
    return -1;
  }

  /**
   * Sets the lifespan of the organism, returns -1 if invalid
   * @param lifeSpan the new lifespan of the organism
   * @return int the new lifespan, -1 if invalid
   * @ensures the lifespan has been set to a valid number, nothing else altered
   */
  public int setLifeSpan (int lifeSpan) {
    if (lifeSpan > 0) {
      this.lifeSpan = lifeSpan;
      return this.lifeSpan;
    }
    return -1;
  }

  /**
   * Sets the consumption rate of the organism, returns -1 if invalid
   * @param conRate the new consumption rate of the organism
   * @return int the new consumption rate, -1 if invalid
   * @ensures the consumption rate has been set to a valid number, 
   *          nothing else altered
   */
  public int setConRate (int conRate) {
    if (conRate > 0) {
      this.conRate = conRate;
      return this.conRate;
    }
    return -1;
  }

  /**
   * returns the organism object in String form
   * @return String a String representation of the organism
   * @ensures nothing is altered
   */
  public String toString () {
    return "{\n  name: " + this.name +
      "\n  id: " + this.id +
      "\n  input: " + Arrays.toString(this.input) +
      "\n  output: " + Arrays.toString(this.output) +
      "\n  repRate: " + this.repRate +
      "\n  lifeSpan: " + this.lifeSpan +
      "\n  conRate: " + this.conRate +
      "\n}";
  }
}
