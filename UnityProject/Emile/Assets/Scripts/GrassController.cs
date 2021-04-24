using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : OrganismController
{
    public float cover = .5f;
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
        base.Update(); 
        hill1.material.SetFloat("_Amt", cover);
        hill2.material.SetFloat("_Amt", cover);
        hill3.material.SetFloat("_Amt", cover);
    }

}
