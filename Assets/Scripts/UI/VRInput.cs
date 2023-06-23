using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRInput : BaseInput
{
    public Camera eventCamera = null;

    public OVRInput.Button clickButton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Controller activeController = OVRInput.Controller.All;

    protected override void Awake() {
        GetComponent<BaseInputModule>().inputOverride = this;
    }

	public override bool GetMouseButton(int button) {
        return OVRInput.Get(clickButton, activeController);
	}
	public override bool GetMouseButtonDown(int button) {
		return OVRInput.GetDown(clickButton, activeController);
	}
	public override bool GetMouseButtonUp(int button) {
		return OVRInput.GetUp(clickButton, activeController);
	}

	public override Vector2 mousePosition {
		get {
			return new Vector2(eventCamera.pixelWidth / 2, eventCamera.pixelHeight / 2);
		}
	}
	public override bool mousePresent {
		get {
			return true;
		}
	}
}
