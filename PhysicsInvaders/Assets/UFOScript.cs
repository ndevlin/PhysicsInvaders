using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOScript : MonoBehaviour
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
        pointValue = 100;
        timer = 50;
        bulletTimer = 0.0f;

        forceVector.x = 50.0f;

    }


    public GameObject alienBullet; // the GameObject to spawn

    // Update is called once per frame
    void Update()
    {

        Vector3 currVec = new Vector3();

        if (!haveBeenHit)
        {
            timer++;

            currVec = ((float)Math.Sin((float)(timer) / 150.0)) * forceVector;

            float probabilityBound = 9950.0f - bulletTimer;

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

            g.bonusActivated = true;

        
            // Destroy removes the gameObject from the scene and marks it for garbage collection
            //Destroy(gameObject);

            haveBeenHit = true;

        }

        if (g.score > 640)
        {
            Application.LoadLevel("YouWon");
        }

    }
}
