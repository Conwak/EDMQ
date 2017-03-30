using UnityEngine;
using System.Collections;

public class InstantiateDestroy : MonoBehaviour {

    float lifeTime = 1f;

	void Awake () {
        Destroy(this.gameObject, lifeTime);
	}
}
