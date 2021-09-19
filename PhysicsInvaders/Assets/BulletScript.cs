using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion heading;

    // Use this for initialization     
    void Start()
    {
        // travel straight in the X-axis         
        thrust.x = 1000.0f;

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

    
    void OnCollisionEnter(Collision collision)
    {
        // The collision contains a lot of info, but its the colliding
        // object we're interested in

        Collider collider = collision.collider;
        
        if(collider.CompareTag("Alien"))
        {
            AlienScript alien = collider.gameObject.GetComponent<AlienScript>();

            //let the other object handle it's own death
            alien.Die();

            //Destroy this bullet which collided with the Alien
            Destroy(gameObject);
        }
        else if (collider.CompareTag("AlienBullet"))
        {
            AlienBulletScript bullet= collider.gameObject.GetComponent<AlienBulletScript>();

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

            //Destroy this bullet which collided with the Alien
            Destroy(gameObject);
        }
        else
        {
            // If we collided with something else, print to the console
            // what the other thing was
            Debug.Log("Collided with " + collider.tag);
        }
    }

    public void Die()
    {
        Debug.Log("Dying");

        // Destroy removes the gameObject from the scene and marks it for garbage collection
        Destroy(gameObject);
    }

}

