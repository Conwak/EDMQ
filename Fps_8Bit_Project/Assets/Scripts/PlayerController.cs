using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 4f;
    public float sprintSpeed = 2f;
    public float sensitivity = 2f;

    CharacterController player;
    public GameObject cam;
    public GameObject gun;
    private GunAmmo gunAmmo;
    private AudioSource locknload;

    float moveFB;
    float moveLR;
    float moveUD;
    float rotX;
    float rotY;
    float gravity = 9.8f;
    float vSpeed = 0;

    public bool hasSpawned;

    void Start() {
        player = GetComponent<CharacterController>();
        gunAmmo = GetComponentInChildren<GunAmmo>();
        locknload = GetComponent<AudioSource>();
        hasSpawned = true;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            Sprint();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = 4f;
        }

        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;

        rotY = Mathf.Clamp(rotY, -60f, 60f);

        Vector3 movement = new Vector3(moveLR, moveUD, moveFB);
        transform.Rotate(0, rotX, 0);
        cam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        gun.transform.localRotation = Quaternion.Euler(rotX, 90, rotY);

        vSpeed -= gravity * Time.deltaTime;
        moveUD = vSpeed;

        movement = transform.rotation * movement;
        player.Move(movement * Time.deltaTime);
    }

    void Sprint () {
        speed = speed * sprintSpeed;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ShotgunAmmo") {
            gunAmmo.shotgunAmmo = gunAmmo.shotgunAmmo + 20;
            locknload.Play();
            Destroy(other.gameObject);

        }
    }
}
