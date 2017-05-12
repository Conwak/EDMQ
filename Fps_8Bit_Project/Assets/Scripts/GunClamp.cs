using UnityEngine;
using System.Collections;

public class GunClamp : MonoBehaviour {

    private float rotationZ = 0f;
    private float rotationY = 0f;
    private float sensitivityZ = 2f;

    void Update () {
        rotationZ += Input.GetAxis("Mouse Y");
        rotationZ = Mathf.Clamp(0, 0, 0);

        rotationY += Input.GetAxis("Mouse X");
        rotationY = Mathf.Clamp(0, 0, 0);

        transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, -rotationZ);
    }
}
