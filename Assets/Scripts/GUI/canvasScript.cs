using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class canvasScript : MonoBehaviour
{
    private HumanGui humanGui;

    public helpBoxScript helpBox;
    public GameObject optionsMenu;
    public marketScript marketScript;

    #region Resource Labels
    public Text foodLabel;
    public Text foodChangeLabel;
    public Text energyLabel;
    public Text energyChangeLabel;
    public Text oreLabel;
    public Text oreChangeLabel;
    public Text moneyLabel;
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
            AI ai = new AI(new ResourceGroup(550, 500, 550));
            humanGui.DisplayGui(new Human(new ResourceGroup(999, 999, 999), 550), HumanGui.GamePhase.PRODUCTION);
            tempFireOnce = false;
        }
    }
   
    public void BuyFromMarket(ResourceGroup resources, int roboticonsToBuy, int price)
    {
        humanGui.BuyFromMarket(resources, roboticonsToBuy, price);
    }

    public void SellToMarket(ResourceGroup resources, int price)
    {
        humanGui.SellToMarket(resources, price);
    }

    public void ShowMarketWindow()
    {
        marketScript.gameObject.SetActive(true);
    }

    public void HideMarketWindow()
    {
        marketScript.gameObject.SetActive(false);
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

    public void SetResourceLabels(ResourceGroup resources, int money)
    {
        foodLabel.text = resources.food.ToString();
        energyLabel.text = resources.energy.ToString();
        oreLabel.text = resources.ore.ToString();
        moneyLabel.text = money.ToString();
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
