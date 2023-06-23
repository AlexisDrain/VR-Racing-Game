using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EndingText : MonoBehaviour
{
    [TextAreaAttribute]
    public string hardModeText;
    [TextAreaAttribute]
    public string normalModeText;

    // Start is called before the first frame update
    void OnEnable()
    {
        if(GameManagerChasm.hardMode) {
            GetComponent<Text>().text = hardModeText;
            //GetComponent<Text>().text = textFieldHard.text;
        } else {
            GetComponent<Text>().text = normalModeText;
            //GetComponent<Text>().text = textFieldNormal.text;
        }
    }


}
