using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 forceVector;
    public float rotationSpeed;
    public float rotation;
    public float shootingTimer;

    public float flashingTimer;

    public float flashingTimerEnd;

    public bool flashing;

    public float lastFire;

    public int thrust;
    public float rotationBool;

    public bool thrustPressed;
    public bool rotatePressed;

    public Vector3 localScale;

    Global g;

    // Use this for initialization 
    void Start()
    {
        GameObject globalObject = GameObject.Find("GlobalObject");
        g = globalObject.GetComponent<Global>();

        rotation = -90.0f;
        thrust = 0;
        thrustPressed = false;

        flashingTimer = 0.0f;
        flashingTimerEnd = 200.0f;

        flashing = false;

        shootingTimer = 0.0f;
        lastFire = 0.0f;

        localScale = gameObject.transform.localScale;
    }

    /* forced changes to rigid body physics parameters should be done through the FixedUpdate() 
    method, not the Update() method*/
    void FixedUpdate()
    {
        shootingTimer += 1.0f;

        if (flashing)
        {
            flashingTimer += 1.0f;
        }

        // Vector3 default initializes all components to 0.0f     
        forceVector.z = 100.0f;

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


    void Flash()
    {
        gameObject.transform.localScale = localScale;
        if (flashingTimer < flashingTimerEnd)
        {
            if ((int)(flashingTimer / 10.0f) % 2 == 0)
            {
                gameObject.transform.localScale = localScale * 0.5f;
            }
        }
        else
        {
            flashing = false;
        }
    }


    public GameObject bullet; // the GameObject to spawn

    // Update is called once per frame 
    void Update ()
    {
        if (g.bonusActivated)
        {
            flashingTimerEnd = 500.0f;
            flashing = true;
        }

        if(flashing)
        {
            Flash();
        }
        else
        {
            gameObject.transform.localScale = localScale;
        }

        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)))
        {

            if (!g.bonusActivated)
            {
                gameObject.transform.localScale = localScale;

                if (shootingTimer > lastFire + 20.0f)
                {
                    lastFire = shootingTimer;

                    Debug.Log("Fire! " + rotation);

                    // We don't want to spawn a bullet inside our ship, so some
                    // simple trigonometry is done here to spawn the bullet at the
                    // tip of where the ship is pointed
                    Vector3 spawnPos = gameObject.transform.position;
                    spawnPos.y += 1.5f;

                    //instatiate the bullet
                    GameObject bul = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

                    // get the Bullet Script Component of the new Bullet instance
                    BulletScript b = bul.GetComponent<BulletScript>();

                    // set the direction the Bullet will travel in
                    Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
                    b.heading = rot;
                }
            }
            else
            {
                if (g.bonusTimer < 500.0f)
                {
                    flashing = true;

                    Debug.Log("Fire! " + rotation);

                    // We don't want to spawn a bullet inside our ship, so some
                    // simple trigonometry is done here to spawn the bullet at the
                    // tip of where the ship is pointed
                    Vector3 spawnPos = gameObject.transform.position;
                    spawnPos.y += 1.5f;

                    //instatiate the bullet
                    GameObject bul = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

                    // get the Bullet Script Component of the new Bullet instance
                    BulletScript b = bul.GetComponent<BulletScript>();

                    // set the direction the Bullet will travel in
                    Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
                    b.heading = rot;
                }
                else
                {
                    g.bonusActivated = false;
                    flashing = false;
                }
            }
        }
    }

    public GameObject deathExplosion;

    public AudioClip deathKnell;

    public void Die()
    {
        Debug.Log("Dying");

        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);

        Instantiate(deathExplosion, gameObject.transform.position, Quaternion.AngleAxis(-90, Vector3.right));

        if(flashing == false)
        {
            g.lives--;
            flashingTimer = 0.0f;
            flashingTimerEnd = 200.0f;
            flashing = true;
        }

        if (g.lives < 1)
        {
            // Destroy removes the gameObject from the scene and marks it for garbage collection
            Destroy(gameObject);

            Application.LoadLevel("GameOver");
        }
    }

}


