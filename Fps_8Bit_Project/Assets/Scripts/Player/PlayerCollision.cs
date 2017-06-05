using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    private PlayerStats pStats;
    private EnemyHealth pHealth;

	void Start () {
        pStats = GetComponent<PlayerStats>();
        pHealth = GetComponent<EnemyHealth>();
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
    }
}
