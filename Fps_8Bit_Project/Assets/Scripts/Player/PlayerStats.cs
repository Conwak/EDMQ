using UnityEngine;
using System.Collections;

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

    [HideInInspector]
    public float wp_shotgun = 1;
    [HideInInspector]
    public float wp_mgun = 1;
    [HideInInspector]
    public float wp_rocket = 1;
    [HideInInspector]
    public float wp_gLauncher = 1;

}
