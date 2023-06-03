using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

/*
* Author: Alexis Clay Drain
*/
public class DialogueType : MonoBehaviour
{

    [Header("stuff to delete at the end")]
    public Text text1;
    public Text text2;

    [Header("audio")]
    public AudioSource textAudioSource;

    private string finalText = "";
    private Text myText;

    private void Start() {
        myText = GetComponent<Text>();

        finalText = GetComponent<Text>().text;
        myText.text = "";
        myText.enabled = true; // disabled in the editor and enabled here so that the first frame doesn't show the full text

        StartCoroutine(Typewriter());
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            StopCoroutine(Typewriter());
            StartCoroutine(startupGame());
        }
    }

    IEnumerator startupGame() {
        text1.enabled = false;
        text2.enabled = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Chasm_WebGL_Scene");
    }

    IEnumerator Typewriter() {
        yield return new WaitForSeconds(2f);
        GetComponent<AudioSource>().Play();
        foreach (char c in finalText) {
            if (c != ' ' || c != '\n') {
                textAudioSource.Play();
            }
            myText.text += c;
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(8f);
        StartCoroutine(startupGame());
        yield return null;
    }

}
