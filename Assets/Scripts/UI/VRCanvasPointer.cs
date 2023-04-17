using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRCanvasPointer : MonoBehaviour
{

	public Transform lineStartTrans;
    public LineRenderer lineRenderer = null;

	public EventSystem eventSystem= null;
	public StandaloneInputModule inputModule = null;

	private void Update() {
		UpdateLength();
	}

	private void UpdateLength() {
		lineRenderer.SetPosition(0, lineStartTrans.position);
		//lineRenderer.SetPosition(0, Vector3.zero);
		lineRenderer.SetPosition(1, GetEnd());
	}


	private Vector3 GetEnd() {
		float distance = GetCanvasDistance();
		Vector3 endPosition = CalculateEnd(10f);

		if (distance != 0f) {
			endPosition = CalculateEnd(distance);
		}

		return endPosition;
	}

	private float GetCanvasDistance() {
		PointerEventData eventData = new PointerEventData(eventSystem);
		eventData.position = inputModule.inputOverride.mousePosition;

		List<RaycastResult> results = new List<RaycastResult>();
		eventSystem.RaycastAll(eventData, results);

		RaycastResult closestResult = FindFirstRaycast(results);
		float distance = closestResult.distance;

		distance = Mathf.Clamp(distance, 0f, 10f);
		return distance;
	}

	private RaycastResult FindFirstRaycast(List<RaycastResult> results) {
		foreach(RaycastResult r in results) {
			if(r.gameObject == null) {
				continue;
			}
			return r;
		}

		return new RaycastResult();
	}
	private Vector3 CalculateEnd(float length) {
		return lineStartTrans.position + (transform.forward * length);
	}
	/*
	private RaycastHit CreateForwardRaycast() {
		RaycastHit hit;

		Ray ray = new Ray(transform.position, transform.forward);

		Physics.Raycast(ray, out hit, 10f);
		return hit;
	}
	*/
}
