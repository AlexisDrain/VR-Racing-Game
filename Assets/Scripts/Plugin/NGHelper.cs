using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class NGHelper : MonoBehaviour
{

    void Start()
    {
		var ngioOptions = new Dictionary<string, object>()
        {
            { "version",            "1.0.0" },
			{ "preloadMedals",      true },
	        { "preloadScoreBoards", true },
        };

        string appID = "56110:Lnuvw67a";
        string aesKey = "lt1FajqUmgZ7vJQkY1tMRw==";
		// initialize the API, using the App ID and AES key from your Newgrounds project
		NGIO.Init(appID, aesKey, ngioOptions);
    }
	void Update() {
		/** 
		 * Even though we call this on every frame, it will only trigger OnConnectionStatusChanged
		 * when there is an actual status change
		 **/
		StartCoroutine(NGIO.GetConnectionStatus(OnConnectionStatusChanged));
		StartCoroutine(NGIO.KeepSessionAlive());
	}
	public void OnConnectionStatusChanged(string status) {

		//if(status == NGIO.STATUS_LOGIN_REQUIRED) {
		//	NGIO.OpenLoginPage();
		//}

		if (status == NGIO.STATUS_READY) {
			print("user is logged in");
		}
	}
	/*
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
	*/
	public void UnlockMedalHexagon() {

        int medalID = 73231;

		StartCoroutine(NGIO.UnlockMedal(medalID));
		print("Medal Unlocked");
    }
    public void SubmitScores(int scoreValue) {
        int scoreboardID = 12650;
		StartCoroutine(NGIO.PostScore(scoreboardID, scoreValue, null));
		print("Submitted Points: " + scoreValue);

    }
}
