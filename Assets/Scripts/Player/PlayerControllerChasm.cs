using Cinemachine;
using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerChasm : MonoBehaviour {

	public float forwardMoveSpeed = 5f;
	public float forwardMaxSpeed = 50f;
	public float forwardDrag = 0.9f;
	public float horizontalMoveSpeed = 50f;
	public float horizontalMaxSpeed = 10f;
	public float horizontalDrag = 0.98f;

	public float jumpPower = 10f;
	public float gravityMultiplier = 1.1f;
	private bool onGround = false;
	private bool inAir = true;
	[HideInInspector]
	public float canJumpCountdown = 0f; // for the hammer enemy

    public Vector3 hoopPower = new Vector3(0f, 10f, 10f);
	private float unlimittedForwardSpeedCountdown = 0f;

	private bool vr = false;
	private Rigidbody myRigidbody;
    private CinemachineVirtualCamera tps;
    private CinemachineVirtualCamera fps;

    [Header("SFX")]
	public AudioClip jumpAudioClip;
	public Vector2 jumpPitch = new Vector2(0.8f, 1.2f);
	public AudioClip landAudioClip;
	public Vector2 landPitch = new Vector2(0.8f, 1.2f);
	public AudioClip dieAudioClip;
	public Vector2 diePitch = new Vector2(0.8f, 1.2f);
	public AudioClip hoopAudioClip;
	public Vector2 hoopPitch = new Vector2(0.8f, 1.2f);

	private AudioSource myAudioSource;
	// Start is called before the first frame update
	void Start()
	{
		myRigidbody = GetComponent<Rigidbody>();
		myAudioSource = GetComponent<AudioSource>();
        
		if (GameManagerChasm.gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android) {
			vr = true;
		} else {
			vr = false;
            fps = GameManagerChasm.playerXRig.transform.Find("PlayerCol/CM vcam2-FPS").GetComponent<CinemachineVirtualCamera>();
            tps = GameManagerChasm.playerXRig.transform.Find("PlayerCol/CM vcam1-3PS").GetComponent<CinemachineVirtualCamera>();
        }
    }

	private void Update() {
		if ((Input.GetButton("Jump") || Input.GetButton("JumpAlt")) && onGround == true && canJumpCountdown <= 0f) {
			if (vr == true) {
				//transform.position = new Vector3(transform.position.x, GameManagerChasm.mainCameraObj.transform.position.y, transform.position.z);
			}

			myRigidbody.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Impulse);
			canJumpCountdown = 1f;
            inAir = true;

            myAudioSource.pitch = Random.Range(jumpPitch.x, jumpPitch.y);
			myAudioSource.PlayWebGL(jumpAudioClip);
		}
		if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("JumpAlt")) && myRigidbody.velocity.y > 0f) {
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y / 2, myRigidbody.velocity.z);
		}

	}

	public void PlayerInHoop() {
		unlimittedForwardSpeedCountdown = 1f;
		myRigidbody.AddForce(hoopPower, ForceMode.Impulse);
		GameManagerChasm.SpawnLoudAudio(hoopAudioClip, new Vector2(hoopPitch.x, hoopPitch.y));
	}

	// Update is called once per frame
	void FixedUpdate() {
		RaycastHit hit = new RaycastHit();

		if (vr == false) {
			// check right side
			onGround = Physics.Linecast(transform.position + new Vector3(0.5f, 0f), transform.position + new Vector3(0.5f, 0f) + Vector3.down, out hit, (1 << GameManagerChasm.layerWorld));
			// check left side
			if (hit.transform == null) {
				onGround = Physics.Linecast(transform.position + new Vector3(-0.5f, 0f), transform.position + new Vector3(-0.5f, 0f) + Vector3.down, out hit, (1 << GameManagerChasm.layerWorld));
			}
		} else {
			if (canJumpCountdown <= 0.5f) {  // sometimes the player gets stuck when too close to the ground in VR, so dont check ground on jump
                // check right side
                onGround = Physics.Linecast(GameManagerChasm.mainCameraObj.transform.position + new Vector3(0.5f, 0f), GameManagerChasm.mainCameraObj.transform.position + new Vector3(0.5f, 0f) + Vector3.down, out hit, (1 << GameManagerChasm.layerWorld));
				// check left side
				if (hit.transform == null) {
					onGround = Physics.Linecast(GameManagerChasm.mainCameraObj.transform.position + new Vector3(-0.5f, 0f), GameManagerChasm.mainCameraObj.transform.position + new Vector3(-0.5f, 0f) + Vector3.down, out hit, (1 << GameManagerChasm.layerWorld));
				}
			} else {
				onGround = false;
			}
        }


		if(inAir && onGround && canJumpCountdown <= 0f) {
			inAir = false;
			myAudioSource.pitch = Random.Range(landPitch.x, landPitch.y);
			myAudioSource.PlayWebGL(landAudioClip);
		}

		if(onGround == true) {
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0f, myRigidbody.velocity.z);
			if (vr == false) {
				transform.position = new Vector3(transform.position.x, hit.point.y + 1f, transform.position.z);
			} else {
				if(transform.position.y < 1f + hit.point.y) {
                    myRigidbody.AddForce(Vector3.up * 20f, ForceMode.Force);
                    //transform.position = new Vector3(transform.position.x, 1f + hit.point.y, transform.position.z);
                }
                //transform.position = new Vector3(transform.position.x, hit.point.y + 0.5f, transform.position.z);
            }
		} else {
			// increase gravity
			myRigidbody.AddForce(Physics.gravity * gravityMultiplier);
		}

		if (canJumpCountdown > 0f) {
			canJumpCountdown -= 0.03f;
        }
       // if (vrDontCheckGround > 0f) {
       //     vrDontCheckGround -= 0.03f;
       // }
        

        if (unlimittedForwardSpeedCountdown > 0f) {
			unlimittedForwardSpeedCountdown -= 0.03f;
		}

		// forward speed
		if (myRigidbody.velocity.z < forwardMaxSpeed) {
			myRigidbody.AddForce(new Vector3(0f,0f,1f) * forwardMoveSpeed);
		}
		else if (unlimittedForwardSpeedCountdown <= 0f && myRigidbody.velocity.z > forwardMaxSpeed) {
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y, myRigidbody.velocity.z * forwardDrag);//Mathf.Min(forwardMaxSpeed, myRigidbody.velocity.z));
		}

		// horizontal speed
		float horizontalAxis = Input.GetAxis("Horizontal");

        if (GameManagerChasm.gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild != GameBuild.VR_Android) {
            fps.m_Lens.Dutch = Mathf.Lerp(fps.m_Lens.Dutch, -horizontalAxis * 2f, 0.1f); // view roll
			tps.m_Lens.Dutch = Mathf.Lerp(fps.m_Lens.Dutch, -horizontalAxis * 2f, 0.1f); // view roll
		}

        myRigidbody.AddForce(new Vector3(horizontalAxis * horizontalMoveSpeed, 0f, 0f), ForceMode.Force);
		if (myRigidbody.velocity.x > horizontalMaxSpeed) {
			myRigidbody.velocity = new Vector3(horizontalMaxSpeed, myRigidbody.velocity.y, myRigidbody.velocity.z);
		} else if (myRigidbody.velocity.x < -horizontalMaxSpeed) {
			myRigidbody.velocity = new Vector3(-horizontalMaxSpeed, myRigidbody.velocity.y, myRigidbody.velocity.z);
		}

		// drag
		myRigidbody.velocity = new Vector3(myRigidbody.velocity.x * horizontalDrag, myRigidbody.velocity.y, myRigidbody.velocity.z);

		// death
		if (transform.position.y <= -15f && GameManagerChasm.playerIsAlive) {
			KillPlayer();

        }

	}
	public void KillPlayer() {
		GameManagerChasm.currentDeaths += 1;
		GameManagerChasm.uiDeathCounter.text = GameManagerChasm.currentDeaths.ToString();

        myAudioSource.pitch = Random.Range(diePitch.x, diePitch.y);
        myAudioSource.PlayWebGL(dieAudioClip);
        GameManagerChasm.EndGame(); // end game has a check that player should be alive to endgame()
    }
}
