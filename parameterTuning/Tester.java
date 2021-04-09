public class Tester {
  public static void main (String [] args) {
    //public Organism (String name, int id,
    //               Input[] input, int[] output,
    //               int repRate, int lifeSpan, int conRate);
    Input[] inputs0 = {new Input(1,Input.CON), new Input(2,Input.CON),
      new Input(3,Input.CON), new Input(14,Input.CON),
      new Input(Input.O2,Input.CON)};
    int[] outputs0 = {Input.F, Input.CO2};
    Organism carnivore = new Organism ("carnivore", 0,
                                       inputs0, outputs0,
                                       2, 30, 5);

    Input[] inputs1 = {new Input(0,Input.DED), new Input(1,Input.DED),
      new Input(2,Input.DED), new Input(3,Input.DED), new Input(14,Input.DED),
      new Input(15,Input.DED), new Input(Input.O2,Input.CON)};
    int[] outputs1 = {Input.F, Input.CO2};
    Organism detritivore = new Organism ("detritivore", 1,
                                         inputs1, outputs1,
                                         2, 45, 2);

    Input[] inputs3 = {new Input(8,Input.CON), new Input(9,Input.CON),
      new Input(10,Input.CON), new Input(13,Input.CON),
      new Input(Input.O2,Input.CON)};
    int[] outputs3 = {Input.F, Input.CO2};
    Organism grazer = new Organism ("grazer", 3,
                                    inputs3, outputs3,
                                    3, 20, 10);

    Input[] inputs9 = {new Input(Input.F,Input.CON),
      new Input(Input.L,Input.CON), new Input(Input.W,Input.CON),
      new Input(Input.CO2,Input.CON)};
    int[] outputs9 = {Input.O2};
    Organism grass = new Organism ("grass", 9,
                                   inputs9, outputs9,
                                   100, 10, 1);


    Organism[] orgs = {carnivore, detritivore, grazer, grass};

    Sim world = new Sim (orgs, 100, 100, 100, 100, 100);

    System.out.println(world);

    world.runSim(true);

    System.out.println(world);
  }
}
