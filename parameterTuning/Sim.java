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

  public Sim (Organism[] orgs) {
    this.orgs = orgs;
    this.population = new int[orgs.length];
    this.stability = new int[orgs.length];
    this.inorgs = {0,0,0,0,0};
    this.rand = new Random();
  }

  public Sim (Organism[] orgs, int initialF, int initialW, int initialL,
              int initialO2, int initialCO2) {
    this.orgs = orgs;
    this.population = new int[orgs.length];
    this.stability = new int[orgs.length];
    this.inorgs = {initialCO2, initialO2, intitialL, initialW, initialF};
    this.rand = new Random();
  }

  private int findOrgIndex (Organism org) {
    for (int i = 0; i < this.orgs.length; i++) {
      if (this.orgs[i].getID() == org.getID()) return i;
    }
    return -1;
  }

  private int findOrgIndex (int id) {
    for (int i = 0; i < this.orgs.length; i++) {
      if (this.orgs[i].getID() == id) return i;
    }
    return -1;
  }

  private void kill (int[] exps) {
    // TODO: rewrite kill to fit new framework in consume
    /*
    int r = this.rand.nextInt(this.SIM_LENGTH + 1);
    if (exps[r] > 0) return r;
    return kill(exps);
    */
  }

  private void safeKill (int[] exps) {
    // TODO: rewrite safeKill to fit new framework in consume
    /*
    boolean check = false;
    for (int i = 0; i < exps.length; i++) {
      if (exps[i] > 0) check = true;
    }
    if (!check) return -1;
    return kill(exps);
    */
  }

  private void eat (int rel, ArrayList<Integer> consumption, int conRate,
                    int[] eaten, int[] dead, int[][] exps) {
    //TODO: cycle through killing random organisms from consumption until
    //      conrate is reached.
  }

  private void consume (int o, int[] eaten, int[] dead, int[][] exps) {
    Organism org = this.orgs[o];
    boolean[] inputsNeeded = new boolean[this.orgs.length + Input.NUM_INORGS];
    //ids offset to accomodate inorganics
    int conRate = org.getConRate() * this.population[o];
    ArrayList<int> consumptionOrgs = new ArrayList<int>();
    ArrayList<int> consumptionDead = new ArrayList<int>();

    for (int i = 0; i < inputsNeeded.length; i++) {
      inputsNeeded[i] = org.getInputById(i - Input.NUM_INORGS) != null;
    }

    for (int i = 0; i < Input.NUM_INORGS; i++) {
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

    for (int i = Input.NUM_INORGS; i < inputsNeeded.length; i++) {
      int inputRel = org.getInputById(i - Input.NUM_INORGS).getRel();
      if (inputsNeeded[i] &&
          inputRel == Input.NONC &&
          conRate > this.population(i - Input.NUM_INORGS)) {
        int overflow = conRate - this.population(i - Input.NUM_INORGS);
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

  private boolean checkFood (Organism org) {
    int consumableOrgs = 0;
    int nonconsumableOrgs = 0;

    Input[] inps = org.getInput();

    for (int i = 0; i < inps.length; i++) {
      if (inps[i].getID() < 0 &&
          this.inorgs[(inps[i].getID() * -1) - 1] < org.getConRate()) {
        return false;
      } else if (inps[i].getID() >= 0 && inps[i].getRel() == Input.NONC) {
        nonconsumableOrgs += this.population(findOrgIndex(inps[i].getID()));
      } else if (inps[i].getID() >= 0) {
        consumableOrgs += this.population(findOrgIndex(inps[i].getID()));
      }
    }
    return consumableOrgs >= org.getConRate() &&
      nonconsumableOrgs >= org.getConRate();
  }

  private void produce (int o) {
    int outs = this.orgs[o].getOutput();
    for (int i = 0; i < outs.length; i++) {
      this.inorgs[outs[i] * -1 - 1] +=
        this.orgs[o].getConRate() * this.population[o];
    }
  }

  private void reproduce (int o, int[] exps, int i) {
    this.population[o] += this.orgs[o].getRepRate() * this.population[o];
  }

  public void runSim () {
    int[] eaten = new int[this.orgs.length];
    int[] dead = new int[this.orgs.length];
    int[][] exps = new int[this.orgs.length][this.SIM_LENGTH + 1];
    //exps counts how many of a given organism should die of age at a given time

    //Setup the initial populations for the simulation
    for (int i = 0; i < this.orgs.length; i++) {
      this.population[i] = this.orgs[i].getRepRate();
      this.stability[i] = 0;
      eaten[i] = 0;
      dead[i] = 0;
      for (int j = 0; j < this.SIM_LENGTH; j++) {
        exps[i][j] = 0;
      }
      if (this.orgs[i].getLifeSpan() >= SIM_LENGTH) {
        exps[i][SIM_LENGTH] = this.population[i];
      } else {
        exps[i][this.orgs[i].getLifeSpan()] = this.population[i];
      }
    }

    //Run through the rounds of the population
    for (int i = 0; i < this.SIM_LENGTH; i++) {
      for (int j = 0; j < this.orgs.length; j++) {
        if (this.population[j] > 0) {
          this.population[j] -= exps[j][i];
          dead[j] += exps[j][i];
          exps[j][i] = 0;
          consume(j, eaten, dead, exps);
          produce(j);
          reproduce(j, exps[j], i);
          /*
          for (int k = 0; k < this.population[j]; k++) {
            if (checkFood(this.orgs[j])) {
              eat(j, eaten, dead, exps[j]);
            } else {
              this.population[j]--;
              dead[j]++;
            }
          }
          */
        }
      }
    }
  }
}
