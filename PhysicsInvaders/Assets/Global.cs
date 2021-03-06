
// Written by Nathan Devlin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global class to control elements that multiple objects may need to interact with

public class Global : MonoBehaviour
{
    public Vector3 originInScreenCoords;
    public int score;

    public int lives;


    public Camera firstPersonCamera;
    public Camera overheadCamera;

    public bool bonusActivated;

    public float bonusTimer;



    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;

        bonusActivated = false;
        bonusTimer = 0.0f;

        lives = 3;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.LoadLevel("TitleScreen");
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            SwitchView();
        }
    }

    // Updates regurlarly regardless of framerate
    void FixedUpdate()
    {
        if(bonusActivated)
        {
            bonusTimer += 1.0f;
        }
    }


    // Call this function to disable FPS camera,
    // and enable overhead camera.
    public void SwitchView()
    {
        if (firstPersonCamera.enabled == true)
        {
            firstPersonCamera.enabled = false;
            overheadCamera.enabled = true;
        }
        else
        {
            firstPersonCamera.enabled = true;
            overheadCamera.enabled = false;
        }
    }

}

