
// Written by Nathan Devlin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls speed and other paramaters of bullets shot by Aliens

public class AlienBulletScript : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion heading;

    // Use this for initialization     
    void Start()
    {
        // travel straight in the X-axis         
        thrust.y = -1000.0f;

        // do not passively decelerate         
        GetComponent<Rigidbody>().drag = 0;

        // set the direction it will travel in        
        GetComponent<Rigidbody>().MoveRotation(heading);

        // apply thrust once, no need to apply it again since         
        // it will not decelerate         
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    // Update is called once per frame     
    void Update()
    {
        //Physics engine handles movement, empty for now.      
    }

    // Behavior upon collision with another object
    void OnCollisionEnter(Collision collision)
    {
        // Collidingobject we're interested in
        Collider collider = collision.collider;
        

        if(collider.CompareTag("Ship"))
        {
            Ship ship = collider.gameObject.GetComponent<Ship>();
            
            //Destroy this bullet which collided with the Ship
            Destroy(gameObject);

            //let the other object handle it's own death
            ship.Die();
        }
        else if (collider.CompareTag("Bullet"))
        {
            BulletScript bullet = collider.gameObject.GetComponent<BulletScript>();

            //let the other object handle it's own death
            bullet.Die();

            //Destroy this bullet which collided with the Alien
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Bollard"))
        {
            BollardScript bollard = collider.gameObject.GetComponent<BollardScript>();

            //let the other object handle it's own death
            bollard.Die();

            //Destroy this bullet which collided with the bollard
            Destroy(gameObject);
        }
        else
        {
            // If we collided with something else, print to the console
            // what the other thing was
            Debug.Log("Collided with " + collider.tag);
        }
    }

    // Destroy this bullet
    public void Die()
    {
        Debug.Log("Dying");

        // Destroy removes the gameObject from the scene and marks it for garbage collection
        Destroy(gameObject);
    }

}

