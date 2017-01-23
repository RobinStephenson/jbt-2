using UnityEngine;
using System.Collections;

public class unitTestManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        MapTests mapTests = new MapTests();
        string testResult = mapTests.TestMap();
        MonoBehaviour.print("TEST:" + testResult);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
