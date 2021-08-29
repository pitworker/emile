using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism : MonoBehaviour
{
    private float theta = 0.0f;
    private float r = .8f;
    private int direction = 1;

    public bool walking; 

    private float offsetMultiplier = .1f;
    public float walkSpeed = 5.0f;
    public Hill hill;
    public float minRadius = 3.5f;
    public float maxRadius = 4.0f;
    public float minScale = 3.0f;
    public float maxScale = 6.0f;

    public int dependsOnOrganism = -1;
    public int dependsOnDeadOrganism = -1;

    public bool willBeDestroyed = false;
    public bool visible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if it should be destroyed and is offscreen, destroy
        if(willBeDestroyed && Offscreen())
        {
            Destroy(gameObject); 
        }
        //if walking, move around the hill 
        if(walking && hill)
        {
            theta = (theta + walkSpeed * Time.deltaTime * direction) % 360;
            UpdatePosition();
        }
        //if sprite is hidden, check if it is off-screen and can become visible again 
        if(!visible && Offscreen())
        {
            EnableRenderers(); 
        }

    }

    bool Offscreen()
    {
        if (!hill) return true; 
        if(hill.transform.eulerAngles.z < 180)
            return theta > 180 - hill.transform.eulerAngles.z && theta < 360 - hill.transform.eulerAngles.z;
        else
            return theta < 360 - hill.transform.eulerAngles.z || theta > 540  - hill.transform.eulerAngles.z;

    }


    public void Randomize()
    {
        //randomize its starting position  
        theta = Random.Range(0.0f, 360.0f);
        
        //randomize its height on the circle 
        r = Random.Range(minRadius, maxRadius);

        //randomize the direction its facing
        if (Random.value < 0.5) direction = -1;
        UpdatePosition();
        UpdateScale();

        //keep sprites hidden for now
        DisableRenderers(); 
    }

    public void UpdateScale()
    {
        transform.localScale = new Vector3(direction , 1.0f, 1.0f) * Random.Range(minScale, maxScale) / hill.transform.localScale.x;

        //Note: The value must be between -32768 and 32767. In-game z position is between 0 and 250 
        if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().sortingOrder = 32767 - (int) (transform.position.z / 200.0f * 65534.0f);
        foreach (Transform child in transform)
        {
            if(child.GetComponent<SpriteRenderer>() != null) child.GetComponent<SpriteRenderer>().sortingOrder = 32767 - (int)(transform.position.z / 200.0f * 65534.0f);
        }

    }
    public void UpdatePosition()
    {
        transform.localPosition = new Vector3( r * Mathf.Cos(theta * Mathf.Deg2Rad), r * Mathf.Sin(theta * Mathf.Deg2Rad), -hill.offset - (1.0f - (r - minRadius)/(maxRadius-minRadius)) * offsetMultiplier);
        transform.rotation = Quaternion.Euler(0, 0, theta - 90 + hill.transform.eulerAngles.z);

        //transform.rotation = Quaternion.Euler(0, 0, theta+90);

    }

    void DisableRenderers()
    {
        visible = false; 
        foreach (Transform child in transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null) child.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void EnableRenderers()
    {
        visible = true; 
        foreach (Transform child in transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null) child.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void FixedUpdate()
    {

    }
}
