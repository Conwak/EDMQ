using UnityEngine;
using System.Collections;

public class ShotgunShoot : MonoBehaviour {

    [HideInInspector]
    static public int gunDamage = 24;
    static public float fireRate = 0.25f;
    static public float weaponRange = 12f;
    static public float hitForce = 100f;
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
        gunAmmo = GetComponent<GunAmmo>();
        anim = GetComponent<Animator>();
    }
	
	void Update () {
        if (Input.GetButtonDown ("Fire1") && Time.time > nextFire) {
            Instantiate(sgShell, shellSpawn.transform.position, sgShell.transform.rotation);
            anim.SetBool("Shot", true);
            gunAmmo.shotgunAmmo = gunAmmo.shotgunAmmo - 1;
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
	}

    private IEnumerator ShotEffect () {
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        anim.SetBool("Shot", false);
        laserLine.enabled = false;
    }
}
