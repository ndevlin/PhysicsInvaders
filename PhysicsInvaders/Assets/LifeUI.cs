using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    Global globalObj;
    Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("GlobalObject");
        globalObj = g.GetComponent<Global>();
        lifeText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = "Lives: " + globalObj.lives.ToString();
    }
}
