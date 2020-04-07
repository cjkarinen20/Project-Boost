using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;
    [SerializeField] float thrustForce = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    int life = 3;

	

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        
	}
	
	

	void Update ()
    {
       
        ProcessInput();
        print(life);
	}
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Safe");
                break;

            case "Fuel":
                print("Fuel");
                if (life < 3)
                {
                    life++;
                }
                break;

            default:
                life--;
                if (life <= 0)
                {
                    Destroy(gameObject);
                }
                break;


        }
    }
    private void ProcessInput()
    {
        thrust();
        rotation();

    }

    private void rotation()
    {

        rigidBody.freezeRotation = true;


        float rotationSpeed = turnSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidBody.freezeRotation = false;
    }

    private void thrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustForce);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
