
// Written by Nathan Devlin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script to control the UI element displaying the current score
public class ScoreUI : MonoBehaviour
{
    Global globalObj;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        scoreText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + globalObj.score.ToString();
    }
}
