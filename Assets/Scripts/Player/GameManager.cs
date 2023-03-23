using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Management;

/*
* Author: Alexis Clay Drain
*/
public class GameManager : MonoBehaviour
{

	public AudioMixer audioMixer;
	public static float audioCutoffDistort = 1200f;

	public static GameObject gameManagerObj;
	private static Pool pool_LoudAudioSource;
	public static GameObject mainCameraObj;
	public static GameObject playerXRig;
	public static AudioSource musicAudioSrc;
	public static GameObject pauseMenu;
	public static GameObject deathMenu;
	public static UIScoreCounter uiScoreCounter;

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
		pauseMenu = GameObject.Find("Canvas/PauseMenu");
		deathMenu = GameObject.Find("Canvas/DeathMenu");
		uiScoreCounter = GameObject.Find("Canvas/TimeScore").GetComponent<UIScoreCounter>();
	}
	private void Start() {
		Time.timeScale = 0f;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);

		deathMenu.SetActive(false);
	}

	public void FixedUpdate() {
		if (GameManager.playerIsAlive) {
			timeElapsedWhileAlive += Time.deltaTime;
		}
	}
	public void Update() {
		if (Input.GetButtonDown("Pause")) {
			PauseGame();
		}

		if (GameManager.playerIsAlive == false) {
			// player is dead
			 if (Input.GetButtonDown("Restart") && playerInPauseMenu == false) {
				StartGame();
			}
		}
	}
	public static void StartGame() {

		gameManagerObj.GetComponent<GenerateObstacles>().KillAllEnemies();

		Time.timeScale = 1f;
		playerIsAlive = true;
		uiScoreCounter.gameObject.SetActive(true);
		timeElapsedWhileAlive = 0f;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", 0f);
		
		pauseMenu.SetActive(false);
		deathMenu.SetActive(false);
		
		playerInPauseMenu = false;
	}
	public static void ResumeGame() {
		Time.timeScale = 1f;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", 0f);

		pauseMenu.SetActive(false);
		deathMenu.SetActive(false);

		playerInPauseMenu = false;
	}
	public static void PauseGame() {

		Time.timeScale = 0f;
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);

		pauseMenu.SetActive(true);
		deathMenu.SetActive(false);
		playerInPauseMenu = true;
	}

	public static void EndGame() {
		// todo: upload score to NG. check achievement
		timeElapsedWhileAliveBest = Mathf.Max(timeElapsedWhileAliveBest, timeElapsedWhileAlive);


		Time.timeScale = 0.15f;
		playerIsAlive = false;
		uiScoreCounter.gameObject.SetActive(false);
		gameManagerObj.GetComponent<GameManager>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);

		pauseMenu.SetActive(false);
		deathMenu.SetActive(true);
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
