using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject door;
    public Transform target;
    public float speed;
    private float step;
    private bool doorPressed;

    void Update () {
        step = speed * Time.deltaTime;
        if (doorPressed) {
            door.transform.position = Vector3.Lerp(door.transform.position, target.position, step);
        }
    }

	void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (Input.GetButton("Action")) {
                doorPressed = true;
            }
        }
    }
}
