﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanGui
{
    public static GameObject humanGuiGameObject;
    private const string humanGuiGameObjectPath = "Prefabs/Player GUI Canvas";

    private Human currentHuman;
    private GameManager.States currentPhase;
    private GameManager gameManager;

    private canvasScript canvas;

    public HumanGui()
    {
        humanGuiGameObject = (GameObject)Resources.Load(humanGuiGameObjectPath);
    }

	public void DisplayGui(Human human, GameManager.States phase)
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
                currentHuman.AcquireRoboticon(new Roboticon(new ResourceGroup()));
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
