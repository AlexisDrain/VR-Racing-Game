using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class ObjectFollowPlayer : MonoBehaviour
{

	public float distanceFromPlayer = 4f;
	public bool chasm = false;
    public bool followXAxis = false;
    void LateUpdate()
	{
		if(chasm && followXAxis) {

            transform.position = new Vector3(GameManagerChasm.playerXRig.transform.position.x, transform.position.y, GameManagerChasm.playerXRig.transform.position.z + distanceFromPlayer);
        } else {
			transform.position = new Vector3(transform.position.x, transform.position.y, GameManager.playerXRig.transform.position.z + distanceFromPlayer);
		}


	}
}
