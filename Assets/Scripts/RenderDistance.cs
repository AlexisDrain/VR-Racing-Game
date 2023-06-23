using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class RenderDistance : MonoBehaviour
{
    public bool renderIfFar = false;
    public float distance;

    private bool isRendering = true;
    void LateUpdate() {
        if(renderIfFar == true) {
            if(isRendering == false && Vector3.Distance(Camera.main.transform.position, transform.position) >= distance) {
                GetComponent<Renderer>().enabled = true;
                isRendering = true;
            } else if (isRendering == true && Vector3.Distance(Camera.main.transform.position, transform.position) < distance) {
                GetComponent<Renderer>().enabled = false;
                isRendering = false;
            }
        } else if (renderIfFar == false) {
            if (isRendering == false && Vector3.Distance(Camera.main.transform.position, transform.position) <= distance) {
                GetComponent<Renderer>().enabled = true;
                isRendering = true;
            } else if (isRendering == true && Vector3.Distance(Camera.main.transform.position, transform.position) > distance) {
                GetComponent<Renderer>().enabled = false;
                isRendering = false;
            }
        }
    }
}
