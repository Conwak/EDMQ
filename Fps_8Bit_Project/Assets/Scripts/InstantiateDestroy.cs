using UnityEngine;
using System.Collections;

public class InstantiateDestroy : MonoBehaviour {

    public float lifeTime = 5f;

	void Awake () {
        Destroy(this.gameObject, lifeTime);
	}

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Enemy") {
            Destroy(this.gameObject);
        }
    }
}
