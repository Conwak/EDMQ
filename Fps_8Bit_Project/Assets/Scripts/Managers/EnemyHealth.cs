using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float currentHealth = 30;
    public int currentArmour = 25;
    public GameObject blood_p;
    public GameObject bloodWO_p;

    private Vector3 position;

    void Start () {
        if (this.gameObject.tag == "Player") {
            blood_p = null;
        }
    }

    public void Damage(float damageAmount) {
        currentHealth -= damageAmount;
        position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Instantiate(bloodWO_p, position, Quaternion.identity);
        if (currentHealth <= 0 && this.gameObject.tag == "Enemy") {
            Instantiate(blood_p, position, Quaternion.identity);
            gameObject.SetActive(false);
            if (gameObject.tag == "Enemy") {
                Destroy(gameObject, 1);
            }
            currentHealth = 0;
        }
    }
}
