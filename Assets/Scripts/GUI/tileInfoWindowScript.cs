using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tileInfoWindowScript : MonoBehaviour
{
    public canvasScript uiCanvas;
    public GameObject acquireTileButton;
    public GameObject installRoboticonButton;
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

    public void Show(Tile tile, GameManager.States gamePhase)
    {
        currentTile = tile;
        UpdateResourceTexts();
        UpdateOwnerText(tile.GetOwner());

        switch (gamePhase)
        {
            case GameManager.States.ACQUISITION:
                installRoboticonButton.SetActive(false);

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

                if (tile.GetOwner() == uiCanvas.GetHumanGui().GetCurrentHuman())
                {
                    installRoboticonButton.SetActive(true);
                }
                else
                {
                    installRoboticonButton.SetActive(false);
                }
                break;

            default:
                installRoboticonButton.SetActive(false);
                acquireTileButton.SetActive(false);
                break;
        }

        gameObject.SetActive(true);
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
        //TODO
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
