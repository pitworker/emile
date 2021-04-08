/**
 * Stores an input for an organism.
 * @author Sebastian Carpenter
 * @version 0.0
 */

public class Input {
  //whether input is organism or not
  public static final boolean ORG = true;
  //consumable
  public static final int CON = 0;
  //nonconsumable
  public static final int NONC = 1;
  //consumable when dead
  public static final int DED = 2;

  //ID for fertilizer
  public static final int F = -1;
  //ID for wind
  public static final int W = -2;
  //ID for light
  public static final int L = -3;
  //ID for oxygen
  public static final int O2 = -4;
  //ID for carbon dioxide
  public static final int CO2 = -5;

  public static final int NUM_INORGS = 5;
    
  private int id;
  private int rel;

  /**
   * Constructs a new input object given an id and relationship.
   * @param id the id of the input object
   * @param rel how the input is consumed
   * @requires id must be either an existing organism or one of the non-organism
   *           constants
   */
  public Input (int id, int rel) {
    this.id = id;
    this.rel = rel;
  }

  /**
   * Constructs a new input object given an organism and relationship.
   * @param org the organism for the input object
   * @param rel how the input is consumed
   * @requires org must a valid organism
   */
  public Input (Organism org, int rel) {
    this.id = org.getID();
    this.rel = rel;
  }

  /**
   * Returns the id of the input object
   * @return int the id of the input object.
   * @ensures nothing is altered.
   */
  public int getID () {
    return this.id;
  }

  /**
   * Returns the relationship of the input object
   * @return int the relationship of the input object
   * @ensures nothing is altered.
   */
  public int getRel () {
    return this.rel;
  }

  /**
   * returns the Input object in String form
   * @return String a String representation of the input
   * @ensures nothing is altered
   */
  public String toString () {
    return "{id: " + this.id + ", rel: " + this.rel + "}";
  }
}
