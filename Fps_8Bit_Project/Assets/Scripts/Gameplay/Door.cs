using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private PlayerStats pStats;

    [Header("DoorProperties")]
    public GameObject door;
    public Transform startTarget;
    public Transform endTarget;
    public float speed = 0.05f;
    public AudioSource doorAS;
    private bool doorPressed;

    [Header("SecretProperties")]
    public GameObject sDoorText;
    public AudioSource sDoorAS;

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
                doorAS.Play();
            }
        } else if (other.gameObject.tag == "Player" && this.tag == "SDoor") {
            if (Input.GetButton("Action")) {
                doorPressed = true;
                sDoorText.SetActive(true);
                doorAS.Play();
                sDoorAS.Play();
            }
        } else if (other.gameObject.tag == "Player" && this.tag == "RDoor") {
            if (Input.GetButton("Action") && pStats.rKey == true) {
                doorPressed = true;
                doorAS.Play();
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player" && this.tag == "SDoor") {
            sDoorText.SetActive(false);
            StartCoroutine(DoorClose());
        } else if (other.gameObject.tag == "Player") {
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
