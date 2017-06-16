using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public PlayerStats pStats;

    [Header("DoorProperties")]
    public GameObject door;
    public Transform startTarget;
    public Transform endTarget;
    public float speed = 0.05f;
    public AudioSource doorAS;
    private bool doorPressed;
    private bool sDoorPressed;

    [Header("SecretProperties")]
    public GameObject sDoorText;
    public AudioSource sDoorAS;

    void Start () {
        
    }

    void Update () {

        if (!pStats) {
            pStats = GameObject.FindObjectOfType<PlayerStats>();
        }

        if (doorPressed) {
            door.transform.position = Vector3.MoveTowards(door.transform.position, endTarget.position, speed);
        } else {
            Close();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && this.tag == "EDoor" && pStats.rKey) {
                doorPressed = true;
        }
    }

    void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player" && this.tag == "Door") {
            if (Input.GetButton("Action")) {
                doorPressed = true;
                doorAS.Play();
            }
        } else if (other.gameObject.tag == "Player" && this.tag == "SDoor") {
            if (Input.GetButton("Action") && !sDoorPressed) {
                doorPressed = true;
                sDoorPressed = true;
                pStats.levelSecrets += 1;
                sDoorText.SetActive(true);
                doorAS.Play();
                sDoorAS.Play();
            } else if (Input.GetButton("Action") && sDoorPressed) {
                doorPressed = true;
                doorAS.Play();
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
            StartCoroutine(DoorClose());
        } else if (other.gameObject.tag == "Player" && this.tag != "EDoor") {
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
        if (this.tag == "SDoor") {
            sDoorText.SetActive(false);
        }
        doorPressed = false;
    }
}
