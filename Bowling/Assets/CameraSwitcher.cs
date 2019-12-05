using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera freeCam;
    public Camera pinCam;
    public Camera playerCamera;

    private int clickCounter = 0;
    private bool duringSwitching = true;

    public void SwitchCamera(int x)
    {
        if (x == 0)
        {
            freeCam.enabled = true;
        }
        else if (x == 1)
        {
            playerCamera.enabled = true;
        }
        else if (x == 2)
        {
            pinCam.enabled = true;
        }
    }

    public void DisableAll()
    {
        freeCam.enabled = false;
        pinCam.enabled = false;
        playerCamera.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick1Button5) 
            || Input.GetKeyDown(KeyCode.Joystick1Button4)) && !duringSwitching)
        {
            duringSwitching = true;
            clickCounter++;
            DisableAll();
            SwitchCamera(clickCounter % 3);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.Joystick1Button5) || Input.GetKeyUp(KeyCode.Joystick1Button4))
        {
            duringSwitching = false;
        }
    }
}
