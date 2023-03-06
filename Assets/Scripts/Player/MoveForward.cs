using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
		myRigidbody = GetComponent<Rigidbody>();

	}

    // Update is called once per frame
    void FixedUpdate()
    {
		myRigidbody.AddForce(new Vector3(0f,0f,1f) * moveSpeed);

	}
}
