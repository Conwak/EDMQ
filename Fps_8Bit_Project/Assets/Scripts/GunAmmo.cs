using UnityEngine;
using System.Collections;

public class GunAmmo : MonoBehaviour {

    public int shotgunAmmo;

	void Start () {
	
	}
	
    void Rotation () {
        transform.Rotate(Vector3.up * 2.5f);
    }

	void Update () {
        Rotation();
	}
}
