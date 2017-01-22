using UnityEngine;
using System.Collections;

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
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationQuat, Time.deltaTime * riseSpeed);
	}
}
