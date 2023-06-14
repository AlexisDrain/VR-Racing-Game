using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomosController : MonoBehaviour
{
    public float distanceFromPlayerToMove = 90f;
    public float finalDistance = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z > finalDistance) {
            return;
        }

        transform.position = new Vector3
            (transform.position.x,
            transform.position.y,
            GameManagerChasm.playerCol.transform.position.z + distanceFromPlayerToMove);
    }
}
