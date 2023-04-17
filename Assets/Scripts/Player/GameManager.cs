using Oculus.Interaction.Unity.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Management;

/*
* Author: Alexis Clay Drain
*/

public enum GameBuild {
	WebGL,
	VR_Android
}

public class GameManager : MonoBehaviour
{
	public GameBuild gameBuild = GameBuild.WebGL;
	public AudioMixer audioMixer;
	public static float audioCutoffDistort = 1200f;

	public static GameObject gameManagerObj;
	private static Pool pool_LoudAudioSource;
	public static GameObject mainCameraObj;
	public static GameObject playerXRig;
	public static AudioSource musicAudioSrc;
	public static GameObject pauseMenu;
	public static GameObject deathMenu;
	public static GameObject uiScoreCounterBG;
	public static UIScoreCounter uiScoreCounter;
	// WebGL only
	public static NGHelper ngHelper;

	// VR only

	public static GameObject controllerRight;
	public static GameObject controllerLeft;

	public static bool playerIsAlive = false;
	public static bool playerInPauseMenu = true;
	public static float timeElapsedWhileAlive; // score
	public static float timeElapsedWhileAliveBest; // score

	void Awake()
	{
		gameManagerObj = gameObject;

		pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
		mainCameraObj = GameObject.Find("XRRig/Camera Offset/Main Camera");
		playerXRig = GameObject.Find("XRRig");
		musicAudioSrc = transform.Find("Music").GetComponent<AudioSource>();
		if (gameBuild == GameBuild.WebGL) {
			ngHelper = transform.Find("NewgroundsIO").GetComponent<NGHelper>();
			pauseMenu = GameObject.Find("Canvas/PauseMenu");
			deathMenu = GameObject.Find("Canvas/DeathMenu");
			uiScoreCounterBG = GameObject.Find("Canvas/TimeScoreBG").gameObject;
			uiScoreCounter = GameObject.Find("Canvas/TimeScoreBG/TimeScore").GetComponent<UIScoreCounter>();
		} else if (gameBuild == GameBuild.VR_Android) {


			pauseMenu = GameObject.Find("World/CanvasVRWorld/PauseMenu");
			deathMenu = GameObject.Find("World/CanvasVRWorld/DeathMenu");
			uiScoreCounterBG = GameObject.Find("World/CanvasVRWorld/TimeScoreBG").gameObject;
			uiScoreCounter = GameObject.Find("World/CanvasVRWorld/TimeScoreBG/TimeScore").GetComponent<UIScoreCounter>();

			controllerRight = playerXRig.transform.Find("RightController").gameObject;
			controllerLeft = playerXRig.transform.Find("LeftController").gameObject;
		}

	}
	private void Start() {
		Time.timeScale = 0f;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);

		deathMenu.SetActive(false);
	}

	public void FixedUpdate() {
		//OVRInput.FixedUpdate(); // Contrary to the Meta docs DO NOT CALL THIS
		if (GameManager.playerIsAlive) {
			timeElapsedWhileAlive += Time.deltaTime;
		}
	}
	public void Update() {
		//OVRInput.Update(); // Contrary to the Meta docs DO NOT CALL THIS
		if (Input.GetButtonDown("Pause") || OVRInput.GetDown(OVRInput.Button.Start)) {
			PauseGame();
		}
		


		if (GameManager.playerIsAlive == false) {
			// player is dead
			 if ((Input.GetButtonDown("Restart") || OVRInput.GetDown(OVRInput.Button.One)) && playerInPauseMenu == false) {
				StartGame();
			}
		}

		// TESTING. For VR only
		if (OVRInput.GetDown(OVRInput.Button.One) && playerInPauseMenu == true) {
			StartGame();
		}
	}
	public static void StartGame() {

		gameManagerObj.GetComponent<GenerateObstacles>().KillAllEnemies();

		Time.timeScale = 1f;
		playerIsAlive = true;
		uiScoreCounterBG.SetActive(true);
		timeElapsedWhileAlive = 0f;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", 0f);
		
		pauseMenu.SetActive(false);
		deathMenu.SetActive(false);
		
		playerInPauseMenu = false;

		//VR only
		if (gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(false);
			controllerLeft.gameObject.SetActive(false);
		}
		
	}
	public static void ResumeGame() {
		Time.timeScale = 1f;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", 0f);
		uiScoreCounterBG.SetActive(true);
		
		pauseMenu.SetActive(false);
		deathMenu.SetActive(false);

		playerInPauseMenu = false;
		//VR only
		if (gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(false);
			controllerLeft.gameObject.SetActive(false);
		}
	}
	public static void PauseGame() {

		Time.timeScale = 0f;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);

		pauseMenu.SetActive(true);
		deathMenu.SetActive(false);
		playerInPauseMenu = true;
		//VR only
		if (gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(true);
			controllerLeft.gameObject.SetActive(true);
		}
	}

	public static void EndGame() {

		if(gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.WebGL) {
			timeElapsedWhileAliveBest = Mathf.Max(timeElapsedWhileAliveBest, timeElapsedWhileAlive);
			print("posting scores");
			int timeInMilliSeconds = (int)(timeElapsedWhileAliveBest * 1000f);
			if (timeInMilliSeconds >= 60000) { // 1 minute
				//ngHelper.UnlockMedalHexagon();
			}
			//ngHelper.SubmitScores(timeInMilliSeconds);
		} else if (gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.VR_Android) {
			print("posting scores");
			if(timeElapsedWhileAliveBest < timeElapsedWhileAlive) {
				timeElapsedWhileAliveBest = timeElapsedWhileAlive;

				int timeInMilliSeconds = (int)(timeElapsedWhileAliveBest * 1000f);
				if (timeInMilliSeconds >= 60000) { // 1 minute
					Oculus.Platform.Achievements.Unlock("Hexagon");
				}
				Oculus.Platform.Leaderboards.WriteEntry("SurvivalTime", (long) timeInMilliSeconds);
			}

		}

		Time.timeScale = 0.15f;
		playerIsAlive = false;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);
		if (gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.WebGL) {
			uiScoreCounterBG.SetActive(false);
		}
		pauseMenu.SetActive(false);
		deathMenu.SetActive(true);
		//VR only
		if (gameManagerObj.GetComponent<GameManager>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(true);
			controllerLeft.gameObject.SetActive(true);
		}
	}

	public void ResetWorldPos() {
		GetComponent<GenerateObstacles>().PushBackEnemies();
	}
	public static AudioSource SpawnLoudAudio(AudioClip newAudioClip, float newVolume = 1f) {

		AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
		audioObject.PlayWebGL(newAudioClip, newVolume);
		return audioObject;
		// audio object will set itself to inactive after done playing.
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
