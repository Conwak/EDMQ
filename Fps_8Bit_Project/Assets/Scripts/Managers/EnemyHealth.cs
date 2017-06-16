using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    private PlayerStats pStats;

    public float currentHealth = 30;
    public float currentArmour = 25;
    public GameObject blood_p;
    public GameObject bloodWO_p;

    private Vector3 position;

    void Start () {
        if (this.gameObject.tag == "Player") {
            blood_p = null;
        }
    }

    void Update () {
        if (!pStats) {
            pStats = GameObject.FindObjectOfType<PlayerStats>();
        }

        if (currentHealth >= 200) {
            currentHealth = 200;
        }
        else if (currentHealth <= 0) {
            currentHealth = 0;
        }
        if (currentArmour >= 200) {
            currentArmour = 200;
        } 
        else if (currentArmour <= 0) {
            currentArmour = 0;
        }
    }

    public void Damage(float damageAmount) {
        if (currentArmour > 0) {
            currentHealth -= damageAmount / 4.8f;
            currentArmour -= damageAmount;
        }
        else if (currentArmour <= 0) {
            currentHealth -= damageAmount;
        }
        position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Instantiate(bloodWO_p, position, Quaternion.identity);
        if (currentHealth <= 0 && this.gameObject.tag == "Enemy") {
            Instantiate(blood_p, position, Quaternion.identity);
            gameObject.SetActive(false);
            if (gameObject.tag == "Enemy") {
                pStats.levelKills += 1;
                Destroy(gameObject, 1);
            }
            currentHealth = 0;
        }
    }
}
