using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class canvasScript : MonoBehaviour
{
    private HumanGui humanGui;

    public helpBoxScript helpBox;
    public GameObject optionsMenu;
    public roboticonWindowScript roboticonList;
    public marketScript marketScript;
    public GameObject endPhaseButton;
    public tileInfoWindowScript tileWindow;

    #region Resource Labels
    public Text foodLabel;
    public Text foodChangeLabel;
    public Text energyLabel;
    public Text energyChangeLabel;
    public Text oreLabel;
    public Text oreChangeLabel;
    public Text moneyLabel;
    #endregion

    public void EndPhase()
    {
        humanGui.EndPhase();
    }

    public void DisableEndPhaseButton()
    {
        endPhaseButton.SetActive(false);
    }

    public void EnableEndPhaseButton()
    {
        endPhaseButton.SetActive(true);
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

    public void ShowTileInfoWindow(Tile tile)
    {
        tileWindow.Show(tile);
    }

    public void HideTileInfoWindow()
    {
        tileWindow.Hide();
    }

    public void ShowRoboticonList()
    {
        roboticonList.DisplayRoboticonList(humanGui.GetCurrentHumanRoboticonList());
    }

    /// <summary>
    /// Adds a roboticon to the roboticon display list if it is currently
    /// being displayed.
    /// </summary>
    public void AddRoboticonToList(Roboticon roboticon)
    {
        if (roboticonList.isActiveAndEnabled)
        {
            roboticonList.AddRoboticon(roboticon);
        }
    }

    public void HideRoboticonList()
    {
        roboticonList.HideRoboticonList();
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

    public void SetHumanGui(HumanGui gui)
    {
        humanGui = gui;
    }

    private string FormatResourceChangeLabel(int changeAmount)
    {
        string sign = (changeAmount >= 0) ? "+" : "";
        return "(" + sign + changeAmount.ToString() + ")";
    }
}
