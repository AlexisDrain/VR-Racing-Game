using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIScoreCounter : MonoBehaviour
{
    public float timeElapsedWhileAlive;

    private Text myText;
    // Start is called before the first frame update
    void Awake()
    {
		myText = GetComponent<Text>();

	}

    // Update is called once per frame
    void FixedUpdate() {
        if (GameManager.playerIsAlive) {
            timeElapsedWhileAlive += Time.deltaTime;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsedWhileAlive);

        if (timeSpan.Days != 0) {
            myText.text = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
                timeSpan.Days,timeSpan.Hours,timeSpan.Minutes,timeSpan.Seconds);
        } else if (timeSpan.Hours != 0) {

			myText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
				timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		} else {
			// regular case
			myText.text = string.Format("{0:D2}:{1:D2}",
				timeSpan.Minutes, timeSpan.Seconds);
		}
       
	}
}
