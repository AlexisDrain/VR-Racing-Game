using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class HammerAnimator : MonoBehaviour
{
    public bool hammerDownOnStart = false;
    public AudioClip hammerDownSFX;
    //public AudioClip hammerUpSFX;

    private bool hammerStateDown = false;
    private float isAnimating = 2f;

    private Animator myAnimator;
    private AudioSource myAudioSource;

    void Start() {
        myAnimator = transform.GetComponent<Animator>();
        myAudioSource = transform.GetComponent<AudioSource>();

        GameManagerChasm.resetEnemyCollisions.AddListener(ResetHammer);
        ResetHammer();
    }
    public void ResetHammer() {

        if (hammerDownOnStart) {
            myAnimator.SetTrigger("HammerDown");
            //myAudioSource.PlayWebGL(hammerDownSFX);
            hammerStateDown = true;
            isAnimating = 5f;
        } else {
            myAnimator.SetTrigger("HammerUp");
            //myAudioSource.PlayWebGL(hammerUpSFX);
            hammerStateDown = false;
            isAnimating = 5f;
        }
    }
    public void Update() {
        if (isAnimating > 0f) {
            isAnimating -= 0.1f;
        }
        if (GameManagerChasm.playerCol.GetComponent<PlayerControllerChasm>().canJumpCountdown >= 0.9f) { //  Input.GetButtonDown("Jump") || Input.GetButtonDown("JumpAlt")
            if (isAnimating > 0f) {
                return;
            }
            if (transform.parent.name == "Hammer (1)") {
                print(isAnimating);
            }
            if (hammerStateDown == true) {
                myAnimator.SetTrigger("HammerUp");
                hammerStateDown = false;
                isAnimating = 5f;
                if(GameManagerChasm.hardMode) {
                    isAnimating = 2.5f;
                }
            } else {
                myAnimator.SetTrigger("HammerDown");
                hammerStateDown = true;
                isAnimating = 5f;
                if (GameManagerChasm.hardMode) {
                    isAnimating = 2.5f;
                }
            }
            // special case for level 10: hammer sound only plays if close to hammers
            if (GameManagerChasm.currentLevel != 9) {
                myAudioSource.PlayWebGL(hammerDownSFX);

            } else if (GameManagerChasm.playerCol.transform.position.z > transform.position.z - 45f
                && GameManagerChasm.playerCol.transform.position.z < transform.position.z + 5f) {
                myAudioSource.PlayWebGL(hammerDownSFX);
            }
        }
    }
}
