using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class OculusAPIHelper : MonoBehaviour
{
    public bool chasm = false;
    void Start()
    {
        if (chasm) {
            Oculus.Platform.Core.Initialize("9628178733890694");
        } else {
		    Oculus.Platform.Core.Initialize("9279204032120254");
        }
	}

    public void UnlockFewerThanTenMedal() {
        Oculus.Platform.Achievements.Unlock("FinalMedal");
        print("Medal Unlocked Fewer Than 10 Deaths");
    }

    public void UnlockLevelMedal(int levelUnlock) {

        // https://www.newgrounds.com/projects/games/4808749/apitools/medals
        string id1 = ""; // original
        string id2 = ""; // extreme

        switch (levelUnlock) {
            case 0:
            id1 = "Original1";
            id2 = "Hard1";
            break;
            case 1:
            id1 = "Original2";
            id2 = "Hard2";
            break;
            case 2:
            id1 = "Original3";
            id2 = "Hard3";
            break;
            case 3:
            id1 = "Original4";
            id2 = "Hard4";
            break;
            case 4:
            id1 = "Original5";
            id2 = "Hard5";
            break;
            case 5:
            id1 = "Original6";
            id2 = "Hard6";
            break;
            case 6:
            id1 = "Original7";
            id2 = "Hard7";
            break;
            case 7:
            id1 = "Original8";
            id2 = "Hard8";
            break;
            case 8:
            id1 = "Original9";
            id2 = "Hard9";
            break;
            case 9:
            id1 = "Original10";
            id2 = "Hard10";
            break;
            case 10:
            id1 = "Original11";
            id2 = "Hard11";
            break;
        }

        Oculus.Platform.Achievements.Unlock(id1);
        print("Medal Unlocked: " + levelUnlock);

        if (GameManagerChasm.hardMode == true) {
            Oculus.Platform.Achievements.Unlock(id2);
            print("Medal Unlocked HARD: " + levelUnlock);
        }

    }
}
