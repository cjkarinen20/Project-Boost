using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float turnSpeed = 100f;
    [SerializeField] float thrustForce = 100f;
    [SerializeField] float levelLoadDelay;

    [SerializeField] AudioClip[] audioClips;
    [SerializeField] ParticleSystem thrustEngine;
    [SerializeField] ParticleSystem death;
    [SerializeField] ParticleSystem win;


    Rigidbody rigidBody;
    AudioSource audioSource;

    double fuelGauge;

    enum State { Alive, Dying, Transcending}
    State state = State.Alive;
	

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        fuelGauge = 100.0;
        
	}
	
	

	void Update ()
    {
       
        ProcessInput();

        print(fuelGauge);
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Fuel":
               
                break;


            case "Finish":
                levelChangeState();
                break;

            default:
                deathState();
                break;
        }
    }

    private void deathState()
    {
        state = State.Dying;

        audioSource.PlayOneShot(audioClips[1]);
        death.Play();
        Invoke("ReloadCurrent", levelLoadDelay);
    }

    private void levelChangeState()
    {
        state = State.Transcending;

        audioSource.PlayOneShot(audioClips[2]);
        win.Play();
        Invoke("LoadNext", levelLoadDelay);
    }

    private void ReloadCurrent()
    {
        SceneManager.LoadScene("Level2"); 
    }

    private void LoadNext()
    {
        SceneManager.LoadScene("Level1");
    }

    private void ProcessInput()
    {   if (state == State.Alive)
        {
            thrust();
            rotation();
        }
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

            /*if (fuelGauge > 0)
            {}*/
                rigidBody.AddRelativeForce(Vector3.up * thrustForce);

                if (!audioSource.isPlaying)
                    audioSource.PlayOneShot(audioClips[0]);

            thrustEngine.Play();
        }
        else
        {
            audioSource.Stop();
            thrustEngine.Stop();
        }
    }
}
