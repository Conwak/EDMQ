using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {

    [Header("Movement")]
    public float walkSpeed = 6f;
    public float runSpeed = 4f;
    public bool limitDiagonalSpeed = false;
    public bool toggleRun = false;
    public float jumpSpeed = 6f;
    public float gravity = 14f;

    [Header("ScriptInfo")]
    [HideInInspector]
    public float inputX;
    [HideInInspector]
    public float inputY;

    [HideInInspector]
    public bool isUnderwater;
    private bool inWater;

    [Header("Mouse")]
    private float rotX;
    private float rotY;
    public float xMouseSensitivity = 60f;
    public float yMouseSensitivity = 60f;

    [Header("Other")]
    public float antiBunnyHop = 1;
    public bool airControl = true;

    public float fallingDamageThreshold = 0f;
    public bool slideWhenOverSlopeLimit = false;
    public bool slideOnTaggedObjects = false;
    public float slideSpeed = 0f;
    public float antiBumpFactor = 0.75f;

    public Transform cam;
    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    private CharacterController controller;
    private float speed;
    private RaycastHit hit;
    private float fallStartLevel;
    private bool falling;
    private float slideLimit;
    private float rayDistance;
    private Vector3 contactPoint;
    private bool playerControl = false;
    private float jumpTimer;

    void Start () {
        controller = GetComponent<CharacterController>();
        speed = runSpeed;
        rayDistance = controller.height * 0.5f + controller.radius;
        jumpTimer = antiBunnyHop;
    }

    void FixedUpdate () {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? .7071f : 1.0f;

        if (grounded) {
            bool sliding = false;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, rayDistance)) {
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit) {
                    sliding = false;
                } else {
                    Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, out hit);
                    if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit) 
                        sliding = true;
                }
            }

            if (falling) {
                falling = false;
            }

            if (!toggleRun)
                speed = Input.GetButton("Run") ? runSpeed : walkSpeed;
            if ((sliding && slideWhenOverSlopeLimit)) {
                Vector3 hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize(ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
                playerControl = false;
            }
            else {
                moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
                moveDirection = transform.TransformDirection(moveDirection) * speed;
                playerControl = true;
            }
            if (!Input.GetButton("Jump"))
                jumpTimer++;
            else if (jumpTimer >= antiBunnyHop) {
                moveDirection.y = jumpSpeed;
                jumpTimer = 0;
            }
        } else {
            if (!falling) {
                falling = true;
                fallStartLevel = transform.position.y;
            }

            if (playerControl) {
                moveDirection.x = inputX * speed * inputModifyFactor;
                moveDirection.z = inputY * speed * inputModifyFactor;
                moveDirection = transform.TransformDirection(moveDirection);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
    }

    void Update () {
        rotX -= Input.GetAxis("Mouse Y") * xMouseSensitivity;
        rotY += Input.GetAxis("Mouse X") * yMouseSensitivity;

        if (rotX < -90)
            rotX = -90;
        else if (rotX > 90)
            rotX = 90;

        transform.rotation = Quaternion.Euler(0, rotY, 0);
        cam.rotation = Quaternion.Euler(rotX, rotY, 0);
    }

    void OnControllerColliderHit (ControllerColliderHit hit) {
        contactPoint = hit.point;
    }

    void OnTriggerStay (Collider other) {
        if (other.tag == "Water") {
            isUnderwater = true;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.tag == "Water") {
            isUnderwater = false;
        }
    }

}
