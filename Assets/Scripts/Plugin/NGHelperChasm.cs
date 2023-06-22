using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

public class NGHelperChasm : MonoBehaviour
{

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

    public void UnlockFewerThanTenMedal() {
        io.newgrounds.components.Medal.unlock medal_unlock = new io.newgrounds.components.Medal.unlock();
        medal_unlock.id = 74381;
        medal_unlock.callWith(ngio_core);
        print("Medal Unlocked Fewer Than 10 Deaths");
    }

	public void UnlockLevelMedal(int levelUnlock) {

        // https://www.newgrounds.com/projects/games/4808749/apitools/medals
        int id1 = 0; // original
        int id2 = 0; // extreme

		switch (levelUnlock) {
			case 0:
            id1 = 74382;
            id2 = 74387;
            break;
            case 1:
            id1 = 74383;
            id2 = 74394;
            break;
            case 2:
            id1 = 74384;
            id2 = 74395;
            break;
            case 3:
            id1 = 74388;
            id2 = 74396;
            break;
            case 4:
            id1 = 74385;
            id2 = 74397;
            break;
            case 5:
            id1 = 74389;
            id2 = 74398;
            break;
            case 6:
            id1 = 74390;
            id2 = 74399;
            break;
            case 7:
            id1 = 74391;
            id2 = 74400;
            break;
            case 8:
            id1 = 74392;
            id2 = 74401;
            break;
            case 9:
            id1 = 74393;
            id2 = 74402;
            break;
            case 10:
            id1 = 74386;
            id2 = 74403;
            break;
        }
		
		io.newgrounds.components.Medal.unlock medal_unlock = new io.newgrounds.components.Medal.unlock();
        medal_unlock.id = id1;
		medal_unlock.callWith(ngio_core);
		print("Medal Unlocked: " + levelUnlock);

		if (GameManagerChasm.hardMode == true) {
            io.newgrounds.components.Medal.unlock medal_unlock2 = new io.newgrounds.components.Medal.unlock();
            medal_unlock.id = id2;
            medal_unlock.callWith(ngio_core);
            print("Medal Unlocked HARD: " + levelUnlock);
        }

	}
	/*
	public void SubmitScores(int scoreValue) {
		io.newgrounds.components.ScoreBoard.postScore submit_score = new io.newgrounds.components.ScoreBoard.postScore();
		submit_score.id = 12650;
		submit_score.value = scoreValue;
		submit_score.callWith(ngio_core);
		print("Submitted Points: " + scoreValue);

	}
	*/
}
