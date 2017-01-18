using UnityEngine;
using System.Collections;

public class GuiTextStore
{
    private static string[] helpBoxText = 
    {
        "Acquisition Phase.",
        "Purchase Phase.",
        "Installation Phase.",
        "Auction Phase.",
        "Production Phase."
    };

    public static string GetHelpBoxText(HumanGui.GamePhase phase)
    {
        return helpBoxText[(int)phase];
    }
}
