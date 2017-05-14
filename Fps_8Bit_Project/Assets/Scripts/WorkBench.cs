using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorkBench : MonoBehaviour {

    private PlayerStats pStats;

    public GameObject[] wb_buttons;

    private float s_shotgun = 673;
    private float s_mgun = 673;
    private float s_rocket = 673;
    private float s_gLauncher = 673;

    void Start () {
        pStats = GameObject.FindObjectOfType<PlayerStats>();
	}

    void OnTriggerStay (Collider other) {
        if (other.tag == "Player" && Input.GetButton("Action")) {
            foreach (GameObject obj in wb_buttons) {
                obj.SetActive(true);
            }
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.tag == "Player") {
            foreach (GameObject obj in wb_buttons) {
                obj.SetActive(false);
            }
        }
    }

    void ShotgunPurchased () {
        if (pStats.shade >= s_shotgun) {
            pStats.wp_shotgun = pStats.wp_shotgun + 1;
            pStats.shade = pStats.shade - s_shotgun;
            s_shotgun = Mathf.Pow(pStats.wp_shotgun, 1.5f);
        }
    }

    void MgunPurchased () {
        if (pStats.shade >= s_mgun) {
            pStats.wp_mgun = pStats.wp_mgun + 1;
            pStats.shade = pStats.shade - s_mgun;
            s_mgun = Mathf.Pow(pStats.wp_mgun, 1.5f);
        }
    }

    void RocketPurchased () {
        if (pStats.shade >= s_rocket) {
            pStats.wp_rocket = pStats.wp_rocket + 1;
            pStats.shade = pStats.shade - s_rocket;
            s_rocket = Mathf.Pow(pStats.wp_rocket, 1.5f);
        }
    }

    void LauncherPurchased () {
        if (pStats.shade >= s_gLauncher) {
            pStats.wp_gLauncher = pStats.wp_gLauncher + 1;
            pStats.shade = pStats.shade - s_gLauncher;
            s_gLauncher = Mathf.Pow(pStats.wp_gLauncher, 1.5f);
        }
    }
}
