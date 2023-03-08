using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

/*
* Author: Alexis Clay Drain
*/
public class GameManager : MonoBehaviour
{
	public static GameObject gameManagerObj;
	public static GameObject mainCameraObj;
	public static GameObject playerXRig;

	void Awake()
    {
		gameManagerObj = gameObject;
		mainCameraObj = GameObject.Find("XRRig/Camera Offset/Main Camera");
		playerXRig = GameObject.Find("XRRig");
	}

	public void ResetWorldPos() {
		GetComponent<GenerateObstacles>().PushBackEnemies();
	}
	//VR
	/*
	void Start() {
		// According to https://docs.unity3d.com/Packages/com.unity.xr.management@4.0/manual/EndUser.html
		// VR must be called AFTER start has finished.
		StartCoroutine(StartXRCoroutine());
	}


	public IEnumerator StartXRCoroutine() {
		yield return new WaitForSeconds(0.1f);

		Debug.Log("Initializing XR...");
		yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

		if (XRGeneralSettings.Instance.Manager.activeLoader == null) {
			Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
		} else {
			Debug.Log("Starting XR...");
			XRGeneralSettings.Instance.Manager.StartSubsystems();
		}
	}


	void StopXR() {
		Debug.Log("Stopping XR...");

		XRGeneralSettings.Instance.Manager.StopSubsystems();
		XRGeneralSettings.Instance.Manager.DeinitializeLoader();
		Debug.Log("XR stopped completely.");
	}

	*/
}
