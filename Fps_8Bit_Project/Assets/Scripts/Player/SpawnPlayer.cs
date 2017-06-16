using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

    private GameObject iPlayer;
    private EnemyHealth playerHealth;

    public GameObject player;
    public Transform spawn;

	void Start () {
        Instantiate(player, spawn.position, Quaternion.identity);	
	}

    void Update() {
        if (!iPlayer) {
            iPlayer = GameObject.FindGameObjectWithTag("Player");
            playerHealth = iPlayer.GetComponent<EnemyHealth>();
        }

        if (playerHealth.currentHealth <= 0) {
            Destroy(iPlayer);
            Instantiate(player, spawn.position, Quaternion.identity);
        }
    }

}
