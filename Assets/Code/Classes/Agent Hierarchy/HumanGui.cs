using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanGui
{
    public static GameObject humanGuiGameObject;

    private Human currentHuman;
    private GameManager.States currentPhase;
    private GameManager gameManager;

    private canvasScript canvas;
    private Tile currentSelectedTile;

    public const string ANIM_TRIGGER_FLASH_RED = "Flash Red";
    private const string humanGuiGameObjectPath = "Prefabs/GUI/Player GUI Canvas";

    public HumanGui()
    {
        humanGuiGameObject = (GameObject)Resources.Load(humanGuiGameObjectPath);

        if(humanGuiGameObject == null)
        {
            throw new System.ArgumentException("Could not find human GUI GameObject at the specified path.");
        }
    }

    public canvasScript GetCanvas()
    {
        return canvas;
    }

    public void DisplayGui(Human human, GameManager.States phase)
    {
        currentHuman = human;
        currentPhase = phase;

        canvas.ShowMarketButton();
        canvas.ShowRoboticonButton();
        
        ShowHelpBox();

        UpdateResourceBar(false);
        canvas.RefreshRoboticonList();
        canvas.EnableEndPhaseButton();
        canvas.RefreshTileInfoWindow();
        canvas.HideGamblingWindow();
        canvas.HideMarketWindow();
        canvas.HideAITurnText();

        //Added by JBT - enables or disables gambling, market and roboticon buttons depending on the phase, as the buttons are only used in the production and installation phase
        if (phase == GameManager.States.PURCHASE)
        {
            canvas.ShowGambleButton();
            canvas.ShowMarketButton();
            canvas.ShowRoboticonButton();
        }
        else if(phase == GameManager.States.INSTALLATION)
        {
            canvas.HideGambleButton();
            canvas.HideMarketButton();
            canvas.ShowRoboticonButton();
        }
        else
        {
            canvas.HideGambleButton();
            canvas.HideMarketButton();
            canvas.HideRoboticonButton();
        }

        canvas.SetCurrentPhaseText(GameManager.StateToPhaseName(phase) + " Phase");
    }

    //Added by JBT to display information about the current AI
    public void DisplayAIInfo(AI ai, GameManager.States phase)
    {
        currentHuman = null;
        currentPhase = phase;
        canvas.HideGamblingWindow();
        canvas.HideMarketWindow();
        canvas.HideRoboticonWindow();
        canvas.HideMarketButton();
        canvas.HideRoboticonButton();
        canvas.HideTileInfoWindow();
        canvas.SetAITurnText(ai.GetName() + " is thinking...");
        UpdateResourceBar(true);
    }

    public void SetCurrentPlayerName(string name)
    {
        canvas.SetCurrentPlayerName(name);
    }

    public void EndPhase()
    {
        gameManager.CurrentPlayerEndTurn();
    }

    public void DisableGui()
    {
        currentHuman = new Human(new ResourceGroup(), "", 0);
        UpdateResourceBar(false);    //This will reset all resource values to 0.
        canvas.HideRoboticonUpgradesWindow();

        canvas.DisableEndPhaseButton();
    }

    public void PurchaseTile(Tile tile)
    {
        try
        {
            currentHuman.AcquireTile(tile);
            UpdateResourceBar(false);
        }
        catch(System.InvalidOperationException)
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
                gameManager.market.BuyFrom(resourcesToBuy, roboticonsToBuy);
            }
            catch (System.ArgumentException e)
            {
                canvas.marketScript.PlayPurchaseDeclinedAnimation();
                return;
            }

            currentHuman.SetMoney(currentHuman.GetMoney() - buyPrice);

            for(int i = 0; i < roboticonsToBuy; i ++)
            {
                Roboticon newRoboticon = new Roboticon();
                currentHuman.AcquireRoboticon(newRoboticon);
                canvas.AddRoboticonToList(newRoboticon);
            }

            ResourceGroup currentResources = currentHuman.GetResources();
            currentHuman.SetResources(currentResources + resourcesToBuy);

            UpdateResourceBar(false);
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
                gameManager.market.SellTo(resourcesToSell);
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

            UpdateResourceBar(false);
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

    public void DisplayTileInfo(Tile tile)
    {
        currentSelectedTile = tile;     //Selection of a tile always passes through here
        canvas.ShowTileInfoWindow(tile);
    }

    public Tile GetCurrentSelectedTile()
    {
        return currentSelectedTile;
    }

    public void UpgradeRoboticon(Roboticon roboticon, ResourceGroup upgrades)
    {
        Player currentPlayer = GameHandler.GetGameManager().GetCurrentPlayer();
        int upgradeCost = (upgrades * Roboticon.UPGRADEVALUE).Sum();

        if (currentPlayer.GetMoney() >= upgradeCost)
        {
            currentPlayer.SetMoney(currentPlayer.GetMoney() - upgradeCost);
            roboticon.Upgrade(upgrades);
            UpdateResourceBar(false);
            canvas.ShowRoboticonUpgradesWindow(roboticon);
            canvas.RefreshTileInfoWindow();
        }
    }

    public void InstallRoboticon(Roboticon roboticon)
    {
        if (currentSelectedTile.GetOwner() == currentHuman)
        {
            //Added by JBT - only install a roboticon to a tile if the roboticon is not already installed to a tile
            if (!roboticon.IsInstalledToTile())
            {
                currentHuman.InstallRoboticon(roboticon, currentSelectedTile);
                canvas.RefreshTileInfoWindow();
            }
            else
            {
                throw new System.InvalidOperationException("Tried to install a roboticon which is already installed");
            }
        }
        else
        {
            throw new System.InvalidOperationException("Tried to install roboticon to tile which is not owned by the current player. This should not happen.");
        }
    }

    //Added by JBT to support the uninstallation of roboticons from tiles
    public void UninstallRoboticon(Roboticon roboticon)
    {
        if(roboticon.IsInstalledToTile())
        {
            currentHuman.UninstallRoboticon(roboticon, roboticon.InstalledTile);
            canvas.RefreshTileInfoWindow();
        }
        else
        {
            throw new System.InvalidOperationException("This roboticon is not installed on a tile. This should not happen");
        }
    }

    //Changed by JBT to show different values depending on if the current player is an AI
    private void UpdateResourceBar(bool aiTurn)
    {
        if (aiTurn)
        {
            canvas.SetUnknownResourceLabels();
            canvas.SetUnknownChangeLabels();
        }
        else
        {
            canvas.SetResourceLabels(currentHuman.GetResources(), currentHuman.GetMoney());
            canvas.SetResourceChangeLabels(currentHuman.CalculateTotalResourcesGenerated());
        }
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
