using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder;

/*
* Author: Alexis Clay Drain
*/
public class ChangeMaterialIngame : MonoBehaviour
{
	public Material newMat;
	public Material vrNewMat;
	public Vector3 bounds = new Vector3(1f, 1f, 1f);

	public bool lineRenderer = false;
	void Start() {
		bool vr = GameManagerChasm.gameManagerChasmObj.GetComponent<GameManagerChasm>().gameBuild == GameBuild.VR_Android;
		if (lineRenderer) {
			if (vr) {
                GetComponent<LineRenderer>().material = vrNewMat;
            } else{
				GetComponent<LineRenderer>().material = newMat;
			}
            return;
        }
		if (vr) {
			GetComponent<MeshRenderer>().material = vrNewMat;
		} else {
            GetComponent<MeshRenderer>().material = newMat;
        }

		// for camera culling
		GetComponent<MeshRenderer>().bounds = new Bounds(GetComponent<MeshRenderer>().bounds.center, GetComponent<MeshRenderer>().bounds.size + bounds);
	}
}
