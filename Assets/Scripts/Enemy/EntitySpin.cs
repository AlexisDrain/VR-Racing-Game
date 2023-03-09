using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpin : MonoBehaviour
{
    public Vector2 spinSpeedRange = new Vector2(3f, 15f);

    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Awake()
    {
		myRigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void OnEnable()
    {
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;

        float speed = (Random.Range(0,2) * 2-1) * Random.Range(spinSpeedRange.x,spinSpeedRange.y);

		myRigidbody.AddTorque(new Vector3(0f,0f, speed));

	}
}
