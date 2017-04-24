using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int currentHealth = 3;
    public int currentArmour = 25;
    public GameObject blood_p;

    public Vector3 position;

    public void Damage(int damageAmount) {
        currentHealth -= damageAmount;
        position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Instantiate(blood_p, position, Quaternion.identity);
        if (currentHealth <= 0) {
            gameObject.SetActive(false);
            currentHealth = 0;
        }
    }
}
