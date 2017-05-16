using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject door;
    public Transform startTarget;
    public Transform endTarget;
    public float speed = 0.05f;
    private bool doorPressed;

    void Update () {
        if (doorPressed) {
            door.transform.position = Vector3.MoveTowards(door.transform.position, endTarget.position, speed);
        } else {
            Close();
        }
    }

	void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player") {
            if (Input.GetButton("Action")) {
                doorPressed = true;
            }
            else if (this.tag == "Red_Door") {
                if (Input.GetButton("Action")) {
                    doorPressed = true;
                }
            }
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(DoorClose());
        }
    }

    void Close () {
        if (door.transform.position == startTarget.position)
            return;
        door.transform.position = Vector3.MoveTowards(door.transform.position, startTarget.position, speed);
    }

    IEnumerator DoorClose () {
        yield return new WaitForSeconds(5f);
        doorPressed = false;
    }
}
