using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Drawing;
using UnityEngine.SocialPlatforms.Impl;

public class UIScoreCounter : MonoBehaviour
{
	public bool deathText = false;

	private Text myText;
	// Start is called before the first frame update
	void Awake()
	{
		myText = GetComponent<Text>();

	}

	string FormatTime(float time) {
		TimeSpan timeSpan = TimeSpan.FromSeconds(time);

		string scoreString = "";

		if (timeSpan.Days != 0) {
			scoreString = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
				timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		} else if (timeSpan.Hours != 0) {

			scoreString = string.Format("{0:D2}:{1:D2}:{2:D2}",
				timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		} else {
			// regular case
			scoreString = string.Format("{0:D2}:{1:D2}",
				timeSpan.Minutes, timeSpan.Seconds);
		}

		return scoreString;
	}
	void FixedUpdate() {
		

		if (deathText == true) {

			myText.text = "<size=30>Your score is:</size>\n"
				+ "<color=#0095FF>" + FormatTime(GameManager.timeElapsedWhileAlive) + "</color>"
				+ "<color=#aaaaaa><size=30>\nYour personal best this session is:</size>\n"
				+ FormatTime(GameManager.timeElapsedWhileAliveBest)
				+ "</color>\n";

				if (GameManager.gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.WebGL) {
					myText.text += "<color=#aaaaaa><size=25>Scores on Newgrounds.com</size></color>";
				} else if (GameManager.gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.VR_Android) {
					myText.text += "<color=#aaaaaa><size=25>Scores on Oculus home.</size></color>";
				}

		} else {
			myText.text = FormatTime(GameManager.timeElapsedWhileAlive);
		}
	}
}
