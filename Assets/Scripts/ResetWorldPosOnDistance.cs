using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWorldPosOnDistance : MonoBehaviour
{
    public float resetDistance = 75f;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.z >= resetDistance) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            if (GameManagerChasm.gameManagerChasmObj.GetComponent<GameManagerChasm>().currentGameScene == GameScene.chasm) {
                GameManagerChasm.gameManagerChasmObj.GetComponent<GameManagerChasm>().ResetWorldPos();
            } else {
                GameManager.gameManagerObj.GetComponent<GameManager>().ResetWorldPos();
            }
        }
    }
}
