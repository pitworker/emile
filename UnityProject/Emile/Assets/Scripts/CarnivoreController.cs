using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivoreController : MonoBehaviour
{
    public int population;
    private int num_spawned;
    public GameObject carnivorePrefab;

    public List<GameObject> carnivoreList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //TO DO: how do I get a position to display to? 
        //randomly choose from the 3 hills, 
        //
    }

    // Update is called once per frame
    void Update()
    {
        //make the num spawned match the population
        if(carnivoreList.Count < population)
        {
            Hill hill = WorldController.Instance.hills[Random.Range(0, WorldController.Instance.hills.Length)]; 
            GameObject newCarnivore = Instantiate(carnivorePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            carnivoreList.Add(newCarnivore);
            newCarnivore.transform.parent = hill.transform;
            newCarnivore.GetComponent<Carnivore>().hill = hill;

            newCarnivore.GetComponent<Carnivore>().Randomize(); 
        }

        else if(carnivoreList.Count > population)
        {
            GameObject destroyCarnivore = carnivoreList[0];
            carnivoreList.RemoveAt(0);
            Destroy(destroyCarnivore); 
        }
    }
}
