﻿using UnityEngine;
using System.Collections;

public class ShotgunShoot : MonoBehaviour {

    static public int gunDamage = 24;
    private int pellets = 6;
    static public float fireRate = 0.25f;
    static public float weaponRange = 7.8f;
    static public float hitForce = 100f;
    public LayerMask playerLayer;
    private GunAmmo gunAmmo;
    public Transform gunEnd;

    public GameObject sgShell;
    public GameObject dust_p;
    public GameObject bDecal;
    public Transform shellSpawn;
    private Animator anim;

    private Camera cam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private float nextFire;

	void Start () {
        gunAudio = GetComponent<AudioSource>();
        cam = GetComponentInParent<Camera>();
        gunAmmo = GetComponentInParent<GunAmmo>();
        anim = GetComponent<Animator>();
    }
	
	void Update () {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && gunAmmo.shotgunAmmo > 0) {
            Instantiate(sgShell, shellSpawn.transform.position, sgShell.transform.rotation);
            anim.SetBool("Shot", true);
            gunAmmo.shotgunAmmo = gunAmmo.shotgunAmmo - 1;
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;  

            for (int i = 0; i < pellets; i++) {
                float randomX = Random.Range(-0.2f, 0.2f);
                float randomY = Random.Range(-0.2f, 0.2f);
                rayOrigin.y += randomY;
                rayOrigin.x += randomX;

                if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange, playerLayer)) {
                    EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                    if (health != null) {
                        health.Damage(gunDamage);
                    }
                    if (hit.collider.tag != "Enemy") {
                        Instantiate(dust_p, hit.point, dust_p.transform.rotation);
                        Instantiate(bDecal, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
                    }
                    if (hit.rigidbody != null) {
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                    }
                }
            }
        }
	}

    private IEnumerator ShotEffect () {
        gunAudio.Play();
        yield return shotDuration;
        anim.SetBool("Shot", false);
    }
}
