using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class LaserController : MonoBehaviour
{
    public Transform startLine;
    public Transform endLine;
    public LineRenderer myLineRenderer1;
    public LineRenderer myLineRenderer2;

    void Start() {
        
    }

    public void FixedUpdate() {

        RaycastHit hit;
        Physics.Linecast(startLine.position, endLine.position, out hit, (1<<GameManagerChasm.layerWorld));

        if(hit.collider != null) {

            // line 1 effect
            myLineRenderer1.SetPosition(1, transform.InverseTransformPoint(hit.point));
            GameManagerChasm.particles_LazerEnd.transform.position = hit.point;
            GameManagerChasm.particles_LazerEnd.Emit(1);

            // line 2
            RaycastHit hit2;
            Physics.Linecast(endLine.position, startLine.position, out hit2, (1 << GameManagerChasm.layerWorld));

            if (hit2.collider != null) {
                myLineRenderer2.SetPosition(1, transform.InverseTransformPoint(hit2.point));
                GameManagerChasm.particles_LazerEnd.transform.position = hit2.point;
                GameManagerChasm.particles_LazerEnd.Emit(1);
            }
        } else {
            myLineRenderer1.SetPosition(1, transform.InverseTransformPoint(endLine.position+new Vector3(0f,0.5f,0f)));
            myLineRenderer2.SetPosition(1, myLineRenderer2.GetPosition(0));
        }
    }
}
