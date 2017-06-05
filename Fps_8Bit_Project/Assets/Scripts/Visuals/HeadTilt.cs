using UnityEngine;
using System.Collections;

public class HeadTilt : MonoBehaviour {

    private PlayerController pController;
    private Animator anim;

	void Start () {
        pController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
	}

	void Update () {
	    if (pController.inputX > 0) {
            anim.SetFloat("Horizontal", 1f);
        }
        else if (pController.inputX < 0) {
            anim.SetFloat("Horizontal", -1f);
        } 
        else if (pController.inputX == 0) {
            anim.SetFloat("Horizontal", 0f);
        }
    }
}
