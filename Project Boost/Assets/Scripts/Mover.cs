using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Mover : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    // todo remove from inspector
    [Range(0,1)][SerializeField] float movementFactor;

    Vector3 startPos;

	void Start ()
    {
        startPos = transform.position;
	}
	
	void Update ()
    {
        if(period <= Mathf.Epsilon) { return;  }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2; // 6.28
        float rawSinWave = Mathf.Sin(cycles);

        movementFactor = rawSinWave / 2f * 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startPos + offset;
	}
}
