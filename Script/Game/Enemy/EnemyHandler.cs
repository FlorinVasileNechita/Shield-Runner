using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour {
	int speed;	
	BoxCollider2D mBoxCollider;
	GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		mBoxCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void openFire() {

	}
}
