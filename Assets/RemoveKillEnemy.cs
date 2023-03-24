using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class RemoveKillEnemy : MonoBehaviour
{
    public GameObject killEnemyCollider;
    public void DelelteKillEnemyCollider()
    {
        Destroy(killEnemyCollider);
    }
}
