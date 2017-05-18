using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float currentHealth = 3;
    public int currentArmour = 25;
    public GameObject blood_p;
    public GameObject bloodWO_p;

    public Vector3 position;

    public void Damage(int damageAmount) {
        currentHealth -= damageAmount;
        position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Instantiate(bloodWO_p, position, Quaternion.identity);
        if (currentHealth <= 0) {
            Instantiate(blood_p, position, Quaternion.identity);
            gameObject.SetActive(false);
            if (gameObject.tag == "Enemy") {
                Destroy(gameObject, 1);
            }
            currentHealth = 0;
        }
    }
}
