using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    private Pause pause;

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
    public GameObject lGun;

    [HideInInspector]
    public bool mGunFound;
    [HideInInspector]
    public bool lGunFound;

    [Header("UI")]
    public GameObject ui_shotgun;
    public GameObject ui_machinegun;
    public GameObject ui_launcher;

    private Color sgAlpha;
    private Color mgAlpha;
    private Color lgAlpha;

    [HideInInspector]
    public float wp_shotgun = 1;
    [HideInInspector]
    public float wp_mgun = 1;
    [HideInInspector]
    public float wp_rocket = 1;
    [HideInInspector]
    public float wp_gLauncher = 1;

    void Start () {
        pause = GameObject.FindObjectOfType<Pause>();
        ui_shotgun = GameObject.Find("Canvas/1./UI_Shotgun");
        ui_machinegun = GameObject.Find("Canvas/2./UI_MachineGun");
        ui_launcher = GameObject.Find("Canvas/3./UI_GLauncher");

        sgAlpha = ui_shotgun.GetComponent<Image>().color;
        mgAlpha = ui_machinegun.GetComponent<Image>().color;
        lgAlpha = ui_launcher.GetComponent<Image>().color;

        mgAlpha.a = 0.5f;
        lgAlpha.a = 0.5f;
    }

    void Update () {
        if (Input.GetButtonDown("Weapon01") && !sGun.activeSelf && !pause.paused) {
            sGun.SetActive(true);
            mGun.SetActive(false);
            lGun.SetActive(false);
        }
        else if (Input.GetButtonDown("Weapon02") && !mGun.activeSelf && !pause.paused && mGunFound) {
            mGun.SetActive(true);
            sGun.SetActive(false);
            lGun.SetActive(false);
        } 
        else if (Input.GetButtonDown("Weapon03") && !lGun.activeSelf && !pause.paused && lGunFound) {
            lGun.SetActive(true);
            mGun.SetActive(false);
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

        if (lGun.activeSelf) {
            lgAlpha.a = 1.0f;
        } else {
            lgAlpha.a = 0.5f;
        }

        ui_shotgun.GetComponent<Image>().color = sgAlpha;
        ui_machinegun.GetComponent<Image>().color = mgAlpha;
        ui_launcher.GetComponent<Image>().color = lgAlpha;
    }
}
