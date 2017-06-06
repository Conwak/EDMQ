using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [HideInInspector]
    public float shade = 0;

    [Header("Keys")]
    public bool rKey; public bool bKey;

    private float vitality = 1;
    private float strength = 1;
    private float dexterity = 1;
    private float intelligence = 1;
    private float faith = 1;

    private string helm;
    private string body;
    private string legs;
    private string boots;

    [Header("Weapons")]
    public GameObject sGun;
    public GameObject mGun;

    [Header("UI")]
    public GameObject ui_shotgun;
    public GameObject ui_machinegun;

    private Color sgAlpha;
    private Color mgAlpha;

    [HideInInspector]
    public float wp_shotgun = 1;
    [HideInInspector]
    public float wp_mgun = 1;
    [HideInInspector]
    public float wp_rocket = 1;
    [HideInInspector]
    public float wp_gLauncher = 1;

    void Start () {
        ui_shotgun = GameObject.Find("Canvas/1./UI_Shotgun");
        ui_machinegun = GameObject.Find("Canvas/2./UI_MachineGun");

        sgAlpha = ui_shotgun.GetComponent<Image>().color;
        mgAlpha = ui_machinegun.GetComponent<Image>().color;

        mgAlpha.a = 0.5f;
    }

    void Update () {
        if (Input.GetButtonDown("Weapon01") && !sGun.activeSelf) {
            sGun.SetActive(true);
            mGun.SetActive(false);
        }
        else if (Input.GetButtonDown("Weapon02") && !mGun.activeSelf) {
            mGun.SetActive(true);
            sGun.SetActive(false);
        }

        if (sGun.activeSelf) {
            sgAlpha.a = 1.0f;
        } else {
            sgAlpha.a = 0.5f;
        }

        if (mGun.activeSelf) {
            mgAlpha.a = 1.0f;
        } else {
            mgAlpha.a = 0.5f;
        }

        ui_shotgun.GetComponent<Image>().color = sgAlpha;
        ui_machinegun.GetComponent<Image>().color = mgAlpha;
    }
}
