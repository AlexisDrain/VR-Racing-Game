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

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy")) {
            if (killPlayer) {
                print("Collision with: " + col.gameObject.name);
                GameManager.playerIsAlive = false;
                Destroy(col.gameObject);
            }
            if (playWhooshSFX) {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
