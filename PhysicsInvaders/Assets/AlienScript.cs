
// Written by Nathan Devlin

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Determines Alien behavior including speed, movement, and bullet shooting

public class AlienScript : MonoBehaviour
{

    public Vector3 forceVector;
    public int pointValue;
    public int timer;
    float bulletTimer;

    public bool haveBeenHit;

    public Collider coll;

    // Start is called before the first frame update
    void Start()
    {

        haveBeenHit = false;
        pointValue = 10;
        timer = 300;
        bulletTimer = 0.0f;

        forceVector.x = 2.0f;

    }


    public GameObject alienBullet; // the GameObject to spawn

    // Update is called once per frame
    void Update()
    {

        Vector3 currVec = new Vector3();

        if (!haveBeenHit)
        {
            timer++;

            currVec = ((float)Math.Sin((float)(timer) / 200.0)) * forceVector;

            float probabilityBound = 9995.0f - bulletTimer;

            if (UnityEngine.Random.Range(0.0f, 10000.0f) > probabilityBound)
            {
                Vector3 spawnPos = gameObject.transform.position;
                spawnPos.y -= 1.0f;

                //instatiate the bullet
                GameObject obj = Instantiate(alienBullet, spawnPos, Quaternion.identity) as GameObject;

                // get the Bullet Script Component of the new Bullet instance
                AlienBulletScript b = obj.GetComponent<AlienBulletScript>();

                // set the direction the Bullet will travel in
                Quaternion rot = Quaternion.Euler(new Vector3(0, 90.0f, 0));
                b.heading = rot;

                bulletTimer += 1.0f;

            }
        }      

        GetComponent<Rigidbody>().AddRelativeForce(currVec);

    }


    void fixedUpdate()
    {
    }


    public GameObject deathExplosion;

    public AudioClip deathKnell;

    // Upon death, create explosion effect, sound effect, increment score
    public void Die()
    {
        GameObject obj = GameObject.Find("GlobalObject");
        Global g = obj.GetComponent<Global>();

        if (!haveBeenHit)
        {
            Debug.Log("Dying");

            GetComponent<Rigidbody>().useGravity = true;

            AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);

            Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

            g.score += pointValue;

            PlayerPrefs.SetInt("score", g.score);

            haveBeenHit = true;
        }

        if (g.score > 640)
        {
            Application.LoadLevel("YouWon");
        }

    }
}
