using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
* Author: Alexis Clay Drain
*/
public class UI_Volume : MonoBehaviour
{
	public AudioMixer audioMixer;
	public void SetMusicVolume(float newVolume) {

		// convert from float [0 to 10] TO [-80 to 0]
		// 0       -80
		// 10       0
		float originalStart = 0f, originalEnd = 10f;
		float newStart = -80f, newEnd = 0f;
		float scale = (newEnd - newStart) / (originalEnd - originalStart);
		newVolume = (newStart + ((newVolume - originalStart) * scale));

		audioMixer.SetFloat("MusicVolume", newVolume);
	}
	public void SetSFXVolume(float newVolume) {

		float originalStart = 0f, originalEnd = 10f;
		float newStart = -80f, newEnd = 0f;
		float scale = (newEnd - newStart) / (originalEnd - originalStart);
		newVolume = (newStart + ((newVolume - originalStart) * scale));

		audioMixer.SetFloat("SFXVolume", newVolume);
	}

}
