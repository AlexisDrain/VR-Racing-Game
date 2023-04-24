using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerChasm : MonoBehaviour
{

    public float forwardMoveSpeed = 5f;
    public float forwardMaxSpeed = 50f;
	public float horizontalMoveSpeed = 10f;

	public float jumpPower = 10f;
	private bool holdingJump = false;
	private bool onGround = true;

	private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();

	}

	private void Update() {
		if (Input.GetButtonDown("Jump") && onGround == true) {
			myRigidbody.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Impulse);
			holdingJump = true;
		}
		if (Input.GetButtonUp("Jump") && myRigidbody.velocity.y > 0f && holdingJump == true) {
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y / 2, myRigidbody.velocity.z);
		}

		if (onGround == true) {
			//holdingJump = false;
		}
	}
	// Update is called once per frame
	void FixedUpdate()
    {
		float horizontalAxis = Input.GetAxis("Horizontal");
		myRigidbody.AddForce(new Vector3(horizontalAxis * horizontalMoveSpeed, 0f, 0f), ForceMode.Force);

        if (myRigidbody.velocity.z < forwardMaxSpeed) {
		    myRigidbody.AddForce(new Vector3(0f,0f,1f) * forwardMoveSpeed);
        }
		else if (myRigidbody.velocity.z > forwardMaxSpeed) {
			myRigidbody.velocity = new Vector3(0f, 0f, forwardMaxSpeed);
		}

		if (transform.position.y <= -15f) {
			GameManagerChasm.EndGame(); // end game has a check that player should be alive to endgame()
		}
	}
}
