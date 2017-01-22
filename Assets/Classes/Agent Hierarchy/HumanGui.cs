using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanGui
{
    public static GameObject humanGuiGameObject;
    private const string humanGuiGameObjectPath = "Prefabs/GUI/Player GUI Canvas";

    private Human currentHuman;
    private GameManager.States currentPhase;
    private GameManager gameManager;

    private canvasScript canvas;

    public HumanGui()
    {
        humanGuiGameObject = (GameObject)Resources.Load(humanGuiGameObjectPath);

        if(humanGuiGameObject == null)
        {
            throw new System.ArgumentException("Could not find human GUI GameObject at the specified path.");
        }
    }

	public void DisplayGui(Human human, GameManager.States phase)
    {
        currentHuman = human;
        currentPhase = phase;

        ShowHelpBox();

        UpdateResourceBar();
        canvas.EnableEndPhaseButton();
    }

    public void EndPhase()
    {
        gameManager.CurrentPlayerEndTurn();
    }

    public void DisableGui()
    {
        currentHuman = new Human(new ResourceGroup(), "", 0);
        UpdateResourceBar();    //This will reset all resource values to 0.

        canvas.DisableEndPhaseButton();
    }

    public void PurchaseTile(Tile tile)
    {
        if(tile.GetPrice() < currentHuman.GetMoney())
        {
            currentHuman.SetMoney(currentHuman.GetMoney() - tile.GetPrice());
            currentHuman.AcquireTile(tile);
            UpdateResourceBar();
        }
        else
        {
            canvas.tileWindow.PlayPurchaseDeclinedAnimation();
        }
    }

    public void BuyFromMarket(ResourceGroup resourcesToBuy, int roboticonsToBuy, int buyPrice)
    {
        if(currentHuman.GetMoney() >= buyPrice)
        {
            try
            {
                gameManager.market.BuyFrom(resourcesToBuy, buyPrice);
            }
            catch (System.ArgumentException e)
            {
                //TODO - Implement separate animation for when the market does not have enough resources
                canvas.marketScript.PlayPurchaseDeclinedAnimation();
                return;
            }

            currentHuman.SetMoney(currentHuman.GetMoney() - buyPrice);

            for(int i = 0; i < roboticonsToBuy; i ++)
            {
                Roboticon newRoboticon = new Roboticon(new ResourceGroup());
                currentHuman.AcquireRoboticon(newRoboticon);
                canvas.AddRoboticonToList(newRoboticon);
            }

            ResourceGroup currentResources = currentHuman.GetResources();
            currentHuman.SetResources(currentResources + resourcesToBuy);

            UpdateResourceBar();
        }
        else
        {
            canvas.marketScript.PlayPurchaseDeclinedAnimation();
        }
    }

    public void SellToMarket(ResourceGroup resourcesToSell, int sellPrice)
    {
        ResourceGroup humanResources = currentHuman.GetResources();
        bool humanHasEnoughResources =
            humanResources.food   >= resourcesToSell.food &&
            humanResources.energy >= resourcesToSell.energy &&
            humanResources.ore    >= resourcesToSell.ore;

        if(humanHasEnoughResources)
        {
            try
            {
                gameManager.market.SellTo(resourcesToSell, sellPrice);
            }
            catch (System.ArgumentException e)
            {
                //TODO - Implement separate animation for when the market does not have enough resources
                canvas.marketScript.PlaySaleDeclinedAnimation();
                return;
            }

            currentHuman.SetMoney(currentHuman.GetMoney() + sellPrice);

            ResourceGroup currentResources = currentHuman.GetResources();
            currentHuman.SetResources(currentResources - resourcesToSell);

            UpdateResourceBar();
        }
        else
        {
            canvas.marketScript.PlaySaleDeclinedAnimation();
        }
    }

    public List<Roboticon> GetCurrentHumanRoboticonList()
    {
        return currentHuman.GetRoboticons();
    }

    public Human GetCurrentHuman()
    {
        return currentHuman;
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void SetCanvasScript(canvasScript canvas)
    {
        this.canvas = canvas;
    }

    private void UpdateResourceBar()
    {
        canvas.SetResourceLabels(currentHuman.GetResources(), currentHuman.GetMoney());
        canvas.SetResourceChangeLabels(currentHuman.CalculateTotalResourcesGenerated());
    }

    public void DisplayTileInfo(Tile tile)
    {
        canvas.ShowTileInfoWindow(tile);
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
