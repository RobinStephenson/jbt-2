using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuDayNightCylce : MonoBehaviour {
    public Light sun;

    public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        sun.transform.Rotate(new Vector3(0.5f, 0.0f));
    }
}
