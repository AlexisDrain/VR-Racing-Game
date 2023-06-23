using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class ObjectFollowPlayer : MonoBehaviour
{

	public float distanceFromPlayer = 4f;
	void LateUpdate()
	{

		transform.position = new Vector3(transform.position.x, transform.position.y, GameManager.playerXRig.transform.position.z + distanceFromPlayer);

	}
}
