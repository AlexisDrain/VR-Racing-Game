using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Drawing;

public class UIScoreCounter : MonoBehaviour
{
    public bool deathText = false;

    private Text myText;
    // Start is called before the first frame update
    void Awake()
    {
		myText = GetComponent<Text>();

	}

    // Update is called once per frame
    void FixedUpdate() {
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.timeElapsedWhileAlive);

		string scoreString = "";

        if (timeSpan.Days != 0) {
			scoreString = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
                timeSpan.Days,timeSpan.Hours,timeSpan.Minutes,timeSpan.Seconds);
        } else if (timeSpan.Hours != 0) {

			scoreString = string.Format("{0:D2}:{1:D2}:{2:D2}",
				timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		} else {
			// regular case
			scoreString = string.Format("{0:D2}:{1:D2}",
				timeSpan.Minutes, timeSpan.Seconds);
		}

		if (deathText == true) {
			myText.text = "Your score is:\n<color=#0095FF>" + scoreString + "</color>";
		} else {
			myText.text = scoreString;
		}
	}
}
