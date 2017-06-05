using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

    public GameObject player;
    public Transform spawn;

	void Start () {
        Instantiate(player, spawn.position, Quaternion.identity);	
	}

}
