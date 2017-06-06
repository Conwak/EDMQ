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

        if (waypoints.Length > 2) {
            waypointInd = Random.Range(0, waypoints.Length);
            navComponent.SetDestination(waypoints[waypointInd].transform.position);
        } else if (waypoints.Length > 1) {
            navComponent.SetDestination(waypoints[waypointInd].transform.position);
        } else if (waypoints.Length <= 0) {
            waypointInd = 0;
        }
         
    }
	void Update () {
        if (!target) {
            FindPlayer();
        }

        distance = Vector3.Distance(target.position, transform.position);

        if (distance < 7f && distance > 1.5f && !hit && !playerHit) {
            navComponent.speed = 4.5f;
            navComponent.stoppingDistance = 1.5f;
            anim.SetBool("PlayerFound", true);
            navComponent.SetDestination(target.position);
            navComponent.Resume();
        } else if (distance < 1.5f && !hit) {
            playerHit = true;
            navComponent.Stop();
            anim.SetBool("InRange", true);
        } else if (distance > 7f) {
            Patrol();
        }

        if (hit) {
            anim.SetBool("Hit", true);
            navComponent.Stop();
        }
	}

    void FindPlayer () {
        if (target)
        return;
        player = GameObject.FindObjectOfType<PlayerController>();
        target = player.transform;
    }

    public void Attack () {
        playerHit = true;
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null && distance < 1.5f) {
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

    void Patrol() {
        navComponent.speed = 3f;
        navComponent.stoppingDistance = 0f;

        if (waypoints.Length == 1 || waypoints.Length == 0) {
            navComponent.Stop();
            anim.SetBool("PlayerFound", false);
        }

        if (waypoints.Length >= 2 && !inPos) {
            anim.SetBool("PlayerFound", true);
            navComponent.SetDestination(waypoints[waypointInd].transform.position);
            navComponent.Resume();
        }
        if (waypoints.Length > 1 && Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) < 0.5f) {
            inPos = true;
            StartCoroutine(WaypointIdle());
        }
    }

    IEnumerator WaypointIdle () {
        anim.SetBool("PlayerFound", false);
        navComponent.velocity = Vector3.zero;
        navComponent.Stop();
        if (waypoints.Length <= 2 && waypointInd == 0) {
            waypointInd = 1;
        } else if (waypoints.Length <= 2 && waypointInd == 1) {
            waypointInd = 0;
        }
        if (waypoints.Length > 2) {
            waypointInd = Random.Range(0, waypoints.Length);
        }
        yield return new WaitForSeconds(5f);
        inPos = false;
    }
}
