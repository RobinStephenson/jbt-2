using UnityEngine;
using System.Collections;

public class humanGuiTestScript : MonoBehaviour
{
    private HumanGui humanGui;
    private bool testOnce = true;

	// Use this for initialization
	void Start ()
    {
        humanGui = new HumanGui();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(testOnce)
        {
            humanGui.DisplayGui(new Human(), HumanGui.GamePhase.ACQUISITION);
        }
    }
}
