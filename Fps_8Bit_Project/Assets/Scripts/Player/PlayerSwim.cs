using UnityEngine;
using System.Collections;

public class PlayerSwim : MonoBehaviour {

    private PlayerController pController;

	void Start () {
        pController = GetComponent<PlayerController>();

        RenderSettings.fog = false;
        RenderSettings.fogColor = new Color(63f, 47f, 23f, 1f);
        RenderSettings.fogDensity = 0.01f;
	}
	
    bool IsUnderwater () {
        return pController.isUnderwater;
    }

	void Update () {
        RenderSettings.fog = IsUnderwater();

        if (IsUnderwater()) {
            pController.runSpeed = 2f;
            pController.walkSpeed = 2f;
            pController.gravity = 0.01f;
            pController.jumpSpeed = 4f;
        } else {
            pController.runSpeed = 3f;
            pController.walkSpeed = 6f;
            pController.gravity = 14f;
            pController.jumpSpeed = 6f;
        }
	}
}
