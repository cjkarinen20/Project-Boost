using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessInput();
	}

    private void ProcessInput()
    {
        thrust();
        rotation();

    }

    private void rotation()
    {

        rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float rotationSpeed = 
            transform.Rotate(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward);
        }

        rigidBody.freezeRotation = false;
    }

    private void thrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
