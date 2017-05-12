using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour {

    public Transform startTarget;
    public Transform endTarget;
    public float speed;
    private bool liftPressed;

    void Update() {
        if (liftPressed && transform.position != endTarget.position) {
            transform.position = Vector3.MoveTowards(transform.position, endTarget.position, speed);
        }
        if (!liftPressed) {
            GoUp();
        }
        if (transform.position == endTarget.position) {
            StartCoroutine(LiftUp());
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (Input.GetButton("Action") && transform.position == startTarget.position) {
                liftPressed = true;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player" && transform.position == endTarget.position) {
            StartCoroutine(LiftUp());
        }
    }

    void GoUp () {
        if (transform.position == startTarget.position)
            return;
        transform.position = Vector3.MoveTowards(transform.position, startTarget.position, speed);
    }

    IEnumerator LiftUp () {
        yield return new WaitForSeconds(5f);
        liftPressed = false;
    }
}
