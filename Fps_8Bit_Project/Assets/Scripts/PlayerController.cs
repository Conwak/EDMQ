using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [Header ("Speed and Sensitivity")]
    public float speed = 4f;
    public float jSpeed = 2f;
    public float sensitivity = 2f;

    [Header("Guns")]
    public GameObject shotgun;
    public GameObject machineGun;

    CharacterController player;

    [Header("Other")]
    public GameObject cam;
    public GameObject gun;
    private GunAmmo gunAmmo;
    private AudioSource jumpAudio;
    private Rigidbody rb;

    float moveFB;
    float moveLR;
    float moveUD;
    float rotX;
    float rotY;
    float jumpHeight = 4f;
    float gravity = 10f;
    float vSpeed = 0;

    bool grounded = false;
    Vector3 moveDirection = Vector3.zero;

    public bool hasSpawned;

    void Start() {
        player = GetComponent<CharacterController>();
        gunAmmo = GetComponentInChildren<GunAmmo>();
        jumpAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        hasSpawned = true;
    }
    void Update() {
        WeaponSwitch();

        if (Input.GetButtonDown("Jump")) {
            jumpAudio.Play();
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

        movement = transform.rotation * movement;
        player.Move(movement * Time.deltaTime);
    }

    void FixedUpdate () {
        if (grounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= jSpeed;

            if (Input.GetButton("Jump")) moveDirection.y = jumpHeight;
        }
        moveDirection.y -= gravity * Time.deltaTime;

        CharacterController controller = (CharacterController)GetComponent(typeof(CharacterController));
        CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
        grounded = (flags & CollisionFlags.CollidedBelow) != 0;
    }

    void WeaponSwitch () {
        if (Input.GetKeyDown("1")) {
            shotgun.SetActive(true);
            machineGun.SetActive(false);
        }
        else if (Input.GetKeyDown("2")) {
            shotgun.SetActive(false);
            machineGun.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ShotgunAmmo") {
            gunAmmo.shotgunAmmo = gunAmmo.shotgunAmmo + 20;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 2);
        }
        if (other.gameObject.tag == "Lava") {
            GetComponent<EnemyHealth>().currentHealth--;
        }
    }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Lava") {
            GetComponent<EnemyHealth>().currentHealth--;
        }
    }
}
