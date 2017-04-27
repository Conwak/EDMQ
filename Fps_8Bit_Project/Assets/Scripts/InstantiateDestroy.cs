using UnityEngine;
using System.Collections;

public class InstantiateDestroy : MonoBehaviour {

    float lifeTime = 5f;

	void Awake () {
        Destroy(this.gameObject, lifeTime);
	}
}
