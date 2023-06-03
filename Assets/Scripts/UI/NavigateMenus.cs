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
        GameManagerChasm.nextLevelMenu.SetActive(false);
        GameManagerChasm.endingMenu.SetActive(false);
        
    }
    public void OpenDeathMenu() {
        GameManagerChasm.pauseMenu.SetActive(false);
        GameManagerChasm.deathMenu.SetActive(true);
        GameManagerChasm.levelMenu.SetActive(false);
        GameManagerChasm.nextLevelMenu.SetActive(false);
        GameManagerChasm.endingMenu.SetActive(false);
    }
    public void OpenLevelMenu() {
        GameManagerChasm.pauseMenu.SetActive(false);
        GameManagerChasm.deathMenu.SetActive(false);
        GameManagerChasm.levelMenu.SetActive(true);
        GameManagerChasm.nextLevelMenu.SetActive(false);
        GameManagerChasm.endingMenu.SetActive(false);
    }
    public void OpenNextLevelMenu() {
        GameManagerChasm.pauseMenu.SetActive(false);
        GameManagerChasm.deathMenu.SetActive(false);
        GameManagerChasm.levelMenu.SetActive(false);
        GameManagerChasm.nextLevelMenu.SetActive(true);
        GameManagerChasm.endingMenu.SetActive(false);
    }
    public void OpenEndingMenu() {
        GameManagerChasm.pauseMenu.SetActive(false);
        GameManagerChasm.deathMenu.SetActive(false);
        GameManagerChasm.levelMenu.SetActive(false);
        GameManagerChasm.nextLevelMenu.SetActive(false);
        GameManagerChasm.endingMenu.SetActive(true);
    }
    public void CloseAllMenus() {
        GameManagerChasm.pauseMenu.SetActive(false);
        GameManagerChasm.deathMenu.SetActive(false);
        GameManagerChasm.levelMenu.SetActive(false);
        GameManagerChasm.nextLevelMenu.SetActive(false);
        GameManagerChasm.endingMenu.SetActive(false);
    }
}
