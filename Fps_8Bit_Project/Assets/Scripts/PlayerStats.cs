using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    [HideInInspector]
    public float shade = 0;

    private float vitality = 1;
    private float strength = 1;
    private float dexterity = 1;
    private float intelligence = 1;
    private float faith = 1;

    private string helm;
    private string body;
    private string legs;
    private string boots;

    [HideInInspector]
    public float wp_shotgun = 1; public float wp_mgun = 1; public float wp_rocket = 1; public float wp_gLauncher = 1;

}
