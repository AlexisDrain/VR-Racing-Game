using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alexis Clay Drain
 */

public class PlayerEnemyCollision : MonoBehaviour
{
	public bool killPlayer;
	public bool playWhooshSFX;
	public Vector2 sfxDeathPitchRange = new Vector2(0.6f, 1.5f);

	private bool canWooshAgain = true;

	void OnTriggerEnter(Collider col)
	{

		if(col.CompareTag("Hoop")) {
			GameManagerChasm.playerCol.GetComponent<PlayerControllerChasm>().PlayerInHoop();
		}

        if (col.CompareTag("Goal")) {
			print("TODO: increment unlocked levels incase its the most recent unlocked one");
			GameManagerChasm.unlockedLevels += 1;
			GameManagerChasm.NextLevelMenu();
        }
        if (col.CompareTag("Enemy")) {

			print("col with enemy");
			if (playWhooshSFX && canWooshAgain && GameManager.playerIsAlive
				&& (col.GetComponent<RemoveKillEnemy>() == null || killPlayer == false)) {
				// an object with <RemoveKillEnemy>() means that hitting it will remove the collider that kills the player

				canWooshAgain = false;
				StartCoroutine(WhooshDelay());

				GetComponent<AudioSource>().pitch = Random.Range(sfxDeathPitchRange.x, sfxDeathPitchRange.y);
				GetComponent<AudioSource>().Play();
			}
            if (GameManagerChasm.gameManagerChasmObj.GetComponent<GameManagerChasm>().currentGameScene == GameScene.chasm) {
                if (killPlayer && GameManagerChasm.playerIsAlive) {
                    if (col.GetComponent<RemoveKillEnemy>() != null) {
                        col.GetComponent<RemoveKillEnemy>().DelelteKillEnemyCollider();
                    } else {
                        GameManagerChasm.EndGame();
                    }
                }
            } else {
                if (killPlayer && GameManager.playerIsAlive) {
                    if (col.GetComponent<RemoveKillEnemy>() != null) {
                        col.GetComponent<RemoveKillEnemy>().DelelteKillEnemyCollider();
                    } else {
                        GameManager.EndGame();
                    }
                }
            }

		}
	}

	private IEnumerator WhooshDelay() {
		yield return new WaitForSeconds(0.1f);
		canWooshAgain = true;
	}
	/*
	void OnCollisionEnter(Collision col) {
		print(col);
		if (col.collider.CompareTag("Enemy")) {
			if (playWhooshSFX && canWooshAgain && GameManager.playerIsAlive) {
				canWooshAgain = false;
				StartCoroutine(WhooshDelay());

				GetComponent<AudioSource>().pitch = Random.Range(sfxPitchRange.x, sfxPitchRange.y);
				GetComponent<AudioSource>().Play();
			}
			if (killPlayer && GameManager.playerIsAlive) {
				print("Collision with: " + col.gameObject.name);
				GameManager.EndGame();
				//Destroy(col.gameObject);
			}
		}

	}
	*/
}
