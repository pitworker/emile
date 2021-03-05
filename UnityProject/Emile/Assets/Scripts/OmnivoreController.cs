using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmnivoreController : MonoBehaviour
{
    public int population;
    private int num_spawned;
    public GameObject omnivorePrefab;

    public List<GameObject> omnivoreList = new List<GameObject>();

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
        if(omnivoreList.Count < population)
        {
            Hill hill = WorldController.Instance.hills[Random.Range(0, WorldController.Instance.hills.Length)]; 
            GameObject newOmnivore = Instantiate(omnivorePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            omnivoreList.Add(newOmnivore);
            newOmnivore.transform.parent = hill.transform;
            newOmnivore.GetComponent<Omnivore>().hill = hill;

            newOmnivore.GetComponent<Omnivore>().Randomize(); 
        }

        else if(omnivoreList.Count > population)
        {
            GameObject destroyOmnivore = omnivoreList[0];
            omnivoreList.RemoveAt(0);
            Destroy(destroyOmnivore); 
        }
    }
}
