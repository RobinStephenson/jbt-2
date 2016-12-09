using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class canvasScript : MonoBehaviour
{
    private HumanGui humanGui;

    public helpBoxScript helpBox;
    public GameObject optionsMenu;

    #region Resource Labels
    public Text foodLabel;
    public Text foodChangeLabel;
    public Text energyLabel;
    public Text energyChangeLabel;
    public Text oreLabel;
    public Text oreChangeLabel;
    #endregion

    bool tempFireOnce = true;

    // Use this for initialization
    void Start ()
    {
        humanGui = new HumanGui();
    }

    // Update is called once per frame
    void Update ()
    {
	    if(tempFireOnce)
        {
            humanGui.DisplayGui(new Human(new ResourceGroup(999, 999, 999)), HumanGui.GamePhase.PRODUCTION);
            tempFireOnce = false;
        }
    }

    public void ShowMarketWindow()
    {

    }

    public void HideMarketWindow()
    {

    }

    public void ShowOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void ShowHelpBox(string helpBoxText)
    {
        helpBox.ShowHelpBox(helpBoxText);
    }

    public void HideHelpBox()
    {
        helpBox.HideHelpBox();
    }

    public void SetResourceLabels(ResourceGroup resources)
    {
        foodLabel.text = resources.food.ToString();
        energyLabel.text = resources.energy.ToString();
        oreLabel.text = resources.ore.ToString();
    }

    public void SetResourceChangeLabels(ResourceGroup resources)
    {
        foodChangeLabel.text = FormatResourceChangeLabel(resources.food);
        energyChangeLabel.text = FormatResourceChangeLabel(resources.energy);
        oreChangeLabel.text = FormatResourceChangeLabel(resources.ore);
    }

    private string FormatResourceChangeLabel(int changeAmount)
    {
        string sign = (changeAmount >= 0) ? "+" : "";
        return "(" + sign + changeAmount.ToString() + ")";
    }
}
