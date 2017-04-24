using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunAmmo : MonoBehaviour {

    public int shotgunAmmo = 25;
    public int machineAmmo = 25;
    private EnemyHealth playerHealth;
    private PlayerController playerController;

    private GameObject healthObj;
    private GameObject ammoObj;
    private GameObject armourObj;
    public Text healthText;
    public Text ammoText;
    private Text armourText;

	void Start () {
        playerHealth = GetComponent<EnemyHealth>();
        playerController = GetComponent<PlayerController>();

        ammoObj = GameObject.Find("Ammo");
        healthObj = GameObject.Find("Health");
        armourObj = GameObject.Find("Armour");
        ammoText = ammoObj.GetComponent<Text>();
        healthText = healthObj.GetComponent<Text>();
        armourText = armourObj.GetComponent<Text>();
	}

    void Update () {
        if (playerController.shotgun.activeSelf == true) {
            ammoText.text = shotgunAmmo.ToString("0");
        }
        else if (playerController.machineGun.activeSelf == true) {
            ammoText.text = machineAmmo.ToString("0");
        }
        healthText.text = playerHealth.currentHealth.ToString("0");
        armourText.text = playerHealth.currentArmour.ToString("0");
    }
}
