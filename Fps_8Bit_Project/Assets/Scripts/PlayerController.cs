using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Header ("Speed and Sensitivity")]
    public float speed = 4f;
    public float sensitivity = 2f;

    CharacterController player;
    public GameObject cam;
    public GameObject gun;
    private GunAmmo gunAmmo;
    private AudioSource locknload;
    private Rigidbody rb;

    float moveFB;
    float moveLR;
    float moveUD;
    float rotX;
    float rotY;
    float jumpHeight = 500f;
    float gravity = 5f;
    float vSpeed = 0;

    private bool isFalling = false;
    public bool hasSpawned;

    void Start() {
        player = GetComponent<CharacterController>();
        gunAmmo = GetComponentInChildren<GunAmmo>();
        locknload = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        hasSpawned = true;
    }
    void Update() {
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

    void FixedUpdate () {
        if (Input.GetButtonDown("Jump")) {
            rb.AddRelativeForce(Vector3.up * moveUD);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ShotgunAmmo") {
            gunAmmo.shotgunAmmo = gunAmmo.shotgunAmmo + 20;
            locknload.Play();
            Destroy(other.gameObject);

        }
    }
}
