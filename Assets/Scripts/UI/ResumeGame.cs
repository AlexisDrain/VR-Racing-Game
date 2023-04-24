using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    public bool chasm = false;
    // Update is called once per frame
    public void StartGame()
    {
        if(chasm) {
            GameManagerChasm.StartGame();
        } else {
            GameManager.StartGame();

        }
    }
	public void Resume() {
		if(GameManager.playerIsAlive == false) {
            if(chasm) {
                GameManagerChasm.StartGame();
            } else {
			    GameManager.StartGame();

            }
		} else {
            if (chasm) {
                GameManagerChasm.ResumeGame();
            } else {
                GameManager.ResumeGame();
            }
        }

	}
}
