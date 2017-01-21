using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tileInfoWindowScript : MonoBehaviour
{
    #region Resource Labels
    public Text foodBase;
    public Text energyBase;
    public Text oreBase;
    public Text foodTotal;
    public Text energyTotal;
    public Text oreTotal;
    #endregion

    public void Show(Tile tile)
    {
        ResourceGroup tileBaseResources = tile.GetBaseResourcesGenerated();
        ResourceGroup tileTotalResources = tile.GetTotalResourcesGenerated();

        foodBase.text = tileBaseResources.getFood().ToString();
        energyBase.text = tileBaseResources.getEnergy().ToString();
        oreBase.text = tileBaseResources.getOre().ToString();

        foodTotal.text = tileTotalResources.getFood().ToString();
        energyTotal.text = tileTotalResources.getEnergy().ToString();
        oreTotal.text = tileTotalResources.getOre().ToString();

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
