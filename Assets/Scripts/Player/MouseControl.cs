using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class MouseControl : MonoBehaviour
{

	void FixedUpdate()
    {
		DetectInputMethod();

	}

	private void DetectInputMethod() {

		//Camera cam = gameObject.GetComponent<Camera>();
		//Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z * -18f));
		
		Vector2 mousePos = Input.mousePosition;

		// mouse in relation of middle of the screen
		mousePos.x -= Screen.width / 2;
		mousePos.y -= Screen.height / 2;

		Vector2 mousePosNormalized = mousePos.normalized;
		transform.position = new Vector3(mousePosNormalized.x, mousePosNormalized.y, transform.position.z);
		//print(mousePosNormalized.y + " trans:" + transform.position.y);
	}

}
