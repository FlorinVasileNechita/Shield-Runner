using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour {
	int speed;	
	BoxCollider2D mBoxCollider;
	int attackPerPeriod = 3;
	int attackPattern = 6;
	AttackMethod attackMethod;
	// Use this for initialization
	void Start () {
		attackPattern = Random.Range(1, attackPattern);
		mBoxCollider = GetComponent<BoxCollider2D>();
		attackMethod = gameObject.AddComponent<AttackMethod>();
		InvokeRepeating("openFire", 1, attackPerPeriod);
	}
	
	
	void openFire() {
		attackMethod.shootBullet(new Vector2(transform.position.x, transform.position.y+0.5f), attackPattern);
	}
}
