using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerChasm : MonoBehaviour
{

    public float forwardMoveSpeed = 5f;
    public float forwardMaxSpeed = 50f;
	public float horizontalMoveSpeed = 50f;
    public float horizontalMaxSpeed = 10f;
	public float horizontalDrag = 0.98f;

    public float jumpPower = 10f;
	public float gravityMultiplier = 1.1f;
	private bool onGround = true;
	private float canJumpCountdown = 0f;

	private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();

	}

	private void Update() {
		if ((Input.GetButton("Jump") || Input.GetButton("JumpAlt")) && onGround == true && canJumpCountdown <= 0f) {
			myRigidbody.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Impulse);
			canJumpCountdown = 1f;

        }
		if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("JumpAlt")) && myRigidbody.velocity.y > 0f) {
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y / 2, myRigidbody.velocity.z);
		}

	}

	// Update is called once per frame
	void FixedUpdate() {
		// onground
		onGround = Physics.Linecast(transform.position + new Vector3(0.5f, 0f), transform.position + new Vector3(0.5f, 0f) + Vector3.down, (1 << GameManagerChasm.layerWorld))
				|| Physics.Linecast(transform.position + new Vector3(-0.5f, 0f), transform.position + new Vector3(-0.5f, 0f) + Vector3.down, (1 << GameManagerChasm.layerWorld));
        //onGround = Physics.BoxCast(transform.position, Vector3.one, Vector3.down, Quaternion.identity, 2f, (1 << GameManagerChasm.layerWorld));
        if (canJumpCountdown > 0f) {
			canJumpCountdown -= 0.03f;
		}
		// increase gravity
        myRigidbody.AddForce(Physics.gravity * gravityMultiplier);

        // forward speed
        if (myRigidbody.velocity.z < forwardMaxSpeed) {
		    myRigidbody.AddForce(new Vector3(0f,0f,1f) * forwardMoveSpeed);
        }
		else if (myRigidbody.velocity.z > forwardMaxSpeed) {
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y, Mathf.Min(forwardMaxSpeed, myRigidbody.velocity.z));
		}

		// horizontal speed
		float horizontalAxis = Input.GetAxis("Horizontal");
		myRigidbody.AddForce(new Vector3(horizontalAxis * horizontalMoveSpeed, 0f, 0f), ForceMode.Force);
		if (myRigidbody.velocity.x > horizontalMaxSpeed) {
            myRigidbody.velocity = new Vector3(horizontalMaxSpeed, myRigidbody.velocity.y, myRigidbody.velocity.z);
        } else if (myRigidbody.velocity.x < -horizontalMaxSpeed) {
            myRigidbody.velocity = new Vector3(-horizontalMaxSpeed, myRigidbody.velocity.y, myRigidbody.velocity.z);
        }

		// drag
		myRigidbody.velocity = new Vector3(myRigidbody.velocity.x * horizontalDrag, myRigidbody.velocity.y, myRigidbody.velocity.z);

		// death
		if (transform.position.y <= -15f) {
			GameManagerChasm.EndGame(); // end game has a check that player should be alive to endgame()
		}
	}
}
