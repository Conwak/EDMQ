using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    private PlayerStats pStats;

	void Start () {
        pStats = GetComponent<PlayerStats>();
	}

	void OnTriggerEnter (Collider other) {
        if (other.gameObject.name == "RedKey") {
            pStats.rKey = true;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 0.5f);
        }
    }
}
