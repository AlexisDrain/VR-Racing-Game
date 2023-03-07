using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWorldPosOnDistance : MonoBehaviour
{
    public float resetDistance = 150f;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.z >= resetDistance) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
    }
}
