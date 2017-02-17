using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndGameGui
{

    private EndGameScript canvas;

    public EndGameGui(Player Winner)
    {
        canvas.SetWinnerText(Winner.GetName());
    }
}
