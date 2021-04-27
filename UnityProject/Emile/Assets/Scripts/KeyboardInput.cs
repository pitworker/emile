using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
    public Text displayText;

    public string str;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            WorldController.WC.RotateHills();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            WorldController.WC.ResetAll();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WorldController.WC.hardCodeSelection = 1;
            WorldController.WC.Simulate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WorldController.WC.hardCodeSelection = 2;
            WorldController.WC.Simulate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WorldController.WC.hardCodeSelection = 3;
            WorldController.WC.Simulate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            WorldController.WC.hardCodeSelection = 4;
            WorldController.WC.Simulate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            WorldController.WC.hardCodeSelection = 5;
            WorldController.WC.Simulate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            WorldController.WC.hardCodeSelection = 6;
            WorldController.WC.Simulate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            WorldController.WC.hardCodeSelection = 7;
            WorldController.WC.Simulate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            WorldController.WC.hardCodeSelection = 8;
            WorldController.WC.Simulate();
        }

        //the correct code!
        //foreach (char c in Input.inputString)
        //{
        //    if (c == '\b') // has backspace/delete been pressed?
        //    {
        //        if (str.Length != 0)
        //        {
        //            str = str.Substring(0, str.Length - 1);
        //        }
        //    }
        //    else if ((c == '\n') || (c == '\r')) // enter/return
        //    {

        //        displayText.text = str;
        //        SendToWorldController();
        //    }
        //    else
        //    {
        //        str += c;
        //    }


        //}
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
            WorldController.WC.Sync(o0, o1, o2, o3, time);
        }

        str = "";



        //reset the string at the end
    }


}
