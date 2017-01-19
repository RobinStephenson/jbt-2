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

    public void BuyFromMarket(ResourceGroup resourcesToBuy, int roboticonsToBuy, int buyPrice)
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

    public void SellToMarket(ResourceGroup resourcesToSell, int sellPrice)
    {
        //TODO Interface with market - market has finite money?
        ResourceGroup humanResources = currentHuman.GetResources();
        bool humanHasEnoughResources =
            humanResources.food   >= resourcesToSell.food &&
            humanResources.energy >= resourcesToSell.energy &&
            humanResources.ore    >= resourcesToSell.ore;

        if(humanHasEnoughResources)
        {
            currentHuman.SetMoney(currentHuman.GetMoney() + sellPrice);

            //TODO - Replace with overloaded ResourceGroup operations
            ResourceGroup currentResources = currentHuman.GetResources();
            ResourceGroup newResources = new ResourceGroup();

            newResources.food = currentResources.food - resourcesToSell.food;
            newResources.energy = currentResources.energy - resourcesToSell.energy;
            newResources.ore = currentResources.ore - resourcesToSell.ore;

            currentHuman.SetResources(newResources);
            //TODO - Call market SellFrom method.

            UpdateResourceBar();
        }
        else
        {
            canvas.marketScript.PlaySaleDeclinedAnimation();
        }
    }

    private void UpdateResourceBar()
    {
        canvas.SetResourceLabels(currentHuman.GetResources(), currentHuman.GetMoney());
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
