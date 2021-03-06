﻿//Game executable hosted by JBT at: http://robins.tech/jbt/documents/assthree/GameExecutable.zip

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI interaction for the tile info window
/// </summary>
public class tileInfoWindowScript : MonoBehaviour
{
    public canvasScript uiCanvas;
    public GameObject acquireTileButton;
    public Text priceText;
    public Text ownerText;

    #region Resource Labels
    public Text foodBase;
    public Text energyBase;
    public Text oreBase;
    public Text foodTotal;
    public Text energyTotal;
    public Text oreTotal;
    #endregion

    private Tile currentTile;

    public void Show(Tile tile)
    {
        currentTile = tile;
        UpdateResourceTexts();
        UpdateOwnerText(tile.GetOwner());
        UpdatePriceText(tile.GetPrice());
        uiCanvas.RefreshRoboticonList();

        GameManager.States gamePhase = GameHandler.GetGameManager().GetCurrentState();

        switch (gamePhase)
        {
            case GameManager.States.ACQUISITION:
                if (tile.GetOwner() == null)
                {
                    acquireTileButton.SetActive(true);
                }
                else
                {
                    acquireTileButton.SetActive(false);
                }
                break;

            case GameManager.States.INSTALLATION:
                acquireTileButton.SetActive(false);
                break;

            default:
                acquireTileButton.SetActive(false);
                break;
        }

        gameObject.SetActive(true);
    }

    public void Refresh()
    {
        if(currentTile != null)
        {
            uiCanvas.RefreshRoboticonList();
            Show(currentTile);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void AcquireTile()
    {
        if (currentTile != null)
        {
            uiCanvas.PurchaseTile(currentTile);
            UpdateOwnerText(currentTile.GetOwner());
        }
    }

    public void PlayPurchaseDeclinedAnimation()
    {
        priceText.GetComponent<Animator>().SetTrigger(HumanGui.ANIM_TRIGGER_FLASH_RED);
    }

    private void UpdateResourceTexts()
    {
        ResourceGroup tileBaseResources = currentTile.GetBaseResourcesGenerated();
        ResourceGroup tileTotalResources = currentTile.GetTotalResourcesGenerated();

        foodBase.text = tileBaseResources.getFood().ToString();
        energyBase.text = tileBaseResources.getEnergy().ToString();
        oreBase.text = tileBaseResources.getOre().ToString();

        foodTotal.text = tileTotalResources.getFood().ToString();
        energyTotal.text = tileTotalResources.getEnergy().ToString();
        oreTotal.text = tileTotalResources.getOre().ToString();
    }

    private void UpdatePriceText(int price)
    {
        priceText.text = "£" + price.ToString();
    }

    private void UpdateOwnerText(Player owner)
    {
        if (owner == null)
        {
            ownerText.text = "Unowned";
        }
        else if (owner == GameHandler.GetGameManager().GetCurrentPlayer())
        {
            ownerText.text = "You";
        }
        else
        { 
            ownerText.text = owner.GetName();
        }
    }
}
