/**
 * Simulates the world with the input organisms over 14 days.
 * Assigns a score based on the population of each organism by the end.
 * @author Sebastian Carpenter
 * @version 0.0
 */

import java.util.Random;

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

  public Sim (Orgnaism[] orgs, int initialF, int initialW, int initialL,
              int initialO2, int initialCO2) {
    this.orgs = orgs;
    this.population = new int[orgs.length];
    this.stability = new int[orgs.length];
    this.inorgs = {initialF, initialW, initialL, intitialO2, initialCO2};
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
  
  private int kill (int[] exps) {
    int r = this.rand.nextInt(this.SIM_LENGTH + 1);
    if (exps[r] > 0) return r;
    return kill(exps);
  }

  private int safeKill (int[] exps) {
    boolean check = false;
    for (int i = 0; i < exps.length; i++) {
      if (exps[i] > 0) check = true;
    }
    if (!check) return -1;
    return kill(exps);
  }

  private void eat (int o, int[] eaten, int[] dead, int[] exps) {
    Organism org = this.orgs[o];
    // TODO: randomly choose which available food to eat and change populations
    //       accordingly
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

    for (int i = 0; i < this.SIM_LENGTH; i++) {
      for (int j = 0; j < this.orgs.length; j++) {
        if (this.population[j] > 0) {
          this.population[j] -= exps[j][i];
	  dead[j] += exps[j][i];
          exps[j][i] = 0;
          for (int k = 0; k < this.population[j]; k++) {
            if (checkFood(this.orgs[j])) {
	      eat(j, eaten, dead, exps[j]);
            } else {
              this.population[j]--;
	      dead[j]++;
            }
          }
	  produce(j);
	  reproduce(j, exps[j], i);
        }
      }
    }
  }
}
