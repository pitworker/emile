using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : OrganismController
{
    public float currentCover = .5f;
    public float goalCover = .5f;

    public float fullPopulation = 50.0f;
    private float growSpeed = .001f; 
    public Renderer hill1;
    public Renderer hill2;
    public Renderer hill3;
    public Shader grassShader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        goalCover = base.population / fullPopulation;
        if (goalCover > 1.0f) goalCover = 1.0f; 

        if (currentCover < goalCover) currentCover += growSpeed * Time.deltaTime;
        else if (currentCover > goalCover) currentCover -= growSpeed * Time.deltaTime;

        base.Update(); 
        hill1.material.SetFloat("_Amt", currentCover);
        hill2.material.SetFloat("_Amt", currentCover);
        hill3.material.SetFloat("_Amt", currentCover);
    }

    public void SpeedUpCover()
    {

        if (currentCover < goalCover) currentCover += 3000 * growSpeed * Time.deltaTime;
        else if (currentCover > goalCover) currentCover -= 3000 * growSpeed * Time.deltaTime;

    }

}
