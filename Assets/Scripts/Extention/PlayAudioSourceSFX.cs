﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSourceSFX: MonoBehaviour {

	public bool chasm = false;
	public float overrideAudioSourceVolume = 1f;
	//private Transform myTransform;

	public void PlaySFX () {

		GetComponent<AudioSource>().PlayWebGL(GetComponent<AudioSource>().clip, overrideAudioSourceVolume);
		//myTransform = transform;
	}
	public void PlayLoudSFXInGameManager(AudioClip newAudioClip) {

		if (chasm == true) {
			GameManagerChasm.SpawnLoudAudio(newAudioClip, overrideAudioSourceVolume);
		} else {
			GameManager.SpawnLoudAudio(newAudioClip, overrideAudioSourceVolume);
		}
		//myTransform = transform;
	}


	/*
	private void Update () {
		
	}
	*/
}
