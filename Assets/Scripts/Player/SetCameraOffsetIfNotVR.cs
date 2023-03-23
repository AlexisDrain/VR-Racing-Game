using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;
using UnityEngine.XR;

/*
* Author: Alexis Clay Drain
*/
public class SetCameraOffsetIfNotVR : MonoBehaviour
{

	public float _cameraYOffset = 1.36144f;
	void Start()
    {
		GetComponent<CameraOffset>().cameraYOffset = 0f;
		if (Application.platform == RuntimePlatform.Android) {
			print("Platform is Android. Adding cameraYOffset");
			GetComponent<CameraOffset>().cameraYOffset = _cameraYOffset;
		}
		/*
		// Input devices from https://docs.unity3d.com/Manual/xr_input.html

		var inputDevices = new List<UnityEngine.XR.InputDevice>();
		UnityEngine.XR.InputDevices.GetDevices(inputDevices);


		foreach (var device in inputDevices) {
			Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
			GetComponent<CameraOffset>().cameraYOffset = _cameraYOffset;
		}
		*/
	}
}
