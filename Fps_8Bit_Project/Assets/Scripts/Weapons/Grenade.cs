using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

    private GLauncherShoot lGun;
    private bool hasShot;
    private bool hasExploded;
    private AudioSource explosionAS;

    public GameObject explosion;

    [SerializeField]
    private Light redLight;

    private float lightTimer = 0.2f;
    private float explosiveTimer = 2f;

    [SerializeField]
    private Rigidbody rb;

    private float damage;
    public LayerMask lmEnemy;
    private float launchSpeed = 300f;

	void Awake () {
        lGun = GameObject.FindObjectOfType<GLauncherShoot>();
        explosionAS = GetComponent<AudioSource>();

        damage = Random.Range(40, 120);
    }

    void Update () {
        lightTimer -= Time.deltaTime;
        explosiveTimer -= Time.deltaTime;

        if (lightTimer < 0 && !hasExploded) {
            if (redLight.enabled == true) {
                redLight.enabled = false;
            } else if (redLight.enabled == false) {
                redLight.enabled = true;
            }
            lightTimer = 0.2f;
        }
    }

	void FixedUpdate () {
        if (lGun.shot && !hasShot) {
            rb.AddForce(lGun.gameObject.transform.forward * launchSpeed, ForceMode.Impulse);
            lGun.shot = false;
            hasShot = true;
        }
        if (explosiveTimer <= 0f && !hasExploded) {
            Instantiate(explosion, transform.position, Quaternion.identity);
            explosionAS.Play();
            hasExploded = true;
            Explosion(center: transform.position, radius: 5f, enemy: lmEnemy);
            bool obj = GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 1f);
        }
    }

    void Explosion(Vector3 center, float radius, LayerMask enemy) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, enemy);
        int i = 0;
        while (i < hitColliders.Length) {
            hitColliders[i].GetComponent<EnemyHealth>().Damage(damage);
            i++;
        }
    }
}
