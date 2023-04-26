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
        for (int i = 1; i < transform.childCount - 1; i++) { // only check levels from 1 till 11. Leave 0 and 11
            if (i > GameManagerChasm.unlockedLevels) {
                transform.GetChild(i).GetComponent<Button>().interactable = false;
            }

        }
    }
    // Update is called once per frame
    public void SpawnLevel(int levelOnButton)
    {
        GameManagerChasm.gameManagerChasmObj.GetComponent<LevelSpawner>().SpawnLevel(levelOnButton);
        // transform.parent.GetComponent<NavigateMenus>().CloseAllMenus();

	}
}
