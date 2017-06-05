using UnityEngine;
using System.Collections;

public class MouseConstrict : MonoBehaviour {

    private Pause pause;

	void Start () {
        pause = FindObjectOfType<Pause>();
	}
	
	void Update () {
        if (pause.paused == true) {
            Cursor.lockState = CursorLockMode.None;
        } 
        else if (pause.paused == false) {
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
}
