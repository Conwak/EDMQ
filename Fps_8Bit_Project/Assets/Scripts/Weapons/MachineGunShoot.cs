using UnityEngine;
using System.Collections;

public class MachineGunShoot : MonoBehaviour {

    private PlayerController pController;
    private MuzzleFlash mFlash;
    private Pause pause;

    [HideInInspector]
    public int gunDamage = 9;
    public float fireRate = 0.15f;
    public float weaponRange = 229f;
    public float hitForce = 100f;
    public bool shooting;
    public LayerMask decalLayer;
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
        pause = GameObject.FindObjectOfType<Pause>();
    }
	
	void Update () {
        if (pController.inputX > 0 || pController.inputX < 0 || pController.inputY > 0 || pController.inputY < 0) {
            anim.SetBool("Walking", true);
        } else {
            anim.SetBool("Walking", false);
        }
        if (Input.GetButton ("Fire1") && Time.time > nextFire && gunAmmo.machineAmmo > 0 && !pause.paused) {
            Instantiate(sgShell, shellSpawn.transform.position, Quaternion.identity);
            gunAmmo.machineAmmo = gunAmmo.machineAmmo - 1;
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange, decalLayer)) {
                EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                if (hit.collider.tag == "Enemy") {
                    health.Damage(gunDamage);
                    hit.collider.GetComponent<Enemy_Golem>().hit = true;
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
        mFlash.MuzzleShoot();
        yield return shotDuration;
        mFlash.MuzzleOff();
    }
}
