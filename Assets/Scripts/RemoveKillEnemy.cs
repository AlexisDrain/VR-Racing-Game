using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class RemoveKillEnemy : MonoBehaviour
{
    public GameObject killEnemyCollider;

    public void Start() {
        GameManagerChasm.resetEnemyCollisions.AddListener(ResetEnemyCollision);
    }

    public void ResetEnemyCollision() {
        killEnemyCollider.GetComponent<BoxCollider>().enabled = true;
    }

    public void DelelteKillEnemyCollider()
    {
        if (GameManagerChasm.gameManagerChasmObj.GetComponent<GameManagerChasm>().currentGameScene == GameScene.chasm) {
            killEnemyCollider.GetComponent<BoxCollider>().enabled = false;
        } else {
            Destroy(killEnemyCollider);

        }
    }
}
