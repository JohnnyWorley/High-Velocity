using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject levelSelectScreen;
    public GameObject settingsScreen;
    public GameObject mainMenu;



    public void OpenLevelSelect()
    {
        levelSelectScreen.SetActive(true);
        mainMenu.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
        levelSelectScreen.SetActive(false);
        mainMenu.SetActive(false);
        PlayerSettings.instance.LoadData();
    }

    public void ExitMenu()
    {
        levelSelectScreen.SetActive(false);
        settingsScreen.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
