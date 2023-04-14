using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking.Types;

public class NGHelper : MonoBehaviour
{
	/*
	public io.newgrounds.core ngio_core;
	//string appID = "56110:Lnuvw67a";
	//string aesKey = "lt1FajqUmgZ7vJQkY1tMRw==";
	void Start() {
		ngio_core.onReady(() => {
			ngio_core.checkLogin((bool logged_in) => {
				if (logged_in) {
					onLoggedIn();
				}// else {
				 //   requestLogin();
				 //}
			});
		});
	}

	void onLoggedIn() {
		io.newgrounds.objects.user player = ngio_core.current_user;
	}
	void requestLogin() {
		ngio_core.requestLogin(onLoggedIn, onLoginFailed, onLoginCancelled);
	}
	void onLoginFailed() {
		io.newgrounds.objects.error error = ngio_core.login_error;
		print("NG io error: " + error);
	}
	void onLoginCancelled() {
		print("NG io login canceled");
	}

	public void UnlockMedalHexagon() {

		io.newgrounds.components.Medal.unlock medal_unlock = new io.newgrounds.components.Medal.unlock();
		medal_unlock.id = 73231;

		medal_unlock.callWith(ngio_core);
		print("Medal Unlocked");
	}
	public void SubmitScores(int scoreValue) {
		io.newgrounds.components.ScoreBoard.postScore submit_score = new io.newgrounds.components.ScoreBoard.postScore();
		submit_score.id = 12650;
		submit_score.value = scoreValue;
		submit_score.callWith(ngio_core);
		print("Submitted Points: " + scoreValue);

	}
	*/
}
