using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    bool has_grass = false;
    bool has_carnivore = false;

   // gameObject grass;
   // gameObject carnivore; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrass()
    {
        has_grass = true;

    }

    void CreateCarnivore()
    {
        has_carnivore = true;
    }
    void RemoveGrass()
    {
        has_grass = false;

    }
    void RemoveCarnivore()
    {
        has_carnivore = false;
    }
}
