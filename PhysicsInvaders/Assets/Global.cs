using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public Vector3 originInScreenCoords;
    public int score;

    public Camera firstPersonCamera;
    public Camera overheadCamera;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;
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
