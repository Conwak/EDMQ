using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunAmmo : MonoBehaviour {

    [Header("Ammo")]
    public int shotgunAmmo = 25;
    public int machineAmmo = 25;
    private EnemyHealth playerHealth;
    private PlayerStats playerStats;

    [Header("Text")]
    private GameObject healthObj;
    private GameObject ammoObj;
    private GameObject armourObj;

    private Text healthText;
    private Text ammoText;
    private Text armourText;

	void Start () {
        playerHealth = GetComponent<EnemyHealth>();
        playerStats = GetComponent<PlayerStats>();

        healthObj = GameObject.Find("Canvas/HealthText");
        ammoObj = GameObject.Find("Canvas/AmmoText");
        armourObj = GameObject.Find("Canvas/ArmourText");

        healthText = healthObj.GetComponent<Text>();
        ammoText = ammoObj.GetComponent<Text>();
        armourText = armourObj.GetComponent<Text>();
	}

    void Update () {
        if (playerStats.sGun.activeSelf == true) {
            ammoText.text = shotgunAmmo.ToString("0");
        }
        else if (playerStats.mGun.activeSelf == true) {
            ammoText.text = machineAmmo.ToString("0");
        }
        healthText.text = playerHealth.currentHealth.ToString("0");
        armourText.text = playerHealth.currentArmour.ToString("0");
    }
}
