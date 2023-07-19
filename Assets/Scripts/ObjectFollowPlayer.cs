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
    public bool followYAxis = false;
	public float yOffset = 1f;
    void LateUpdate()
	{
		if(chasm) {
			if(followXAxis && followYAxis) {
				transform.position = new Vector3(GameManagerChasm.playerCol.transform.position.x, GameManagerChasm.playerCol.transform.position.y + yOffset, GameManagerChasm.playerCol.transform.position.z + distanceFromPlayer);
			}
            else if(followXAxis) {
				transform.position = new Vector3(GameManagerChasm.playerCol.transform.position.x, transform.position.y, GameManagerChasm.playerCol.transform.position.z + distanceFromPlayer);
			} else if (followYAxis) {
				print("Not Implemenmted");
			}
        } else {
			transform.position = new Vector3(transform.position.x, transform.position.y, GameManager.playerXRig.transform.position.z + distanceFromPlayer);
		}


	}
}
