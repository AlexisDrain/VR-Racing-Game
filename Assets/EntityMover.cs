using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class EntityMover : MonoBehaviour
{
    public float startDelay = 1f;
    public float moveSpeed = 2f;
    public Transform destinationTransform;

    private float startDelayCurrent = 0f;
    private Vector3 pos1;
    private Vector3 pos2;
    private bool goTo2 = true;

    void Start() {
        startDelayCurrent = startDelay;
        pos1 = transform.position;
        pos2 = destinationTransform.position;
    }

    public void OnEnable() {
        //transform.position = pos1;
        startDelayCurrent = startDelay;
    }

    public void FixedUpdate() {

        if (startDelayCurrent > 0f) {
            startDelayCurrent -= 0.1f;
            return;
        }

        if(goTo2 == true && Vector3.Distance(transform.position, pos2) >= 0.2f) {
            transform.position = Vector3.MoveTowards(transform.position, pos2, Time.deltaTime * moveSpeed);
        } else {
            goTo2 = false;
        }
        if (goTo2 == false && Vector3.Distance(transform.position, pos1) >= 0.2f) {
            transform.position = Vector3.MoveTowards(transform.position, pos1, Time.deltaTime * moveSpeed);
        } else {
            goTo2 = true;
        }
    }
}
