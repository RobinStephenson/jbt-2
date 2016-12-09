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

    private GameObject canvas;
    private helpBoxScript helpBox;

    public HumanGui()
    {
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");
        helpBox = canvas.GetComponentInChildren<helpBoxScript>();
    }

	public void DisplayGui(Human human, GamePhase phase)
    {
        currentHuman = human;
        currentPhase = phase;

        ShowHelpBox();

        UpdateResourceBar();
    }

    public void DisplayMainMenu()
    {

    }

    private void UpdateResourceBar()
    {

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
        helpBox.ShowHelpBox("TEMPORARY TEST STRING テムプストリング！");
    }

    private void HideHelpBox()
    {
        helpBox.HideHelpBox();
    }
}
