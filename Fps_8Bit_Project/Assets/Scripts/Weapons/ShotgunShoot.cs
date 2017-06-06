using UnityEngine;
using System.Collections;

public class ShotgunShoot : MonoBehaviour {

    private PlayerController pController;
    private MuzzleFlash mFlash;

    static public float gunDamage = 4f;
    static public float gibDamage = 6f;
    private int pellets = 6;
    static public float fireRate = 0.25f;
    static public float weaponRange = 78f;
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
        pController = GetComponentInParent<PlayerController>();
        mFlash = GetComponent<MuzzleFlash>();
        gunAudio = GetComponent<AudioSource>();
        cam = GetComponentInParent<Camera>();
        gunAmmo = GetComponentInParent<GunAmmo>();
        anim = GetComponent<Animator>();
    }
	
	void Update () {
        if (pController.inputX > 0 || pController.inputX < 0 || pController.inputY > 0 || pController.inputY < 0) {
            anim.SetBool("Walking", true);
        } else {
            anim.SetBool("Walking", false);
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && gunAmmo.shotgunAmmo > 0) {
            Instantiate(sgShell, shellSpawn.transform.position, sgShell.transform.rotation);
            anim.SetBool("Shot", true);
            gunAmmo.shotgunAmmo = gunAmmo.shotgunAmmo - 1;
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;  

            for (int i = 0; i < pellets; i++) {
                float randomX = Random.Range(-0.5f, 0.5f);
                float randomY = Random.Range(-0.5f, 0.5f);
                rayOrigin.y += randomY;
                rayOrigin.x += randomX;

                if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange, playerLayer)) {
                    EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();

                    if (hit.collider.tag == "Enemy" && hit.distance > 4f) {
                        health.Damage(gunDamage);
                        hit.collider.GetComponent<Enemy_Golem>().hit = true;
                    } else if (hit.collider.tag == "Enemy" && hit.distance < 4f) {
                        health.Damage(gibDamage);
                        hit.collider.GetComponent<Enemy_Golem>().hit = true;
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
        mFlash.MuzzleShoot();
        yield return shotDuration;
        mFlash.MuzzleOff();
        anim.SetBool("Shot", false);
    }
}
