﻿//Game executable hosted by JBT at: http://robins.tech/jbt/documents/assthree/GameExecutable.zip

using UnityEngine;

/// <summary>
/// Forces a light game object to imitate a sun rise
/// </summary>
public class sunRiseScript : MonoBehaviour
{
    public Vector3 startRotation = new Vector3(280, 0, 0);
    public Vector3 targetRotation = new Vector3(40, 0, 0);
    public float riseSpeed = 1;

    private Quaternion targetRotationQuat;

	// Use this for initialization
	void Start ()
    {
        targetRotationQuat = Quaternion.Euler(targetRotation);
        transform.rotation = Quaternion.Euler(startRotation);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Linearly interpolate between current position and target position by a time-constant amount each frame.
        //Gives a smooth transition 
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationQuat, Time.deltaTime * riseSpeed);
	}
}
