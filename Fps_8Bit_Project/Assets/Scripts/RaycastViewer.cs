using UnityEngine;
using System.Collections;

public class RaycastViewer : MonoBehaviour {

    public float weaponRange = 50f;

    private Camera cam;

	void Start () {
        cam = GetComponentInParent<Camera>();
	}
	
	void Update () {
        Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(lineOrigin, cam.transform.forward * weaponRange, Color.green);
	}
}
