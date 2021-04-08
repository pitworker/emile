using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganismController : MonoBehaviour
{
    public int population;
    public int startingPopulation;
    public GameObject[] organismPrefabs;
    public GameObject[] deadOrganismPrefabs;

    private List<GameObject> organismList = new List<GameObject>();
    private List<GameObject> deadOrganismList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //make the num spawned match the population
        if(organismList.Count < population)
        {
            Hill hill = WorldController.Instance.hills[Random.Range(0, WorldController.Instance.hills.Length)]; 
            GameObject newOrganism = Instantiate(organismPrefabs[Random.Range(0, organismPrefabs.Length)], new Vector3(0, 0, 0), Quaternion.identity);
            organismList.Add(newOrganism);
            newOrganism.transform.parent = hill.transform;
            newOrganism.GetComponent<Organism>().hill = hill;
            newOrganism.GetComponent<Organism>().Randomize(); 

            //TO DO: spawn some dead guys if applicable 
        }

        else if(organismList.Count > population)
        {
            GameObject destroyOrganism = organismList[0];
            organismList.RemoveAt(0);
            Destroy(destroyOrganism); 
        }
    }

    public void reset()
    {
        population = startingPopulation;
    }
    public void remove()
    {
        population = 0;
    }


}
