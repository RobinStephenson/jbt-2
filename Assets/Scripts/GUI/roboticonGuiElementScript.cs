using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class roboticonGuiElementScript : MonoBehaviour
{
    public Text roboticonNameObject;

    public void SetRoboticonName(string name)
    {
        roboticonNameObject.text = name;
    }
}
