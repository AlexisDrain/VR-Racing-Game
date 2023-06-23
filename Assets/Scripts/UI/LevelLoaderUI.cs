using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoaderUI : MonoBehaviour
{

    public void OnEnable() {
        CheckUnlockedLevelButtons();
    }

    public void CheckUnlockedLevelButtons() {
        for (int i = 1; i < transform.childCount; i++) { // only check levels from 1 till 11. Leave 0

            if (i > GameManagerChasm.unlockedLevels) {
                transform.GetChild(i).GetComponent<Button>().interactable = false;
            } else {
                transform.GetChild(i).GetComponent<Button>().interactable = true;
            }

        }
    }

    public void SpawnLevel(int levelOnButton)
    {
        // medal management
        if (levelOnButton == 0) {
            GameManagerChasm.startedGameFromLevelOne = true;
        } else {
            GameManagerChasm.startedGameFromLevelOne = false;
        }
        GameManagerChasm.currentDeaths = 0;
        GameManagerChasm.uiDeathCounter.text = GameManagerChasm.currentDeaths.ToString();

        // actual level spawning
        GameManagerChasm.gameManagerChasmObj.GetComponent<LevelSpawner>().SpawnLevel(levelOnButton);

	}
}
