using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    // Update is called once per frame
    public void StartGame()
    {
        GameManager.StartGame();
    }
	public void Resume() {
		if(GameManager.playerIsAlive == false) {
			GameManager.StartGame();
		} else {
            GameManager.ResumeGame();
        }

	}
}
