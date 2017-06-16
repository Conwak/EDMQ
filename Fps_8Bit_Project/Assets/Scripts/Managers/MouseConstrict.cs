using UnityEngine;
using System.Collections;

public class MouseConstrict : MonoBehaviour {

    private Pause pause;

	void Start () {
        pause = FindObjectOfType<Pause>();
	}
	
	void Update () {
        if (pause.paused) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
}
