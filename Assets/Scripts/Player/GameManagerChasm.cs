using Oculus.Interaction.Unity.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Management;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.UI;
/*
* Author: Alexis Clay Drain
*/
public enum GameScene {
	blueVortex,
	chasm
}

public class GameManagerChasm : MonoBehaviour
{
	public GameBuild gameBuild = GameBuild.WebGL;
	public GameScene currentGameScene;

	public static LayerMask layerWorld;
	public static LayerMask layerEntity;

	public AudioMixer audioMixer;
	public static float audioCutoffDistort = 1200f;

	public static GameObject gameManagerChasmObj;
	private static Pool pool_LoudAudioSource;
    public static Pool pool_Explosions;
    public static ParticleSystem particles_LazerEnd;
    public static ParticleSystem particles_Background;
    public static GameObject mainCameraObj;
	public static GameObject playerXRig;
	public static AudioSource musicAudioSrc;
	public static Transform worldTransform;
    public static GameObject resumeButton;
    public static GameObject pauseMenu;
	public static GameObject deathMenu;
	public static GameObject levelMenu;
	public static GameObject nextLevelMenu;
	public static GameObject endingMenu;
	public static GameObject canvasControls;
	
	//public static GameObject uiScoreCounterBG;
	//public static UIScoreCounter uiScoreCounter;
	public static Text uiLevelCounter;
	public static GameObject playerCol;

	public static bool hardMode = false;
	public static Vector3 playerCheckpointPos;
	public static int unlockedLevels;
	public static int currentLevel;
	

	public static UnityEvent resetEnemyCollisions = new UnityEvent();
	// WebGL only
	public static NGHelper ngHelper;

	// VR only

	public static GameObject controllerRight;
	public static GameObject controllerLeft;

	public static bool playerInNextLevel = false;
	public static bool playerIsAlive = false;
	public static bool playerInPauseMenu = true;
	public static float timeElapsedWhileAlive; // score
	public static float timeElapsedWhileAliveBest; // score

	void Awake()
	{
		gameManagerChasmObj = gameObject;
		currentGameScene = GameScene.chasm;

		layerWorld = LayerMask.NameToLayer("World");
		layerEntity = LayerMask.NameToLayer("Entity");


		pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        pool_Explosions = transform.Find("Pool_Explosions").GetComponent<Pool>();
        
        particles_LazerEnd = transform.Find("Particles_LazerEnd").GetComponent<ParticleSystem>();
        particles_Background = GameObject.Find("WorldDontDelete/Particles_Background").GetComponent<ParticleSystem>();
        
        mainCameraObj = GameObject.Find("XRRig/Camera Offset/Main Camera");
		playerXRig = GameObject.Find("XRRig");
		musicAudioSrc = transform.Find("Music").GetComponent<AudioSource>();
		worldTransform = GameObject.Find("World").transform;
		

		if (gameBuild == GameBuild.WebGL) {
            resumeButton = GameObject.Find("Canvas/PauseMenuChasm/MainMenu/Frame/Resume Game");
            resumeButton.SetActive(false);
            pauseMenu = GameObject.Find("Canvas/PauseMenuChasm");
			deathMenu = GameObject.Find("Canvas/DeathMenuChasm");
			levelMenu = GameObject.Find("Canvas/LevelMenuChasm");
			nextLevelMenu = GameObject.Find("Canvas/NextLevelMenuChasm");
			endingMenu = GameObject.Find("Canvas/EndingMenuChasm");
			
			canvasControls = GameObject.Find("Canvas/Controls_Jump");
			
			ngHelper = transform.Find("NewgroundsIO").GetComponent<NGHelper>();
			//uiScoreCounterBG = GameObject.Find("Canvas/TimeScoreBG").gameObject;
			//uiScoreCounter = GameObject.Find("Canvas/TimeScoreBG/TimeScore").GetComponent<UIScoreCounter>();
			uiLevelCounter = GameObject.Find("Canvas/CurrentLevelBG/TextCurrentLevel").GetComponent<Text>();
			playerCol = playerXRig.transform.Find("PlayerCol").gameObject;


		} else if (gameBuild == GameBuild.VR_Android) {


			pauseMenu = GameObject.Find("World/CanvasVRWorld/PauseMenuChasm");
			deathMenu = GameObject.Find("World/CanvasVRWorld/DeathMenuChasm");
			//uiScoreCounterBG = GameObject.Find("World/CanvasVRWorld/TimeScoreBG").gameObject;
			//uiScoreCounter = GameObject.Find("World/CanvasVRWorld/TimeScoreBG/TimeScore").GetComponent<UIScoreCounter>();

			controllerRight = playerXRig.transform.Find("RightController").gameObject;
			controllerLeft = playerXRig.transform.Find("LeftController").gameObject;
		}

		playerCheckpointPos = playerCol.transform.position;

	}
	private void Start() {
		Time.timeScale = 0f;
		gameManagerChasmObj.GetComponent<GameManagerChasm>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);


        GetComponent<NavigateMenus>().OpenPauseMenu();
	}

	public void FixedUpdate() {
		//OVRInput.FixedUpdate(); // Contrary to the Meta docs DO NOT CALL THIS
		if (GameManagerChasm.playerIsAlive) {
			if (GameManagerChasm.hardMode == false) {
				timeElapsedWhileAlive += Time.deltaTime;
			} else {
				// hard mode. Time moves half as fast
				timeElapsedWhileAlive += Time.deltaTime * 0.5f;
			}
		}
	}
	public void Update() {
		//OVRInput.Update(); // Contrary to the Meta docs DO NOT CALL THIS
		if (Input.GetButtonDown("Pause") || OVRInput.GetDown(OVRInput.Button.Start)) {
			if(GameManagerChasm.playerIsAlive == true) {
				PauseGame();
			} else {
				// player is dead & hit pause button
				StartGame();
				PauseGame();
			}
			
		}

		if (Input.GetKeyDown(KeyCode.C)) {
			ChangeCameraView();
		}

		if(Input.GetKeyDown(KeyCode.H)) {
			canvasControls.SetActive(!canvasControls.activeSelf);
		}

		if (GameManagerChasm.playerInNextLevel == true) {
			if (Input.GetButtonDown("Restart") || Input.GetButtonDown("Jump") || Input.GetButtonDown("JumpAlt") || OVRInput.GetDown(OVRInput.Button.One)) {
				SetupNextLevel();
				StartGame();
				return;
			}
		}

		// playerRestart
		if (GameManagerChasm.playerIsAlive == false) {
			// player is dead
			 if ((Input.GetButtonDown("Restart") || Input.GetButtonDown("Jump") || Input.GetButtonDown("JumpAlt") || OVRInput.GetDown(OVRInput.Button.One)) && playerInPauseMenu == false) {
				StartGame();
			}
		}

		// Test 
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.F2)
			&& gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.WebGL) {
			Time.timeScale = 0.1f;
		}
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.F3)
	&& gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.WebGL) {
			print("Cheat Activated: Unlock all levels");
			GameManagerChasm.unlockedLevels = 10;
			GameObject levelLoaderUI = GameObject.Find("Canvas/LevelMenuChasm/Frame/Levels");
			if (levelLoaderUI != null) {
                levelLoaderUI.GetComponent<LevelLoaderUI>().CheckUnlockedLevelButtons();

			}
		}
		// VR only
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.F1) && gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android) {
			StartGame();
		}
		// For VR only
		if (OVRInput.GetDown(OVRInput.Button.One) && playerInPauseMenu == true) {
			StartGame();
		}
	}
	public static void SetHardMode(bool stateHard) {
		GameManagerChasm.hardMode = stateHard;
	}

	public static void ChangeCameraView() {

		CinemachineVirtualCamera tps = playerXRig.transform.Find("PlayerCol/CM vcam1-3PS").GetComponent<CinemachineVirtualCamera>();
		CinemachineVirtualCamera fps = playerXRig.transform.Find("PlayerCol/CM vcam2-FPS").GetComponent<CinemachineVirtualCamera>();

		if (fps.Priority == 2) {
			fps.Priority = 1;
			tps.Priority = 2;
		} else if (fps.Priority == 1) {
			fps.Priority = 2;
			tps.Priority = 1;
		}
	}

	public static void SetupNextLevel() {
		GameManagerChasm.unlockedLevels = Mathf.Max(GameManagerChasm.unlockedLevels, GameManagerChasm.currentLevel + 1);
		GameManagerChasm.currentLevel = GameManagerChasm.currentLevel + 1;

		if(GameManagerChasm.currentLevel == 10) {
			GameManagerChasm.gameManagerChasmObj.GetComponent<NavigateMenus>().OpenEndingMenu();
			return;
		}

		GameManagerChasm.gameManagerChasmObj.GetComponent<LevelSpawner>().SpawnLevel(GameManagerChasm.currentLevel);
	}

	public static void StartGame() {

		//gameManagerObj.GetComponent<GenerateObstacles>().KillAllEnemies();

		if (GameManagerChasm.hardMode == true) {
			Time.timeScale = 2f;
		} else {
			Time.timeScale = 1f;
		}
		playerIsAlive = true;
		playerInNextLevel = false;
		resumeButton.SetActive(true);
        //uiScoreCounterBG.SetActive(true);
        timeElapsedWhileAlive = 0f;
		gameManagerChasmObj.GetComponent<GameManagerChasm>().audioMixer.SetFloat("MusicCutoff", 0f);

        playerCol.GetComponent<Rigidbody>().velocity = new Vector3();
		playerCol.GetComponent<Rigidbody>().position = playerCheckpointPos; // new bug in Unity 2022.3
		playerCol.transform.position = playerCheckpointPos;
		resetEnemyCollisions.Invoke();

		gameManagerChasmObj.GetComponent<NavigateMenus>().CloseAllMenus();

		playerInPauseMenu = false;

		//VR only
		if (gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(false);
			controllerLeft.gameObject.SetActive(false);
		}
		
	}
	public static void ResumeGame() {
		if (GameManagerChasm.hardMode == true) {
			Time.timeScale = 2f;
		} else {
			Time.timeScale = 1f;
		}

		gameManagerChasmObj.GetComponent<GameManagerChasm>().audioMixer.SetFloat("MusicCutoff", 0f);
		//uiScoreCounterBG.SetActive(true);

		gameManagerChasmObj.GetComponent<NavigateMenus>().CloseAllMenus();

		playerInPauseMenu = false;
		//VR only
		if (gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(false);
			controllerLeft.gameObject.SetActive(false);
		}
	}
	public static void PauseGame() {

		Time.timeScale = 0f;
		gameManagerChasmObj.GetComponent<GameManagerChasm>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);

		gameManagerChasmObj.GetComponent<NavigateMenus>().OpenPauseMenu();
		playerInPauseMenu = true;
		//VR only
		if (gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(true);
			controllerLeft.gameObject.SetActive(true);
		}
	}
	public static void NextLevelMenu() {
		Time.timeScale = 0.15f;
		playerIsAlive = false;
		playerInNextLevel = true;
		gameManagerChasmObj.GetComponent<GameManagerChasm>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);
		if (gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.WebGL) {
			//uiScoreCounterBG.SetActive(false);
		}
		gameManagerChasmObj.GetComponent<NavigateMenus>().OpenNextLevelMenu();
		GameManagerChasm.unlockedLevels = Mathf.Max(GameManagerChasm.unlockedLevels, GameManagerChasm.currentLevel);
		

		//VR only
		if (gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(true);
			controllerLeft.gameObject.SetActive(true);
		}
	}

	public static void EndGame() {

		if (playerIsAlive == false) {
			return;
		}

		if(gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.WebGL) {
			timeElapsedWhileAliveBest = Mathf.Max(timeElapsedWhileAliveBest, timeElapsedWhileAlive);
			print("TODO: posting scores");
			int timeInMilliSeconds = (int)(timeElapsedWhileAliveBest * 1000f);
			if (timeInMilliSeconds >= 60000) { // 1 minute
				//ngHelper.UnlockMedalHexagon();
			}
			//ngHelper.SubmitScores(timeInMilliSeconds);
		} else if (gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android) {
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
		gameManagerChasmObj.GetComponent<GameManagerChasm>().audioMixer.SetFloat("MusicCutoff", audioCutoffDistort);
		if (gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.WebGL) {
			//uiScoreCounterBG.SetActive(false);
		}
		gameManagerChasmObj.GetComponent<NavigateMenus>().OpenDeathMenu();
		//VR only
		if (gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android) {
			controllerRight.gameObject.SetActive(true);
			controllerLeft.gameObject.SetActive(true);
		}
	}

	public void ResetWorldPos() {
		playerXRig.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		//GetComponent<GenerateObstacles>().PushBackEnemies();

	}


	public static AudioSource SpawnLoudAudio(AudioClip newAudioClip, Vector2 pitch = new Vector2(),float newVolume = 1f) {

		float sfxPitch;
		if(pitch.x <= 0.1f) {
			sfxPitch = 1;
		} else {
			sfxPitch = Random.Range(pitch.x, pitch.y);
		}

		AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
		audioObject.GetComponent<AudioSource>().pitch = sfxPitch;
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
