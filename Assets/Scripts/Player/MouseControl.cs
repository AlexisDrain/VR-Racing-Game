using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class MouseControl : MonoBehaviour {

	[Header("Options")]
	public float worldRange = 4.8f;


	[Header("Stats")]
	public Vector2 mousePosPixel;
	public Vector2 mousePosNormalized;
	public Vector2 mousePosCrosshair;
	public Vector2 mousePosWorld;

	void OnApplicationFocus(bool hasFocus) {
		if (hasFocus) {
			Cursor.visible = false;
			Debug.Log("Application is focussed");
		} else {
			Debug.Log("Application lost focus");
		}
	}

	void FixedUpdate() {
		CalculateMousePixel();
		mousePosCrosshair = ConvertNumberRangeToCrosshair();
		mousePosWorld = ConvertNumberRangeToWorldPos();
		transform.position = new Vector3(mousePosWorld.x, mousePosWorld.y, transform.position.z);
	}

	// convert number ranges to tiny crosshair
	private Vector2 ConvertNumberRangeToCrosshair() {
		Vector2 oldMouseRange = new Vector2(Screen.width / 2, Screen.height / 2);
		Vector2 newMouseRange = new Vector2(35f, 35f);
		Vector2 newValue = new Vector2(((((mousePosPixel.x - Screen.width / 2) * newMouseRange.x) / oldMouseRange.x) + 35f),
									   ((((mousePosPixel.y - Screen.height / 2) * newMouseRange.y) / oldMouseRange.y) + 35f));
		return Vector2.ClampMagnitude(newValue, 25f);
	}

	// convert number ranges to world position
	private Vector2 ConvertNumberRangeToWorldPos() {
		Vector2 oldMouseRange = new Vector2(Screen.width / 2, Screen.height / 2);
		Vector2 newMouseRange = new Vector2(worldRange, worldRange);
		Vector2 newValue = new Vector2(((((mousePosPixel.x - Screen.width / 2) * newMouseRange.x) / oldMouseRange.x) + worldRange),
									   ((((mousePosPixel.y - Screen.height / 2) * newMouseRange.y) / oldMouseRange.y) + worldRange));
		return Vector2.ClampMagnitude(newValue, worldRange);
	}
	private void CalculateMousePixel() {
		Vector2 mousePos = Input.mousePosition;

		// mouse in relation of middle of the screen
		mousePos.x -= Screen.width / 2;
		mousePos.y -= Screen.height / 2;

		mousePosPixel = mousePos;
		//mousePosNormalized = mousePos.normalized;
	}
}
