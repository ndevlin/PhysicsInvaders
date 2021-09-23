
// Written by Nathan Devlin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls behavior of bollards, primarily their death

public class BollardScript : MonoBehaviour
{
    // Use this for initialization     
    void Start()
    {
    }

    // Update is called once per frame     
    void Update()
    {
        //Physics engine handles movement, empty for now.      
    }

    // Behavior upon collision with another object
    void OnCollisionEnter(Collision collision)
    {
        // The collision contains a lot of info, but its the colliding
        // object we're interested in

        Collider collider = collision.collider;

        if (collider.CompareTag("Bullet"))
        {
            BulletScript bullet = collider.gameObject.GetComponent<BulletScript>();

            //let the other object handle it's own death
            bullet.Die();

            //Destroy this bollard which collided with the Bullet

            Destroy(gameObject);
        }
        else if (collider.CompareTag("AlienBullet"))
        {
            AlienBulletScript bullet= collider.gameObject.GetComponent<AlienBulletScript>();

            //let the other object handle it's own death
            bullet.Die();

            Destroy(gameObject);
        }
        else
        {
            // If we collided with something else, print to the console
            // what the other thing was
            Debug.Log("Collided with " + collider.tag);
        }
    }

    // Destroy the bollard
    public void Die()
    {
        Debug.Log("Dying");

        // Destroy removes the gameObject from the scene and marks it for garbage collection
        Destroy(gameObject);
    }

}

