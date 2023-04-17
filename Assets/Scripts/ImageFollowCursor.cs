using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class ImageFollowCursor : MonoBehaviour
{
	private RectTransform myRectTransform;

	private void Start() {
		myRectTransform = gameObject.GetComponent<RectTransform>();
	}

	void LateUpdate()
	{
		Vector2 mousePos = Input.mousePosition;

		// mouse in relation of middle of the screen
		mousePos.x -= Screen.width / 2;
		mousePos.y -= Screen.height / 2;

		myRectTransform.anchoredPosition = mousePos;

	}
}
