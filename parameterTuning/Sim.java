/**
 * Simulates the world with the input organisms over 14 days.
 * Assigns a score based on the population of each organism by the end.
 * @author Sebastian Carpenter
 * @version 0.0
 */

import java.util.Random;
import java.util.ArrayList;

public class Sim {
  private static final int SIM_LENGTH = 14;

  private Organism[] orgs;
  private int[] population;
  private int[] stability;

  private int[] inorgs;

  private Random rand;

  /**
   * Constructs a new Sim based on an array of orgs. Assumes all other
   * parameters are zero.
   * @param Organism[] orgs The list of organisms present in the simulation.
   * @ensures Organisms in the simulation are the input list
   * @ensures initial population is an empty array of length or orgs.
   * @ensures initial stability is an empty array of length orgs.
   * @ensures inorgs is an array of length Input.NUM_INORGS, initialized to 0.
   */
  public Sim (Organism[] orgs) {
    this.orgs = orgs;
    this.population = new int[orgs.length];
    this.stability = new int[orgs.length];
    this.rand = new Random();
    this.inorgs = new int[Input.NUM_INORGS];
    for (int i = 0; i < this.inorgs.length; ++i) {
      this.inorgs[i] = 0;
    }
  }

  /**
   * Constructs a new Sim based on an array of orgs and initial values for all
   * inorganic inputs.
   * @param Organism[] orgs The list of organisms present in the simulation.
   * @param int initialF the initial quantity of fertilizer.
   * @param int initialW the initial quantity of wind.
   * @param int initialL the initial quantity of light.
   * @param int initialO2 the initial quantity of oxygen.
   * @param int initialCO2 the initial quantity of carbon dioxide.
   * @ensures Organisms in the simulation are the input list
   * @ensures initial population is an empty array of length or orgs.
   * @ensures initial stability is an empty array of length orgs.
   * @ensures inorgs is an array of length Input.NUM_INORGS, values initialized
   *          to those input.
   */
  public Sim (Organism[] orgs, int initialF, int initialW, int initialL,
              int initialO2, int initialCO2) {
    this.orgs = orgs;
    this.population = new int[orgs.length];
    this.stability = new int[orgs.length];
    this.inorgs = new int[Input.NUM_INORGS];
    this.inorgs[0] = initialCO2;
    this.inorgs[1] = initialO2;
    this.inorgs[2] = initialL;
    this.inorgs[3] = initialW;
    this.inorgs[4] = initialF;
    this.rand = new Random();
  }

  /**
   * Finds the index of an organism in the orgs array from an Organism object.
   * @param Organism org the organism whose index is being looked for.
   * @return int the index of the organism, -1 if organism is not in array.
   * @ensures nothing is altered
   */
  private int findOrgIndex (Organism org) {
    for (int i = 0; i < this.orgs.length; ++i) {
      if (this.orgs[i].getID() == org.getID()) return i;
    }
    return -1;
  }

  /**
   * Finds the index of an organism in the orgs array from an organism ID.
   * @param int id the organism ID.
   * @return int the index of the organism, -1 if organism is not in array.
   * @ensures nothing is altered.
   */
  private int findOrgIndex (int id) {
    for (int i = 0; i < this.orgs.length; ++i) {
      if (this.orgs[i].getID() == id) return i;
    }
    return -1;
  }

  /**
   * Kills n random organisms of a given index
   * @param int o the index of the given organism
   * @param int n the number of organisms to kill
   * @param int[] dead the array of dead organisms
   * @param int[][] exps the 2D array of organism expirations
   * @ensures value at o of dead is incremented by n
   * @ensures random values at o of exps are decremented by net n
   * @ensures this.population decremented by n at index o
   */
  private void kill (int o, int n, int[] dead, int[][] exps) {
    while (n > 0) {
      int r = this.rand.nextInt(exps[o].length);
      int m = this.rand.nextInt(Math.min(exps[o][r], n));
      exps[o][r] -= m;
      this.population[o] -= m;
      dead[o] += m;
      n -= m;
    }
  }

  /**
   * Kills n random organisms of a given index, checking to make sure
   * n <= this.population[o] beforehand. Returns new value at index o of
   * this.population.
   * @param int o the index of the given organism
   * @param int n the number of organisms to kill
   * @param int[] dead the array of dead organisms
   * @param int[][] exps the 2D array of organism expirations
   * @return int the new value of this.population at index o.
   * @ensures nothing is altered if n > this.population[o]
   * @ensures value at o of dead is incremented by n
   * @ensures random values at o of exps are decremented by net n
   * @ensures this.population decremented by n at index o
   */
  private int safeKill (int o, int n, int[] dead, int[][] exps) {
    if (n > this.population[o]) return -1;
    kill(o, n, dead, exps);
    return this.population[o];
  }

  /**
   * Manages consumption of organisms. Picks random organisms from a list of
   * prey and commits safeKills of random quantities until consumption rate has
   * been reached.
   * @param int rel the relationship to the prey (Input.DED or Input.CON
   *                expected).
   * @param ArrayList<Integer> consumption ArrayList of potential prey.
   * @param int conRate the quantity of prey that need be consumed.
   * @param int[] eaten array of organisms that have been consumed.
   * @param int[] dead array of organisms that have died.
   * @param int[][] exps 2D array of organism expirations.
   * @ensures exactly conRate of organisms are consumed, and only organisms in
   *          consumption will be consumed.
   * @ensures consumed organisms are added to eaten array.
   * @ensures when DED, organisms consumed from eaten, otherwise, consumed from
   *          general population.
   */
  private void eat (int rel, ArrayList<Integer> consumption, int conRate,
                    int[] eaten, int[] dead, int[][] exps) {
    // c = random item from consumption
    // n = random number from 1 to min(c.population - 1, conRate)
    // safeKill n of c
    // conRate -= n
    // repeat until conRate = 0
    int e = conRate;
    if (rel == Input.DED) {
      while (e > 0) {
        int r = this.rand.nextInt(consumption.size());
        int n = this.rand.nextInt(Math.min(dead[consumption.get(r)],
                                           e));
        dead[consumption.get(r)] -= n;
        eaten[consumption.get(r)] += n;
        e -= n;
      }
    } else if (rel == Input.CON) {
      while (e > 0) {
        int r = this.rand.nextInt(consumption.size());
        int n = this.rand.nextInt(Math.min(this.population[consumption.get(r)],
                                           e));
        /*
          safeKill called with eaten instead of dead because eaten organisms are
          not edible to detritivores, therefore not considered dead
         */
        safeKill(consumption.get(r), n, eaten, exps);
        e -= n;
      }
    }
  }

  /**
   * For a given species, manages a round of consumption. Adjusts populations,
   * eaten, dead, and exps accordingly. Kills proportional number of organisms
   * if proper quantities of inputs do not exist.
   * exist
   * @param int o the index of the organism
   * @param int[] eaten array of quantities of organisms that have been consumed
   * @param int[] dead array of quantities of dead organisms.
   * @param int[][] exps 2D array of organism expirations
   * @ensures dead, exps, populations updated accordingly to consumption.
   * @ensures safeKill applied to proportional number of given organism if
   *          insufficient input populations exist.
   * @ensures nothing else is altered
   */
  private void consume (int o, int[] eaten, int[] dead, int[][] exps) {
    Organism org = this.orgs[o];
    boolean[] inputsNeeded = new boolean[this.orgs.length + Input.NUM_INORGS];
    //ids offset to accomodate inorganics
    int conRate = org.getConRate() * this.population[o];
    ArrayList<Integer> consumptionOrgs = new ArrayList<Integer>();
    ArrayList<Integer> consumptionDead = new ArrayList<Integer>();

    for (int i = 0; i < inputsNeeded.length; ++i) {
      inputsNeeded[i] = org.getInputById(i - Input.NUM_INORGS) != null;
    }

    for (int i = 0; i < Input.NUM_INORGS; ++i) {
      if (inputsNeeded[i] && conRate <= this.inorgs[i]) {
        this.inorgs[i] -= conRate;
      } else if (inputsNeeded[i]) {
        int overflow = conRate - this.inorgs[i];
        this.inorgs[i] = 0;
        safeKill(o,
                 (int)Math.ceil((double)overflow / (double)org.getConRate()),
                 dead, exps);
      }
    }

    for (int i = Input.NUM_INORGS; i < inputsNeeded.length; ++i) {
      int inputRel = org.getInputById(i - Input.NUM_INORGS).getRel();
      if (inputsNeeded[i] &&
          inputRel == Input.NONC &&
          conRate > this.population[i - Input.NUM_INORGS]) {
        int overflow = conRate - this.population[i - Input.NUM_INORGS];
        safeKill(o,
                 (int)Math.ceil((double)overflow / org.getConRate()),
                 dead, exps);
      } else if (inputsNeeded[i] &&
                 inputRel == Input.DED) {
        consumptionDead.add(i - Input.NUM_INORGS);
      } else if (inputsNeeded[i] &&
                 inputRel == Input.CON) {
        consumptionOrgs.add(i - Input.NUM_INORGS);
      }
    }

    if (consumptionDead.size() > 0) {
      eat(Input.DED, consumptionDead, conRate, eaten, dead, exps);
    }
    if (consumptionOrgs.size() > 0) {
      eat(Input.CON, consumptionOrgs, conRate, eaten, dead, exps);
    }
  }

  /**
   * checks whether there is enough food for a given organism.
   * @param Organism org the organism
   * @return boolean true if there is enough food, false otherwise
   * @ensures nothing is altered
   */
  private boolean checkFood (Organism org) {
    int consumableOrgs = 0;
    int nonConsumableOrgs = 0;

    Input[] inps = org.getInput();

    for (int i = 0; i < inps.length; ++i) {
      if (inps[i].getID() < 0 &&
          this.inorgs[(inps[i].getID() * -1) - 1] < org.getConRate()) {
        return false;
      } else if (inps[i].getID() >= 0 && inps[i].getRel() == Input.NONC) {
        nonConsumableOrgs += this.population[findOrgIndex(inps[i].getID())];
      } else if (inps[i].getID() >= 0) {
        consumableOrgs += this.population[findOrgIndex(inps[i].getID())];
      }
    }
    return consumableOrgs >= org.getConRate() &&
      nonConsumableOrgs >= org.getConRate();
  }

  /**
   * Produces one iteration of outputs for a given organism
   * @param int o the index of the organism
   * @ensures populations of inorganic materials adjusted accordingly
   * @ensures nothing else is altered
   */
  private void produce (int o) {
    int[] outs = this.orgs[o].getOutput();
    for (int i = 0; i < outs.length; ++i) {
      this.inorgs[outs[i] * -1 - 1] +=
        this.orgs[o].getConRate() * this.population[o];
    }
  }

  /**
   * Given a certain organism, reproduces that organism.
   * @param int o the index of the given organism
   * @param int[] exps the expirations for the given organism
   * @param int i the current interation of the simulation
   * @ensures population and exps are updated accordingly
   * @ensures nothing else is changed
   */
  private void reproduce (int o, int[] exps, int i) {
    int index = Math.min(i + this.orgs[o].getLifeSpan(), exps.length);
    int offspring = this.orgs[o].getRepRate() * this.population[o];
    this.population[o] += offspring;
    exps[index] += offspring;
  }

  /**
   * Prints the current state of the simulation world.
   * @param int i the current iteration of the simulation
   * @param int[] eaten the quantities of organisms that have been consumed
   * @param int[] dead the quantities of organsisms that are dead
   * @param int[][] exps the expirations of each organism
   * @ensures nothing is altered
   */
  private void printCurrentState(int i, int[] eaten, int[] dead, int[][] exps) {
    System.out.println("Iteration " + i + ":");
    for (int o = 0; o < this.orgs.length; ++o) {
      System.out.println("  " + this.orgs[o].getName() + ":");
      System.out.println("    Population: " + this.population[o]);
      System.out.println("    Stability:  " + this.stability[o]);
      System.out.println("    Dead:       " + dead[o]);
      System.out.println("    Eaten:      " + eaten[o]);
      System.out.print(  "    Exps:       [");
      for (int j = 0; j < exps[o].length; ++j) {
        System.out.print((j > 0 ? ", " : "") + exps[o][j]);
      }
      System.out.println("]");
    }
    System.out.println("  CO2: " + this.inorgs[Input.CO2 + Input.NUM_INORGS]);
    System.out.println("  O2: " + this.inorgs[Input.O2 + Input.NUM_INORGS]);
    System.out.println("  Light: " + this.inorgs[Input.L + Input.NUM_INORGS]);
    System.out.println("  Wind: " + this.inorgs[Input.W + Input.NUM_INORGS]);
    System.out.println("  Fertilizer: " +
                       this.inorgs[Input.F + Input.NUM_INORGS]);
  }

  /**
   * Runs a simulation with the initial conditions defined at construction.
   * @param boolean debugMode if true, output of populations is printed at each
                              iteration.
   * @ensures all populations permanently adjusted according to results of
   *          simulation.
   */
  public void runSim (boolean debugMode) {
    int[] eaten = new int[this.orgs.length];
    int[] dead = new int[this.orgs.length];
    int[][] exps = new int[this.orgs.length][this.SIM_LENGTH + 1];
    //exps counts how many of a given organism should die of age at a given time

    //Setup the initial populations for the simulation
    for (int i = 0; i < this.orgs.length; ++i) {
      this.population[i] = this.orgs[i].getRepRate();
      this.stability[i] = 0;
      eaten[i] = 0;
      dead[i] = 0;
      for (int j = 0; j < this.SIM_LENGTH; ++j) {
        exps[i][j] = 0;
      }
      if (this.orgs[i].getLifeSpan() >= SIM_LENGTH) {
        exps[i][SIM_LENGTH] = this.population[i];
      } else {
        exps[i][this.orgs[i].getLifeSpan()] = this.population[i];
      }
    }

    //Run through the rounds of the population
    for (int i = 0; i < this.SIM_LENGTH; ++i) {
      for (int j = 0; j < this.orgs.length; ++j) {
        int lastPop = this.population[j];
        if (this.population[j] > 0) {
          this.population[j] -= exps[j][i];
          dead[j] += exps[j][i];
          exps[j][i] = 0;
          consume(j, eaten, dead, exps);
          produce(j);
          reproduce(j, exps[j], i);
        }
        this.stability[j] =
          Math.abs(this.stability[j] + this.population[j] - lastPop) /
          Math.max(1, i);
      }
      if (debugMode) printCurrentState(i, eaten, dead, exps);
    }
  }

  /**
   * Outputs the current state of the simulation world as a string
   * @return String the current state of the simulation world as a string
   * @ensures nothing is altered.
   */
  public String toString() {
    String str = "{\n";
    for (int i = 0; i < this.orgs.length; ++i) {
      str = str + "  " + this.orgs[i].getName() + " {\n" +
        "    population: " + this.population[i] + "\n" +
        "    stability: " + this.stability[i] + "\n  }\n";
    }
    str = str + "  CO2 {\n    population: " +
      this.inorgs[Input.CO2 + Input.NUM_INORGS] + "\n  }\n  " +
      "O2 {\n    population: " +
      this.inorgs[Input.O2 + Input.NUM_INORGS] + "\n  }\n  " +
      "Light {\n    population: " +
      this.inorgs[Input.L + Input.NUM_INORGS] + "\n  }\n  " +
      "Wind {\n    population: " +
      this.inorgs[Input.W + Input.NUM_INORGS] + "\n  }\n  " +
      "Fertilizer {\n    population: " +
      this.inorgs[Input.F + Input.NUM_INORGS] + "\n  }\n" +
      "}";
    return str;
  }
}
