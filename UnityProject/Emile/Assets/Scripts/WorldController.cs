using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController WC { get; private set; }
    public Hill[] hills;

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
        
    }

    public void Sync(int o0, int o1, int o2, int o3, float time_since_reset)
    {
        Debug.Log("syncing with values " + o0 + " " + o1 + " " + o2 + " " + o3 + " " + time_since_reset);
        //if the organisms have changed since last time
        if ((int)org0 != o0 || (int)org1 != o1 || (int)org2 != o2 || (int)org3 != o3 )
        {
            //remove current organisms
            organismControllers[(int)org0].remove();
            organismControllers[(int)org1].remove();
            organismControllers[(int)org2].remove();
            organismControllers[(int)org3].remove();


            //change the organisms in-game 
            org0 = (Organism)o0;
            org1 = (Organism)o1;
            org2 = (Organism)o2;
            org3 = (Organism)o3;


            //reset the populations
            time = 0f;
            organismControllers[(int)org0].reset();
            organismControllers[(int)org1].reset();
            organismControllers[(int)org2].reset();
            organismControllers[(int)org3].reset();
        }

        //otherwise 
        else 
        {
            time = time_since_reset; 

            //TODO update the population values. Needs Sebastians sim script. 
            
            
        }
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
