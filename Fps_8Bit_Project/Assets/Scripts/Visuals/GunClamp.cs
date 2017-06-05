using UnityEngine;
using System.Collections;

public class GunClamp : MonoBehaviour {

    private float rotateRate = 5f;
    private Vector3 angle;

    void Awake () {
        angle = transform.localEulerAngles;
        angle.z = Mathf.Clamp(angle.z + Time.deltaTime * rotateRate, 0f, 0f);
        angle.x = Mathf.Clamp(angle.x + Time.deltaTime * rotateRate, 0f, 0f);
        transform.localEulerAngles = angle;
    }

    void Update () {
        angle.z = Mathf.Clamp(angle.z + Time.deltaTime*rotateRate, 0f, 0f);
        angle.x = Mathf.Clamp(angle.x + Time.deltaTime * rotateRate, 0f, 0f);
        transform.localEulerAngles = angle;
    }
}
