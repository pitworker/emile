public class Tester {
  public static void main (String [] args) {
    //public Organism (String name, int id,
    //               Input[] input, int[] output,
    //               int repRate, int lifeSpan, int conRate);
    Input[] inputs = {new Input(2,Input.CON), new Input(3,Input.CON),
      new Input(4,Input.CON), new Input(15,Input.CON),
      new Input(Input.O2,Input.CON)};
    int[] outputs = {Input.F, Input.CO2};
    Organism carnivore = new Organism ("carnivore", 1,
                                       inputs, outputs,
                                       2, 30, 5);
    
    System.out.println(carnivore);

    carnivore.setRepRate(1);
    carnivore.setLifeSpan(45);

    System.out.println(carnivore);
  }
}
