using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("space"))
            while (!Input.GetKeyUp("up") || !Input.GetKeyUp("space"))
            {
                rigidBody.AddRelativeForce(Vector3.up);
            }
                

        else if (Input.GetKeyDown("down"))
            print("Down");
        else if (Input.GetKeyDown("left"))
            print("Left");
        else if (Input.GetKeyDown("right"))
            print("Right");
    }
}
