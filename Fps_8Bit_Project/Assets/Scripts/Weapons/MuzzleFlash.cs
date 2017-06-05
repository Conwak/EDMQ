using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {

    public Light muzzleLight;
    public Renderer muzzleFlash;

	void Start () {
	
	}

	public void MuzzleShoot() {
        muzzleLight.enabled = true;
        muzzleFlash.enabled = true;
    }

    public void MuzzleOff() {
        muzzleLight.enabled = false;
        muzzleFlash.enabled = false;
    }
}
