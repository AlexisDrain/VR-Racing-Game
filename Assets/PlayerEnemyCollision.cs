using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Alexis Clay Drain
 */

public class PlayerEnemyCollision : MonoBehaviour
{

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy")) {
            Destroy(col.gameObject);
            print("Collision with: " + col.gameObject.name);
        }
    }
}
