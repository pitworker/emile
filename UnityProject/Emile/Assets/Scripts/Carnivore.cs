using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carnivore : MonoBehaviour
{
    private float theta = 0.0f;
    private float r = .8f;
    private int direction = 1;
    private float scale = 3.0f; 

    private float offsetMultiplier = 10.0f;
    private float walkSpeed = 5.0f;
    public Hill hill;
    private int num_anims = 3;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if walking, move around the hill 
        if(GetComponent<Animator>().GetInteger("state") == 1)
        {
            theta = theta + walkSpeed * Time.deltaTime * direction;
            UpdatePosition();
        }

    }

    public void Randomize()
    {
        GetComponent<Animator>().SetInteger("state", Random.Range(0, num_anims));
        theta = Random.Range(0.0f, 360.0f);
        r = Random.Range(0.55f, 0.95f);
        if (Random.value < 0.5) direction = -1;
        UpdatePosition();
        UpdateScale(); 
    }

    public void UpdateScale()
    {
        transform.localScale = new Vector3(direction , 1.0f, 1.0f) * scale / hill.transform.localScale.x;
      //  GetComponent<SpriteRenderer>().sortingOrder = 100 - (int)transform.localPosition.z;

    }
    public void UpdatePosition()
    {
        transform.localPosition = new Vector3( r * Mathf.Cos(theta * Mathf.Deg2Rad), r * Mathf.Sin(theta * Mathf.Deg2Rad), -hill.offset - (1.0f - r) * offsetMultiplier);
        transform.rotation = Quaternion.Euler(0, 0, theta - 90 + hill.transform.eulerAngles.z);

        //transform.rotation = Quaternion.Euler(0, 0, theta+90);

    }
    void FixedUpdate()
    {

    }
}
