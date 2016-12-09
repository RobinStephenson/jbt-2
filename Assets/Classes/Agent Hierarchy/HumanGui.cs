using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanGui
{
    public enum GamePhase
    {
        ACQUISITION,
        PURCHASE,
        INSTALLATION,
        AUCTION,
        PRODUCTION
    }

    private float guiScale = 1;
    private Human currentHuman;
    private GamePhase currentPhase;

    private canvasScript canvas;

    public HumanGui()
    {
        canvas = GameObject.FindGameObjectWithTag("uiCanvas").GetComponent<canvasScript>();
    }

	public void DisplayGui(Human human, GamePhase phase)
    {
        currentHuman = human;
        currentPhase = phase;

        ShowHelpBox();

        UpdateResourceBar();
    }

    public void BuyFromMarket(ResourceGroup resourcesToBuy, int buyPrice)
    {
        if(currentHuman.GetMoney() >= buyPrice)
        {
            currentHuman.SetMoney(currentHuman.GetMoney() - buyPrice);

            //TODO - Replace with overloaded ResourceGroup operations
            ResourceGroup currentResources = currentHuman.GetResources();
            ResourceGroup newResources = new ResourceGroup();

            newResources.food   = currentResources.food   + resourcesToBuy.food;
            newResources.energy = currentResources.energy + resourcesToBuy.energy;
            newResources.ore    = currentResources.ore    + resourcesToBuy.ore;

            currentHuman.SetResources(newResources);
            //TODO - Call market BuyFrom method.

            UpdateResourceBar();
        }
        else
        {
            canvas.marketScript.PlayPurchaseDeclinedAnimation();
        }
    }

    private void UpdateResourceBar()
    {
        canvas.SetResourceLabels(currentHuman.GetResources());
        canvas.SetResourceChangeLabels(currentHuman.CalculateTotalResourcesGenerated());
    }

    private void DisplayRoboticonList(List<Roboticon> roboticons)
    {

    }

    private void DisplayRoboticonInfo(Roboticon roboticon)
    {

    }

    private void DisplayTileInfo(Tile tile)
    {

    }

    private void ShowHelpBox()
    {
        canvas.ShowHelpBox(GuiTextStore.GetHelpBoxText(currentPhase));
    }

    private void HideHelpBox()
    {
        canvas.HideHelpBox();
    }
}
