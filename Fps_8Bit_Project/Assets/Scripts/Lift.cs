using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour {

    public Transform lift;
    public Transform startPos;
    public Transform endPos;
    private float speed = 1f;

    private float startTime;
    private float journeyLength;

    void Start() {
        journeyLength = Vector3.Distance(startPos.position, endPos.position);
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (Input.GetButton("Action")) {
                float fracJourney = speed / journeyLength;
                if (lift.position.y == startPos.position.y) {
                    lift.transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);
                } else {
                    lift.transform.position = Vector3.Lerp(endPos.position, startPos.position, fracJourney);
                }
            }
        }
    }
}
