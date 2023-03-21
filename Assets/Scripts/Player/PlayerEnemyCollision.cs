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
    public Vector2 sfxPitchRange = new Vector2(0.6f, 1.5f);

    private bool canWooshAgain = true;

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy")) {
            if (killPlayer) {
                print("Collision with: " + col.gameObject.name);
                GameManager.EndGame();
                Destroy(col.gameObject);
            }
            if (playWhooshSFX && canWooshAgain) {
				canWooshAgain = false;
                StartCoroutine(WhooshDelay());


				GetComponent<AudioSource>().pitch = Random.Range(sfxPitchRange.x, sfxPitchRange.y);
                GetComponent<AudioSource>().Play();
                
            }
        }
    }

    private IEnumerator WhooshDelay() {

        yield return new WaitForSeconds(0.1f);
        canWooshAgain = true;

	}
}
