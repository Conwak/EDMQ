using UnityEngine;
using System.Collections;

public class MouseConstrict : MonoBehaviour {

    GameObject currentCam;

	void Start () {
        currentCam = GameObject.Find("SceneCamera");
	}
	
	void Update () {
        if (currentCam.active == false) {
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
}
