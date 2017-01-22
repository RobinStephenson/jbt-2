using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class roboticonWindowScript : MonoBehaviour
{
    public GameObject roboticonIconsList;   //Roboticon gui elements are added to this GUI content
    private GameObject roboticonTemplate;
    List<GameObject> currentlyDisplayedRoboticons = new List<GameObject>();

    private const string ROBOTICON_TEMPLATE_PATH = "Prefabs/GUI/TemplateRoboticon";

    private const int ROBOTICON_GUI_ELEMENT_HEIGHT = 30;
    private const int ROBOTICON_GUI_ELEMENT_PADDING = 5;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    /// <summary>
    /// Display a new set of roboticons to the GUI. Overwrite any previously displayed
    /// roboticons.
    /// </summary>
    /// <param name="roboticonsToDisplay"></param>
    public void DisplayRoboticonList(List<Roboticon> roboticonsToDisplay)
    {
        ClearRoboticonList();

        gameObject.SetActive(true);

        foreach (Roboticon roboticon in roboticonsToDisplay)
        {
            AddRoboticon(roboticon);
        }
    }

    public void HideRoboticonList()
    {
        currentlyDisplayedRoboticons = new List<GameObject>();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Add a roboticon to the displayed list of roboticons in the UI.
    /// Returns the gameobject representing the roboticon in the scene.
    /// </summary>
    /// <param name="roboticon"></param>
    /// <returns></returns>
    public void AddRoboticon(Roboticon roboticon)
    {
        LoadRoboticonTemplate();

        GameObject roboticonGuiObject = (GameObject)GameObject.Instantiate(roboticonTemplate);
        roboticonGuiObject.transform.SetParent(roboticonIconsList.transform, true);
        RectTransform guiObjectTransform = roboticonGuiObject.GetComponent<RectTransform>();
        int elementHeight = -currentlyDisplayedRoboticons.Count * (ROBOTICON_GUI_ELEMENT_HEIGHT + ROBOTICON_GUI_ELEMENT_PADDING);

        guiObjectTransform.offsetMax = new Vector2(-1, elementHeight);
        guiObjectTransform.offsetMin = new Vector2(1, elementHeight-ROBOTICON_GUI_ELEMENT_HEIGHT);
        guiObjectTransform.localScale = new Vector3(1, 1, 1);               //Undo Unity's instantiation meddling

        guiObjectTransform.GetComponent<roboticonGuiElementScript>().SetRoboticonName(roboticon.GetName());
        currentlyDisplayedRoboticons.Add(roboticonGuiObject);
    }

    /// <summary>
    /// Loads the roboticon template if not already loaded.
    /// </summary>
    private void LoadRoboticonTemplate()
    {
        if (roboticonTemplate == null)
        {
            roboticonTemplate = (GameObject)Resources.Load(ROBOTICON_TEMPLATE_PATH);

            if (roboticonTemplate == null)
            {
                throw new System.ArgumentException("Cannot find roboticon template at the specified path.");
            }
        }
    }

    private void ClearRoboticonList()
    {
        if (currentlyDisplayedRoboticons.Count > 0)
        {
            for (int i = currentlyDisplayedRoboticons.Count - 1; i >= 0; i--)
            {
                Destroy(currentlyDisplayedRoboticons[i]);
            }

            currentlyDisplayedRoboticons = new List<GameObject>();
        }
    }
}
 