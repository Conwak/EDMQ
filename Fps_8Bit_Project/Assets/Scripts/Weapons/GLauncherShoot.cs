using UnityEngine;
using System.Collections;

public class GLauncherShoot : MonoBehaviour {

    private PlayerController pController;
    private MuzzleFlash mFlash;

    static public float fireRate = 0.25f;
    static public float hitForce = 2f;
    public LayerMask playerLayer;
    private GunAmmo gunAmmo;
    public Transform gunEnd;
    public GameObject grenade;
    
    private Animator anim;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private float nextFire;

    [HideInInspector]
    public bool shot;

	void Start () {
        pController = GetComponentInParent<PlayerController>();
        mFlash = GetComponent<MuzzleFlash>();
        gunAudio = GetComponent<AudioSource>();
        gunAmmo = GetComponentInParent<GunAmmo>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (pController.inputX > 0 || pController.inputX < 0 || pController.inputY > 0 || pController.inputY < 0) {
            anim.SetBool("Walking", true);
        } else {
            anim.SetBool("Walking", false);
        }
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && gunAmmo.launcherAmmo > 0) {
            Shoot();
        }
    }

    public void Shoot () {
        shot = true;
        Instantiate(grenade, gunEnd.position, grenade.transform.rotation);
        anim.SetBool("Shot", true);
        gunAmmo.launcherAmmo = gunAmmo.launcherAmmo - 1;
        nextFire = Time.time + fireRate;

        StartCoroutine(ShotEffect());

    }

    private IEnumerator ShotEffect () {
        gunAudio.Play();
        mFlash.MuzzleShoot();
        yield return shotDuration;
        mFlash.MuzzleOff();
        anim.SetBool("Shot", false);
    }
}
