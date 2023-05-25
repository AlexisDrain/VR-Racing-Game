using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class HammerAnimator : MonoBehaviour
{
    public bool hammerDownOnStart = false;

    public bool hammerStateDown = false;
    //public float delayAnim;
    private float isAnimating = 0f;

    void Start() {
        if(hammerDownOnStart) {
            GetComponent<Animator>().SetTrigger("HammerDown");
            hammerStateDown = true;
            isAnimating = 1f;
        }
    }

    public void Update() {

        if(isAnimating > 0f) {
            isAnimating -= 0.1f;
            return;
        }

        if(GameManagerChasm.playerCol.GetComponent<PlayerControllerChasm>().canJumpCountdown >= 0.9f) { //  Input.GetButtonDown("Jump") || Input.GetButtonDown("JumpAlt")
            if (hammerStateDown == true) {
                GetComponent<Animator>().SetTrigger("HammerUp");
                hammerStateDown = false;
                isAnimating = 1f;
            } else {
                GetComponent<Animator>().SetTrigger("HammerDown");
                hammerStateDown = true;
                isAnimating = 1f;
            }
        }
    }
}
