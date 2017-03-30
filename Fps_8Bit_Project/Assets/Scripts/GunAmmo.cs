using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunAmmo : MonoBehaviour {

    public int shotgunAmmo = 25;
    private EnemyHealth playerHealth;

    private GameObject healthObj;
    private GameObject ammoObj;
    public Text healthText;
    public Text ammoText;

	void Start () {
        playerHealth = GetComponentInParent<EnemyHealth>();

        ammoObj = GameObject.Find("Ammo");
        healthObj = GameObject.Find("Health");
        ammoText = ammoObj.GetComponent<Text>();
        healthText = healthObj.GetComponent<Text>();
	}

    void Update () {
        ammoText.text = shotgunAmmo.ToString("Ammo:0");
        healthText.text = playerHealth.currentHealth.ToString("Health:0");
    }
}
