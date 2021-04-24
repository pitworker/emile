using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hill : MonoBehaviour
{
    private float scale;
    public float offset = .05f;
    public float spinSpeed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        //unity default plane is 10x10
        scale = transform.localScale.x * 5f;
        GetComponent<SpriteRenderer>().sortingOrder = 32767 - (int)(transform.position.z / 200.0f * 65534.0f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * (spinSpeed * Time.deltaTime));
    }

    //gets random point on top half of circle
    public Vector3 GetRandomPointOnSurface()
    {
        //TO DO: uniform distribution on a circle
        float r = scale * Mathf.Sqrt(Random.Range(0.0f, 1.0f));
        float theta = Random.Range(0.0f, 3.14f);

        float localX = Mathf.Cos(theta) * r;
        float localY = Mathf.Sin(theta) * r; 
        return new Vector3(localX + transform.position.x, localY + transform.position.y, transform.position.z - offset); 
    }
    public Vector3 GetRandomPointOnCurve()
    {
        return new Vector3(0, 0, 0); 
    }
}
