using UnityEngine;
using System.Collections;

public class Enemy_Golem : MonoBehaviour {

    private int damage = 16;
    private bool playerHit;
    [HideInInspector]
    public bool hit;
    public static float attackDistance = 1.0f;
    private float distance;
    private Transform target;
    private NavMeshAgent navComponent;
    private Animator anim;
    private PlayerController player;

    [Header("IdleWaypoints")]
    public GameObject[] waypoints;
    public int waypointInd = 0;
    private bool inPos = false;
    
    void Start () {
        navComponent = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (gameObject.name == "Grunt (1)") {
            waypoints = GameObject.FindGameObjectsWithTag("G1_T");
        } else if (gameObject.name == "Grunt (2)") {
            waypoints = GameObject.FindGameObjectsWithTag("G2_T");
        } else if (gameObject.name == "Grunt (3)") {
            waypoints = GameObject.FindGameObjectsWithTag("G3_T");
        } else if (gameObject.name == "Grunt") {
            waypoints = GameObject.FindGameObjectsWithTag("G_T");
        }

        waypointInd = Random.Range(0, waypoints.Length);
        navComponent.SetDestination(waypoints[waypointInd].transform.position);
    }
	void Update () {
        if (!target) {
            FindPlayer();
        }

        distance = Vector3.Distance(target.position, transform.position);

        if (distance > 5f) {
            Patrol();
        } else {
            navComponent.speed = 6f;
            navComponent.stoppingDistance = 1.5f;
        }

        if (distance < 5f && distance > 1.5f && !hit && !playerHit) {
            anim.SetBool("PlayerFound", true);
            navComponent.SetDestination(target.position);
            navComponent.Resume();
        } else if (distance < 1.5f && !playerHit && !hit) {
            anim.SetBool("InRange", true);
            playerHit = true;
            navComponent.Stop();
        } else {
            anim.SetBool("PlayerFound", true);
            Patrol();
        }

        if (hit) {
            anim.SetBool("Hit", true);
            navComponent.Stop();
        }
	}

    void Patrol () {
        navComponent.speed = 3f;
        navComponent.stoppingDistance = 0f;

        if (waypoints.Length < 2) {
            navComponent.Stop();
            anim.SetBool("PlayerFound", false);
        }

        if (waypoints.Length > 2 && !inPos) {
            Debug.Log("Running");
            navComponent.Resume();
        } else {
            navComponent.velocity = Vector3.zero;
            navComponent.Stop();
            anim.SetBool("PlayerFound", false);
        }
        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) < 0.1f) {
            inPos = true;
            StartCoroutine(WaypointIdle());
        }
    }

    void FindPlayer () {
        if (target)
        return;
        player = GameObject.FindObjectOfType<PlayerController>();
        target = player.transform;
    }

    public void Attack () {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null && distance < attackDistance) {
            enemyHealth.Damage(damage);
        }
    }

    void FoundResume () {
        playerHit = false;
        anim.SetBool("InRange", false);
        navComponent.Resume();
    }

    void HitResume () {
        hit = false;
        anim.SetBool("Hit", false);
        navComponent.Resume();
    }

    IEnumerator WaypointIdle () {
        anim.SetBool("PlayerFound", false);
        yield return new WaitForSeconds(5f);
        waypointInd = Random.Range(0, waypoints.Length);
        navComponent.SetDestination(waypoints[waypointInd].transform.position);
        inPos = false;
    }
}
