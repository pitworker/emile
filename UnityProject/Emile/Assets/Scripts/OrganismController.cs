using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganismController : MonoBehaviour
{
    public int population;
    public int deadCount;
    public int startingPopulation;
    public Organism[] organismPrefabs;
    public Organism[] deadOrganismPrefabs;

    public int dependedOnNum = 0;
    public int dependedOnDeadNum = 0;

    private List<Organism> organismList = new List<Organism>();
    private List<Organism> deadOrganismList = new List<Organism>();

    private float cleanDeadFrequency = 600.0f; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        updateLivingPrefabs();
        updateDeadPrefabs();

        //periodically delete dead prefabs 
        InvokeRepeating("CleanDeadCount", cleanDeadFrequency, cleanDeadFrequency);
    }

    //make the num spawned match the population
    void updateLivingPrefabs()
    {
        if (organismList.Count < population - dependedOnNum)
        {
            if (organismPrefabs.Length == 0) return;
            Organism baseOrganism = organismPrefabs[Random.Range(0, organismPrefabs.Length)];

            //only create the organism if its dependencies are there 
            if (WorldController.WC.CanDependOn(baseOrganism.dependsOnOrganism) && WorldController.WC.CanDependOnDead(baseOrganism.dependsOnDeadOrganism))
            {
                Hill hill = WorldController.WC.hills[Random.Range(0, WorldController.WC.hills.Length)];
                //update the dependedOnNum for the controllers this organism depends on 
                WorldController.WC.DependOn(baseOrganism.dependsOnOrganism);
                WorldController.WC.DependOnDead(baseOrganism.dependsOnDeadOrganism);

                GameObject go = Instantiate(baseOrganism.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                Organism newOrganism = go.GetComponent<Organism>();
                organismList.Add(newOrganism);
                newOrganism.transform.parent = hill.transform;
                newOrganism.GetComponent<Organism>().hill = hill;
                newOrganism.GetComponent<Organism>().Randomize();
            }
        }

        else if (organismList.Count > population - dependedOnNum && organismList.Count > 0)
        {
            Organism destroyOrganism = organismList[0];
            organismList.RemoveAt(0);

            //update the dependedOnNum for the controllers this organism depends on 
            WorldController.WC.UndoDependOn(destroyOrganism.dependsOnOrganism);
            WorldController.WC.UndoDependOnDead(destroyOrganism.dependsOnDeadOrganism);

            destroyOrganism.willBeDestroyed = true; 
        }
    }

    void updateDeadPrefabs()
    {
        if (deadOrganismList.Count < deadCount - dependedOnDeadNum)
        {
            if (deadOrganismPrefabs.Length == 0) return; 
            Organism baseOrganism = deadOrganismPrefabs[Random.Range(0, deadOrganismPrefabs.Length)];

            Hill hill = WorldController.WC.hills[Random.Range(0, WorldController.WC.hills.Length)];

            GameObject go = Instantiate(baseOrganism.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            Organism newOrganism = go.GetComponent<Organism>();
            deadOrganismList.Add(newOrganism);
            newOrganism.transform.parent = hill.transform;
            newOrganism.GetComponent<Organism>().hill = hill;
            newOrganism.GetComponent<Organism>().Randomize();
        }

        else if (deadOrganismList.Count > deadCount - dependedOnDeadNum && deadOrganismList.Count > 0)
        {
            Organism destroyOrganism = deadOrganismList[0];
            deadOrganismList.RemoveAt(0);

            destroyOrganism.willBeDestroyed = true;
        }
    }


    void CleanDeadCount()
    {
        if(deadCount > dependedOnDeadNum)
        {
            deadCount--; 
        }
    }

    public void Reset()
    {
        population = startingPopulation;
        deadCount = 0;
    }
    public void Remove()
    {
        population = 0;
        deadCount = 0;
    }

}
