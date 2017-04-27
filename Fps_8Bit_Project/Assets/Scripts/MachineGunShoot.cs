using UnityEngine;
using System.Collections;

public class MachineGunShoot : MonoBehaviour {

    [HideInInspector]
    static public int gunDamage = 9;
    static public float fireRate = 0.1f;
    static public float weaponRange = 100f;
    static public float hitForce = 100f;
    static public bool shooting;
    private GunAmmo gunAmmo;
    public Transform gunEnd;

    public GameObject sgShell;
    public Transform shellSpawn;
    private Animator anim;

    private Camera cam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;

	void Start () {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        cam = GetComponentInParent<Camera>();
        gunAmmo = GetComponentInParent<GunAmmo>();
        anim = GetComponent<Animator>();
    }
	
	void Update () {
        if (Input.GetButton ("Fire1") && Time.time > nextFire && gunAmmo.machineAmmo > 0) {
            Instantiate(sgShell, shellSpawn.transform.position, sgShell.transform.rotation);
            gunAmmo.machineAmmo = gunAmmo.machineAmmo - 1;
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange)) {
                laserLine.SetPosition(1, hit.point);
                EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                if (health != null) {
                    health.Damage(gunDamage);
                }
                if (hit.rigidbody != null) {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            } else {
                laserLine.SetPosition(1, rayOrigin + (cam.transform.forward * weaponRange));
            }
        }
        if (Input.GetButton ("Fire1")) {
            anim.SetBool("Shot", true);
        }
        if (Input.GetButtonUp("Fire1")) {
            anim.SetBool("Shot", false);
        }
	}

    private IEnumerator ShotEffect () {
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
