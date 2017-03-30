﻿using UnityEngine;
using System.Collections;
/*
Author: Connor Wakes	Date: 20th March 2017
Objects holding this script: Enemy_Golem
Function:	Controls the enemy golem's AI as well as animation
*/
public class Enemy_Golem : MonoBehaviour {

    private int damage = 16; //The amount of damage the golem applies to the player
    private bool hit;
    public float attackDistance = 2.5f; //How close the golem needs to be before it can attack
    public float distance; //The distance in between the golem and the player
    private Transform target; //The transform of the player
    private NavMeshAgent navComponent; //The navigation mesh component
    private Animator anim; //The golem's animator controller
    private PlayerController player; //The player controller script

	void Start () {
        navComponent = gameObject.GetComponent<NavMeshAgent>(); //Finds the navigation mesh
        anim = GetComponent<Animator>(); //Finds the animator controller on self
	}
	void Update () {
        FindPlayer();
        distance = Vector3.Distance(target.position, transform.position); //Sets the distance variable
        if (target && distance < 10f) {
            anim.SetBool("PlayerFound", true); //Starts the walking animation
            navComponent.SetDestination(target.position); //Sets destination at the player
            navComponent.Resume();
        }
        else if (target && distance > 10f) {
            anim.SetBool("PlayerFound", false);
            navComponent.Stop();
        }
        if (distance < attackDistance) { //Checks if distance is smaller or equal to attackDistance
            Attack(); //Starts attack function
        }
        else if (distance > attackDistance) {
            anim.SetBool("PlayerAttack", false);
        }
	}
    void FindPlayer () { //Function that finds the player
        if (target) //If player is already found return
            return;
        player = GameObject.FindObjectOfType<PlayerController>(); //Finds player via the PlayerController script
        if (player.hasSpawned == true) { //Checks PlayerController script to see if they have spawned
            target = player.transform; //target is the players transform position
        }
    }
    void Attack () { //Function that controls the golem attack
        navComponent.Stop(); //Stops the movement of the golem whilst attacking
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>(); //Finds the enemy health -- Player health in this case -- Used so that enemies can damage each other
        anim.SetBool("PlayerFound", false); //Stops the walking animation
        anim.SetBool("PlayerAttack", true); //Starts the attacking animation
        if (enemyHealth != null && hit == false) { //if enemy health doesn't equal null, damage the enemy with this scripts damage variable
            enemyHealth.Damage(damage);
            hit = true;
        }
        StartCoroutine(AttackTime()); // Starts AttackTime function
    }
    IEnumerator AttackTime() { //Used to stop movement whilst attacking and then resume once attack is complete
        yield return new WaitForSeconds(2f);
        hit = false;
        navComponent.Resume();
    }
}
