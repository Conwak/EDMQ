using UnityEngine;
using System.Collections;

public class GunClamp : MonoBehaviour {

    private float rotationZ = 0f;
    private float sensitivityZ = 2f;

    void Update () {
        rotationZ += Input.GetAxis("Mouse Y");
        rotationZ = Mathf.Clamp(0, 0, 0);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -rotationZ);
    }
}
