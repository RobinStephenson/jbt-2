using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class roboticonGuiElementScript : MonoBehaviour
{
    public Text roboticonNameObject;
    public GameObject installButton;
    public Text installedText; // JBT
    public GameObject upgradeButton;
    public Roboticon roboticon;
    private roboticonWindowScript roboticonWindow;

    public void SetRoboticon(Roboticon roboticon)
    {
        this.roboticon = roboticon;
        roboticonNameObject.text = roboticon.GetName();
    }

    public void SetButtonEventListeners(roboticonWindowScript roboticonWindow)
    {
        this.roboticonWindow = roboticonWindow;
        installButton.GetComponent<Button>().onClick.AddListener(OnInstallClick);
        upgradeButton.GetComponent<Button>().onClick.AddListener(OnUpgradeClick);
    }

    public void ShowInstallButton()
    {
        installButton.SetActive(true);
        upgradeButton.SetActive(false);
    }

    public void ShowUpgradeButton()
    {
        installButton.SetActive(false);
        upgradeButton.SetActive(true);
    }

    //Added by JBT to fix a bug where the install button would still be visiable when a roboticon was already installed
    public void ShowInstalledText()
    {
        installedText.gameObject.SetActive(true);
    }

    public void HideButtons()
    {
        installButton.SetActive(false);
        upgradeButton.SetActive(false);
        installedText.gameObject.SetActive(false);
    }

    public void OnInstallClick()
    {
        roboticonWindow.InstallRoboticon(roboticon);
    }

    public void OnRemoveClick()
    {
        roboticonWindow.UninstallRoboticon(roboticon);
    }

    public void OnUpgradeClick()
    {
        roboticonWindow.UpgradeRoboticon(roboticon);
    }
}
