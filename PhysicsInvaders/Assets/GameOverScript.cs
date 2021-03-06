
// Written by Nathan Devlin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls behavior of the GAME OVER screen

public class GameOverScript : MonoBehaviour
{

    private GUIStyle buttonStyle;

    public int finalScore;

    // Start is called before the first frame update
    void Start()
    {
        finalScore = -20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Controls the new game controls
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2.2f, Screen.height / 3.0f, 200, 200), "GAME OVER! ");

        GUILayout.BeginArea(new Rect(10, Screen.height / 2 + 100, Screen.width - 10, 200));

        // Load the main scene
        // The scene needs to be added into build setting to be loaded!
        if (GUILayout.Button("New Game"))
        {
            Application.LoadLevel("GameplayScene");
        }
        if(GUILayout.Button("Exit"))
        {
            Application.Quit();
            Debug.Log("Application.Quit() only works in build, not in editor");
        }

        GUILayout.EndArea();


        finalScore = PlayerPrefs.GetInt("score");
        string finalScoreString = finalScore.ToString();

        GUI.Label(new Rect(Screen.width / 2.2f, Screen.height / 3.0f + 100, 200, 200), "Your Score: " + finalScoreString);
        
    }

}
