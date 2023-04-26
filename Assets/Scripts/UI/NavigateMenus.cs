using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class NavigateMenus : MonoBehaviour
{

    public void OpenPauseMenu() {
        GameManagerChasm.pauseMenu.SetActive(true);
        GameManagerChasm.deathMenu.SetActive(false);
        GameManagerChasm.levelMenu.SetActive(false);
    }
    public void OpenDeathMenu() {
        GameManagerChasm.pauseMenu.SetActive(false);
        GameManagerChasm.deathMenu.SetActive(true);
        GameManagerChasm.levelMenu.SetActive(false);
    }
    public void OpenLevelMenu() {
        GameManagerChasm.pauseMenu.SetActive(false);
        GameManagerChasm.deathMenu.SetActive(false);
        GameManagerChasm.levelMenu.SetActive(true);
    }
    public void CloseAllMenus() {
        GameManagerChasm.pauseMenu.SetActive(false);
        GameManagerChasm.deathMenu.SetActive(false);
        GameManagerChasm.levelMenu.SetActive(false);
    }
}
