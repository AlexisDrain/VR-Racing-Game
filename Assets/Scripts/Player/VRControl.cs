using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class VRControl : MonoBehaviour
{
	public Vector2 xyMagnitude = new Vector2(4.8f, 4.8f);
	public void Update() {
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xyMagnitude.x, xyMagnitude.x),
										Mathf.Clamp(transform.position.y, -xyMagnitude.y, xyMagnitude.y),
										transform.position.z);
	}
	void Start()
    {
        
    }
}
