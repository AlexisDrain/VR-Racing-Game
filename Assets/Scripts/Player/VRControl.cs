using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class VRControl : MonoBehaviour
{
	//public Vector2 xyMagnitude = new Vector2(4.8f, 4.8f);
	public float maxWorldMagnitude = 4.8f;
	public Vector3 offset;

	private void Start() {
		offset = GameManager.playerXRig.transform.position;
	}

	public void Update() {
		//transform.position = new Vector3(Mathf.Clamp(transform.position.x + offset.x, -xyMagnitude.x, xyMagnitude.x),
		//								Mathf.Clamp(transform.position.y + offset.y, -xyMagnitude.y, xyMagnitude.y),
		//								transform.position.z);


		// clamp x and y to 4.8. set z to zero
		Vector3 clampedXY = Vector3.ClampMagnitude(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), maxWorldMagnitude);
		transform.position = new Vector3(clampedXY.x, clampedXY.y, transform.position.z);
	}

}
