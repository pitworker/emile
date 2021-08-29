using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController WC { get; private set; }
    public Hill[] hills;
    private float simFrequency = 600.0f;

    public int hardCodeSelection; 


    public enum Organism : ushort
    {
        Carnivore,
        Detritivore,
        Omnivore,
        Grazer,
        Insect,
        Shroom, 
        Mold, 
        Tree, 
        Flower,
        Grass,
        Legume, 
        Vine,
        Lichen, 
        Fern, 
        SmallCarnivore, 
        Mollusk
    }

    private Organism org0;
    private Organism org1;
    private Organism org2;
    private Organism org3;
    private float time; 

    //array to be filled with controllers 
    public OrganismController[] organismControllers;

    void Awake()
    {
        if (WC != null)
        {
            Debug.LogError("There is more than one instance of World Controller!");
            return;
        }

        WC = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        //add all the controllers to the list 
        organismControllers = new OrganismController[15];
        string[] controllerNames = new string[16] { "CarnivoreController", "DetritivoreController", "OmnivoreController", "GrazerController", "InsectController", "ShroomController", "MoldController", "TreeController", "FlowerController", "GrassController", "LegumeController", "VineController", "LichenController", "FernController", "SmallCarnivoreController", "MolluskController" };

        for(int i = 0; i < 15; i++)
        {
            GameObject g = GameObject.Find(controllerNames[i]); 
            if(g == null)
            {
                Debug.Log("WARNING: could not find controller " + controllerNames[i]); 
            }
            else
            {
                organismControllers[i] = g.GetComponent<OrganismController>();
                if (i == null) Debug.Log("WARNING: " + controllerNames[i] + "did not have the OrganismController script.");
            }
    
        }
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Simulate", simFrequency, simFrequency);
    }

    public void Sync(int o0, int o1, int o2, int o3, float time_since_reset)
    {
        Debug.Log("syncing with values " + o0 + " " + o1 + " " + o2 + " " + o3 + " " + time_since_reset);
        //if the organisms have changed since last time
        if ((int)org0 != o0 || (int)org1 != o1 || (int)org2 != o2 || (int)org3 != o3 )
        {
            //remove current organisms
            organismControllers[(int)org0].Remove();
            organismControllers[(int)org1].Remove();
            organismControllers[(int)org2].Remove();
            organismControllers[(int)org3].Remove();


            //change the organisms in-game 
            org0 = (Organism)o0;
            org1 = (Organism)o1;
            org2 = (Organism)o2;
            org3 = (Organism)o3;


            //reset the populations
            time = 0f;
            organismControllers[(int)org0].Reset();
            organismControllers[(int)org1].Reset();
            organismControllers[(int)org2].Reset();
            organismControllers[(int)org3].Reset();
        }

        //otherwise 
        else 
        {
            time = time_since_reset; 

            
            
        }
    }

    public void RotateHills()
    {
        foreach(Hill h in hills)
        {
            h.transform.Rotate(0.0f, 0.0f, h.spinSpeed * 500); 
        }
        //update grass cover ASAP 
        ((GrassController)organismControllers[9]).SpeedUpCover(); 
    }

    public void ResetAll()
    {
        foreach (OrganismController oc in organismControllers)
        {
            oc.Remove();
        }
    }
    public void Simulate()
    {
        //currently hard coded for timelapse video. 
        switch(hardCodeSelection)
        {
            //grass and flowers should multiply at around the same rate, insect 10x of that, and omnivore 0.25x of that
            //Everything should level off after a certain point and remain relatively static
            //flower 8, insect 4, grass 9, omnivore 2
            case 1:
                organismControllers[8].population = 10;
                organismControllers[4].population = 10;
                organismControllers[9].population = 20;
                organismControllers[2].population = 10;

                organismControllers[8].deadCount = 0;
                organismControllers[4].deadCount = 0;
                organismControllers[9].deadCount = 0;
                organismControllers[2].deadCount = 0;
                break;
            case 2:
                organismControllers[8].population = 20;
                organismControllers[4].population = 20;
                organismControllers[9].population = 40;
                organismControllers[2].population = 20;

                organismControllers[8].deadCount = 0;
                organismControllers[4].deadCount = 0;
                organismControllers[9].deadCount = 0;
                organismControllers[2].deadCount = 0;
                break;
            case 3:
                organismControllers[8].population = 40;
                organismControllers[4].population = 30;
                organismControllers[9].population = 80;
                organismControllers[2].population = 30;

                organismControllers[8].deadCount = 5;
                organismControllers[4].deadCount = 5;
                organismControllers[9].deadCount = 5;
                organismControllers[2].deadCount = 5;
                break;
            case 4:
                organismControllers[8].population = 120;
                organismControllers[4].population = 40;
                organismControllers[9].population = 100;
                organismControllers[2].population = 30;

                organismControllers[8].deadCount = 10;
                organismControllers[4].deadCount = 10;
                organismControllers[9].deadCount = 10;
                organismControllers[2].deadCount = 10;
                break;

            //For the second one, grass should multiply around the same as in the first animation at first, but then die off after 3-4 days. All the flowers and carnivores should be gone by day 2, the mushroom should survive 2-3 days.
            //flower 8, carnivore 0, grass 9, mushroom 5
            case 5:
                organismControllers[8].population = 20;
                organismControllers[0].population = 10;
                organismControllers[9].population = 20;
                organismControllers[5].population = 10;

                organismControllers[8].deadCount = 0;
                organismControllers[0].deadCount = 0;
                organismControllers[9].deadCount = 0;
                organismControllers[5].deadCount = 0;
                break;
            case 6:
                organismControllers[8].population = 0;
                organismControllers[0].population = 10;
                organismControllers[9].population = 40;
                organismControllers[5].population = 20;

                organismControllers[8].deadCount = 10;
                organismControllers[0].deadCount = 10;
                organismControllers[9].deadCount = 10;
                organismControllers[5].deadCount = 10;
                break;
            case 7:
                organismControllers[8].population = 10;
                organismControllers[0].population = 5;
                organismControllers[9].population = 20;
                organismControllers[5].population = 10;

                organismControllers[8].deadCount = 10;
                organismControllers[0].deadCount = 10;
                organismControllers[9].deadCount = 10;
                organismControllers[5].deadCount = 10;
                break;
            case 8:
                organismControllers[8].population = 0;
                organismControllers[0].population = 0;
                organismControllers[9].population = 0;
                organismControllers[5].population = 0;

                organismControllers[8].deadCount = 10;
                organismControllers[0].deadCount = 10;
                organismControllers[9].deadCount = 10;
                organismControllers[5].deadCount = 10;
                break;
            case 9:
                organismControllers[8].population = 0;
                organismControllers[0].population = 0;
                organismControllers[9].population = 0;
                organismControllers[5].population = 0;
                organismControllers[4].population = 0;
                organismControllers[2].population = 0;

                organismControllers[8].deadCount = 0;
                organismControllers[0].deadCount = 0;
                organismControllers[9].deadCount = 0;
                organismControllers[5].deadCount = 0;
                organismControllers[4].deadCount = 0;
                organismControllers[2].deadCount = 0;
                break;
        }



        //TODO update the population values. Needs Sebastians sim script. 


    }




    //can an organism be created that depends on an alive organism i? 
    public bool CanDependOn(int i)
    {
        //can always depend on nothing
        if (i == -1) return true;
        Debug.Log(organismControllers[i].population - organismControllers[i].dependedOnNum); 
        return organismControllers[i].population - organismControllers[i].dependedOnNum > 0; 
    }
    //can an organism be created that depends on an dead organism i? 
    public bool CanDependOnDead(int i)
    {
        //can always depend on nothing
        if (i == -1) return true;

        return organismControllers[i].deadCount - organismControllers[i].dependedOnDeadNum > 0;
    }

    public void DependOn(int i) 
    {
        if (i == -1) return;
        organismControllers[i].dependedOnNum++; 
    }

    public void DependOnDead(int i)
    {
        if (i == -1) return;
        organismControllers[i].dependedOnDeadNum++;
    }

    public void UndoDependOn(int i)
    {
        if (i == -1) return;
        organismControllers[i].dependedOnNum--;
    }

    public void UndoDependOnDead(int i)
    {
        if (i == -1) return;
        organismControllers[i].dependedOnDeadNum--;
    }

}
