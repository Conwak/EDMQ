using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    PlayerStats pStats;

    public GameObject door;
    public Transform startTarget;
    public Transform endTarget;
    public float speed = 0.05f;
    private bool doorPressed;

    void Start () {
        pStats = GameObject.FindObjectOfType<PlayerStats>();
    }

    void Update () {

        if (doorPressed) {
            door.transform.position = Vector3.MoveTowards(door.transform.position, endTarget.position, speed);
        } else {
            Close();
        }
    }

	void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player" && this.tag == "Door") {
            if (Input.GetButton("Action")) {
                doorPressed = true;
            }
        }
        else if (other.gameObject.tag == "Player" && this.tag == "RDoor") {
            if (Input.GetButton("Action") && pStats.rKey == true) {
                doorPressed = true;
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
