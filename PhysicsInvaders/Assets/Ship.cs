using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 forceVector;
    public float rotationSpeed;
    public float rotation;
    public float timer;

    public float lastFire;

    public int thrust;
    public float rotationBool;

    public bool thrustPressed;
    public bool rotatePressed;

    // Use this for initialization 
    void Start()
    {
        rotation = -90.0f;
        thrust = 0;
        thrustPressed = false;

        timer = 0.0f;
        lastFire = 0.0f;
    }

    /* forced changes to rigid body physics parameters should be done through the FixedUpdate() 
    method, not the Update() method*/
    void FixedUpdate()
    {
        timer += 1.0f;

        // Vector3 default initializes all components to 0.0f     
        forceVector.z = 3000000.0f;

        // force thruster     
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            thrust = 1;

        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            thrust = -1;
        }
        else
        {
            thrust = 0;
        }


        if(thrust != 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(thrust * forceVector);
        }

    }


    public GameObject bullet; // the GameObject to spawn

    // Update is called once per frame 
    void Update ()
    {
        if((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) && timer > lastFire + 20.0f)
        {
            lastFire = timer;

            Debug.Log("Fire! " + rotation);

            // We don't want to spawn a bullet inside our ship, so some
            // simple trigonometry is done here to spawn the bullet at the
            // tip of where the ship is pointed
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.x += 1.5f * Mathf.Cos(rotation * Mathf.PI / 180);
            spawnPos.z -= 1.5f * Mathf.Sin(rotation * Mathf.PI / 180);

            //instatiate the bullet
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

            // get the Bullet Script Component of the new Bullet instance
            BulletScript b = obj.GetComponent<BulletScript>();

            // set the direction the Bullet will travel in
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            b.heading = rot;
        }
    }

    public GameObject deathExplosion;

    public AudioClip deathKnell;

    public void Die()
    {
        Debug.Log("Dying");

        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);

        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

        // Destroy removes the gameObject from the scene and marks it for garbage collection
        Destroy(gameObject);

        Application.LoadLevel("GameOver");
    }


}


