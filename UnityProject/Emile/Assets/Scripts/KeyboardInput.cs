using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
    public Text displayText;

    public string str;

    private WorldController wc; 

    // Start is called before the first frame update
    void Start()
    {
        wc = GetComponent<WorldController>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (str.Length != 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {

                displayText.text = str;
                SendToWorldController();
            }
            else
            {
                str += c;
            }


        }
    }

    void SendToWorldController()
    {
        //parse the string. example string: 0,1,2,3,20

        string[] split = str.Split(',');
        if(split.Length != 5)
        {
            Debug.Log("input should be of length 5 separated by commas. received input '" + str + "' instead.");
            str = ""; 
            return; 
        }

        int o0 = -2;
        int o1 = -2;
        int o2 = -2;
        int o3 = -2;
        int time = -2;
        bool success = int.TryParse(split[0], out o0);
        success = success && int.TryParse(split[1], out o1);
        success = success && int.TryParse(split[2], out o2);
        success = success && int.TryParse(split[3], out o3);
        success = success && int.TryParse(split[4], out time);


        if (!success) Debug.Log("could not parse '" + str + "' into integers");
        else if (o0 < -1 || o1 < -1 || o2 < -1 || o3 < -1 || o0 > 15 || o1 > 15 || o2 > 15 || o3 > 15) Debug.Log("integers in '" + str + "' are out of bounds.");

        else
        {
           // Debug.Log("parsed '" + str + "'.");
            wc.Sync(o0, o1, o2, o3, time);
        }

        str = "";



        //reset the string at the end
    }


}
