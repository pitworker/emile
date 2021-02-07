/**
 * Simulates the world with the input organisms over 14 days.
 * Assigns a score based on the population of each organism by the end.
 * @author Sebastian Carpenter
 * @version 0.0
 */

public class Sim {
  private static final int simLength = 14;

  private Organism[] orgs;
  private int[] population;
  private int[] stability;

  public Sim (Organism[] orgs) {
    this.orgs = orgs;
    this.population = new int[orgs.length];
    this.stability = new int[orgs.length];
  }

  public void runSim() {
    
  }
}
