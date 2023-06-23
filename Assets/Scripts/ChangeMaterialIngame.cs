using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

/*
* Author: Alexis Clay Drain
*/
public class ChangeMaterialIngame : MonoBehaviour
{
	public Material newMat;
	public Vector3 bounds = new Vector3(1f, 1f, 1f);

	public bool lineRenderer = false;
	void Start() {
		if (lineRenderer) {
            GetComponent<LineRenderer>().material = newMat;
            return;
		}
		GetComponent<MeshRenderer>().material = newMat;
		GetComponent<MeshRenderer>().bounds = new Bounds(GetComponent<MeshRenderer>().bounds.center, GetComponent<MeshRenderer>().bounds.size + bounds);
	}
}
