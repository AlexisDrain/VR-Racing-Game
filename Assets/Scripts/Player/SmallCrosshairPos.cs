using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/*
* Author: Alexis Clay Drain
*/

public class SmallCrosshairPos : MonoBehaviour
{

	private MouseControl mouseControl;
	private RectTransform myRectTransform;

	private void Start() {
		mouseControl = GameManager.mainCameraObj.GetComponent<MouseControl>();
		myRectTransform = gameObject.GetComponent<RectTransform>();
	}

	void LateUpdate()
	{

		myRectTransform.anchoredPosition = mouseControl.mousePosCrosshair;
	}

}
