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

		audioMixer.SetFloat("MusicVolume", newVolume);
	}
	public void SetSFXVolume(float newVolume) {

		audioMixer.SetFloat("SFXVolume", newVolume);

	}

}
