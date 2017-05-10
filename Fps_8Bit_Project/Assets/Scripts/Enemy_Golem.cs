using UnityEngine;
using System.Collections;

public class Enemy_Golem : MonoBehaviour {

    private int damage = 16;
    private bool hit;
    private static float attackDistance = 2.5f;
    private float distance;
    private Transform target;
    private NavMeshAgent navComponent;
    private Animator anim;
    private PlayerController player;

	void Start () {
        navComponent = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
	}
	void Update () {
        if (!target && MainMenu.isPlaying) {
            FindPlayer();
        }
        if (target) {
            distance = Vector3.Distance(target.position, transform.position);
        }
        if (target && distance < 10f) {
            anim.SetBool("PlayerFound", true);
            navComponent.SetDestination(target.position);
            navComponent.Resume();
        } else {
            anim.SetBool("PlayerFound", false);
            navComponent.Stop();
        }
        if (target && distance < attackDistance) {
            Attack();
        } else {
            anim.SetBool("PlayerAttack", false);
        }
	}
    void FindPlayer () {
        if (target)
        return;
        player = GameObject.FindObjectOfType<PlayerController>();
        target = player.transform;
    }
    void Attack () {
        navComponent.Stop();
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        anim.SetBool("PlayerFound", false);
        anim.SetBool("PlayerAttack", true);
        if (enemyHealth != null && hit == false) {
            //enemyHealth.Damage(damage);
            hit = true;
        }
        StartCoroutine(AttackTime());
    }
    IEnumerator AttackTime() {
        yield return new WaitForSeconds(5f);
        hit = false;
        navComponent.Resume();
    }
}
