﻿using UnityEngine;
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
    private EnemyHealth playerHealth;
    private AudioSource jumpAudio;
    private Rigidbody rb;

    float moveFB;
    float moveLR;
    float moveUD;
    float rotX;
    float rotY;
    bool inWater;
    float jumpHeight = 4f;
    [HideInInspector]
    public float gravity = 10f;
    float vSpeed = 0;

    bool grounded = false;
    Vector3 moveDirection = Vector3.zero;

    public bool hasSpawned;

    void Start() {
        player = GetComponent<CharacterController>();
        gunAmmo = GetComponentInChildren<GunAmmo>();
        playerHealth = GetComponent<EnemyHealth>();
        jumpAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        hasSpawned = true;
    }
    void Update() {
        WeaponSwitch();

        if (Input.GetButtonDown("Jump")) {
            jumpAudio.Play();
        }

        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) {
            shotgun.GetComponent<Animator>().SetBool("Walking", true);
        } else {
            shotgun.GetComponent<Animator>().SetBool("Walking", false);
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

        if (!inWater) {
            CharacterController controller = (CharacterController)GetComponent(typeof(CharacterController));
            CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
            grounded = (flags & CollisionFlags.CollidedBelow) != 0;
        } else {
            CharacterController controller = (CharacterController)GetComponent(typeof(CharacterController));
            CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
            grounded = (flags & CollisionFlags.None) == 0;
        }

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
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "MGAmmo") {
            gunAmmo.machineAmmo = gunAmmo.machineAmmo + 25;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "GreenArmour") {
            playerHealth.currentArmour = playerHealth.currentArmour + 100;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "HealthS") {
            playerHealth.currentHealth = playerHealth.currentHealth + 15;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "Lava") {
            GetComponent<EnemyHealth>().currentHealth--;
        }
    }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Lava") {
            GetComponent<EnemyHealth>().currentHealth--;
        }
        if (other.gameObject.tag == "Water") {
            inWater = true;
            gravity = 50f;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Water") {
            inWater = false;
            gravity = 10f;
        }
    }
}
