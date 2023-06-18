using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class HammerAnimator : MonoBehaviour
{
    public bool hammerDownOnStart = false;

    private bool hammerStateDown = false;
    private float isAnimating = 2f;

    void Start() {

        GameManagerChasm.resetEnemyCollisions.AddListener(ResetHammer);
        ResetHammer();
    }
    public void ResetHammer() {
        if (hammerDownOnStart) {
            GetComponent<Animator>().SetTrigger("HammerDown");
            hammerStateDown = true;
            isAnimating = 2f;
        } else {
            GetComponent<Animator>().SetTrigger("HammerUp");
            hammerStateDown = false;
            isAnimating = 2f;
        }
    }
    public void Update() {

        if (isAnimating > 0f) {
            isAnimating -= 0.1f;
            return;
        }

        if (GameManagerChasm.playerCol.GetComponent<PlayerControllerChasm>().canJumpCountdown >= 0.9f) { //  Input.GetButtonDown("Jump") || Input.GetButtonDown("JumpAlt")
            if (hammerStateDown == true) {
                GetComponent<Animator>().SetTrigger("HammerUp");
                hammerStateDown = false;
                isAnimating = 2f;
            } else {
                GetComponent<Animator>().SetTrigger("HammerDown");
                hammerStateDown = true;
                isAnimating = 2f;
            }
        }
    }
}
