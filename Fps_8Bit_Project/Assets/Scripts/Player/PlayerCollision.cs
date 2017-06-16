using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    private PlayerStats pStats;
    private EnemyHealth pHealth;
    private GunAmmo gAmmo;

	void Start () {
        pStats = GetComponent<PlayerStats>();
        pHealth = GetComponent<EnemyHealth>();
        gAmmo = GetComponent<GunAmmo>();
	}

	void OnTriggerEnter (Collider other) {
        if (other.gameObject.name == "RedKey") {
            pStats.rKey = true;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.5f);
        }
        if (other.gameObject.tag == "HealthS") {
            pHealth.currentHealth = pHealth.currentHealth + 15;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "GreenArmour") {
            pHealth.currentArmour = pHealth.currentArmour + 100;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "BlueArmour") {
            pHealth.currentArmour = pHealth.currentArmour + 125;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "MGAmmo") {
            gAmmo.machineAmmo = gAmmo.machineAmmo + 25;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.tag == "SGAmmo") {
            gAmmo.shotgunAmmo = gAmmo.shotgunAmmo + 20;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.name == "MG_Pickup") {
            pStats.mGunFound = true;
            gAmmo.machineAmmo = gAmmo.machineAmmo + 20;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.name == "LG_Pickup") {
            pStats.lGunFound = true;
            gAmmo.launcherAmmo = gAmmo.launcherAmmo + 5;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.2f);
        }
        if (other.gameObject.name == "EndLevelToken") {
            pStats.levelFinished = true;
        }
    }
}
