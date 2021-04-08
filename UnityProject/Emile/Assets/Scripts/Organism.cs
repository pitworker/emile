using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism : MonoBehaviour
{
    private float theta = 0.0f;
    private float r = .8f;
    private int direction = 1;

    public bool walking; 

    private float offsetMultiplier = .2f;
    public float walkSpeed = 5.0f;
    public Hill hill;
    public float minRadius = 3.5f;
    public float maxRadius = 4.0f;
    public float minScale = 3.0f;
    public float maxScale = 6.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if walking, move around the hill 
        if(walking && hill)
        {
            theta = theta + walkSpeed * Time.deltaTime * direction;
            UpdatePosition();
        }

    }

    public void Randomize()
    {
        //find an angle off the screen 
        theta = Random.Range(-hill.transform.eulerAngles.z, -hill.transform.eulerAngles.z - 180 );
        r = Random.Range(minRadius, maxRadius);
        if (Random.value < 0.5) direction = -1;
        UpdatePosition();
        UpdateScale(); 
    }

    public void UpdateScale()
    {
        transform.localScale = new Vector3(direction , 1.0f, 1.0f) * Random.Range(minScale, maxScale) / hill.transform.localScale.x;
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 5000 - (int) (10 * transform.position.z);
        foreach (Transform child in transform)
        {
            if(child.GetComponent<SpriteRenderer>() != null) child.GetComponent<SpriteRenderer>().sortingOrder = 5000 - (int)(10 * transform.position.z);
        }

    }
    public void UpdatePosition()
    {
        transform.localPosition = new Vector3( r * Mathf.Cos(theta * Mathf.Deg2Rad), r * Mathf.Sin(theta * Mathf.Deg2Rad), -hill.offset - (1.0f - (r - minRadius)/(maxRadius-minRadius)) * offsetMultiplier);
        transform.rotation = Quaternion.Euler(0, 0, theta - 90 + hill.transform.eulerAngles.z);

        //transform.rotation = Quaternion.Euler(0, 0, theta+90);

    }
    void FixedUpdate()
    {

    }
}
