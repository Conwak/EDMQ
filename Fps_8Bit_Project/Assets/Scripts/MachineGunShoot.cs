using UnityEngine;
using System.Collections;

public class MachineGunShoot : MonoBehaviour {

    [HideInInspector]
    static public int gunDamage = 18;
    static public float fireRate = 0.1f;
    static public float weaponRange = 100f;
    static public float hitForce = 100f;
    static public bool shooting;
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
        if (Input.GetButton ("Fire1") && Time.time > nextFire && gunAmmo.machineAmmo > 0) {
            Instantiate(sgShell, shellSpawn.transform.position, sgShell.transform.rotation);
            gunAmmo.machineAmmo = gunAmmo.machineAmmo - 1;
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange)) {
                EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                if (health != null) {
                    health.Damage(gunDamage);
                }
                if (hit.rigidbody != null) {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
                if (hit.collider.tag != "Enemy") {
                    Instantiate(dust_p, hit.point, dust_p.transform.rotation);
                    Instantiate(bDecal, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
                }
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
        yield return shotDuration;
    }
}
