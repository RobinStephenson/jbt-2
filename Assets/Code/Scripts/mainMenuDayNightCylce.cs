using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created by JBT to rotate a direction light in the scene
public class mainMenuDayNightCylce : MonoBehaviour {
    public Light sun;
	
	// Update is called once per frame
	void Update () {
        sun.transform.Rotate(new Vector3(0.5f, 0.0f));
    }
}
