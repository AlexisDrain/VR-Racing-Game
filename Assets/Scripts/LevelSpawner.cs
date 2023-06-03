using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class LevelSpawner : MonoBehaviour
{
    public int unlockedLevel = 0;
    public List<GameObject> levels = new List<GameObject>();

    void Start() {
        //SpawnLevel(unlockedLevel);
    }

    public void SpawnLevel(int level) {
        GameObject newLevel = levels[level];
        GameObject.Instantiate(newLevel, GameManagerChasm.worldTransform);

        GameManagerChasm.uiLevelCounter.text = newLevel.GetComponent<LevelValues>().levelTitle;
        GameManagerChasm.playerCol.GetComponent<PlayerControllerChasm>().forwardMaxSpeed = newLevel.GetComponent<LevelValues>().playerMaxForwardSpeed;
        GameManagerChasm.StartGame();
    }
}
