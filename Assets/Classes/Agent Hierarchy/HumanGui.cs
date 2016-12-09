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
