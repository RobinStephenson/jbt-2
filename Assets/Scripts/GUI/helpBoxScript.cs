using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class helpBoxScript : MonoBehaviour
{

    private Animator helpBoxAnimator;
    private Text helpBoxText;

	// Use this for initialization
	void Start ()
    {
        helpBoxAnimator = GetComponent<Animator>();
        helpBoxText = GetComponentInChildren<Text>();
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowHelpBox(string text = "")
    {
        helpBoxText.text = text;
        helpBoxAnimator.SetBool("helpBoxVisible", true);
    }

    public void HideHelpBox()
    {
        helpBoxAnimator.SetBool("helpBoxVisible", false);
    }
}
