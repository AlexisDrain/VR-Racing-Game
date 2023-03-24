using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class MouseControl : MonoBehaviour {

	[Header("Options")]
	public float worldRange = 4.8f;
	public Vector2 tinyCrosshairRange = new Vector2(45f, 45f);

	[Header("Stats")]
	public Vector2 mousePosPixel;
	public Vector2 mousePosNormalized;
	public Vector2 mousePosCrosshair;
	public Vector2 mousePosWorld;

	void OnApplicationFocus(bool hasFocus) {
		//Cursor.visible = false;
		//if (hasFocus) {
			//Debug.Log("Application is focussed");
		//}
		// else {
			//Debug.Log("Application lost focus");
		//}
	}

	void FixedUpdate() {
		CalculateMousePixel();
		mousePosCrosshair = ConvertNumberRangeToCrosshair();
		mousePosWorld = ConvertNumberRangeToWorldPos();
		if (GameManager.playerIsAlive == true) {
			transform.position = new Vector3(mousePosWorld.x, mousePosWorld.y, transform.position.z);
		}
	}

	// convert number ranges to tiny crosshair
	private Vector2 ConvertNumberRangeToCrosshair() {
		Vector2 oldMouseRange = new Vector2(Screen.width / 2, Screen.height / 2);
		Vector2 newMouseRange = new Vector2(tinyCrosshairRange.x, tinyCrosshairRange.y);
		Vector2 newValue = new Vector2(((((mousePosPixel.x - Screen.width / 2) * newMouseRange.x) / oldMouseRange.x) + tinyCrosshairRange.x),
									   ((((mousePosPixel.y - Screen.height / 2) * newMouseRange.y) / oldMouseRange.y) + tinyCrosshairRange.y));
		return Vector2.ClampMagnitude(newValue, 65f);
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
