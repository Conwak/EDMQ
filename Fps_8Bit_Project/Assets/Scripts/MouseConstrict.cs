using UnityEngine;
using System.Collections;

public class MouseConstrict : MonoBehaviour {

    GameObject currentCam;
    private Pause pause;

	void Start () {
        currentCam = GameObject.Find("SceneCamera");
        pause = FindObjectOfType<Pause>();
	}
	
	void Update () {
        if (pause.paused == true && currentCam.activeSelf == false) {
            Cursor.lockState = CursorLockMode.None;
        } 
        else if (currentCam.activeSelf == false && pause.paused == false) {
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
}
