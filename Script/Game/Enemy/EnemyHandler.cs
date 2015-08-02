using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour {
	int speed;	
	BoxCollider2D mBoxCollider;
	GameObject bulletPrefab;
	int attackPattern = 6;
	float attackPeriod = 0.6f;
	int attackPerPeriod = 3;
	// Use this for initialization
	void Start () {
		attackPattern = Random.Range(1, attackPattern);
		mBoxCollider = GetComponent<BoxCollider2D>();
		bulletPrefab = Resources.Load<GameObject>("Game/Test/Bullet");
		InvokeRepeating("openFire", 1, attackPerPeriod);
	}
	
	void openFire() {
		float acutalAttackPeriod = attackPeriod;
		for (int i = 0; i < attackPattern; i++) {
			Color color = (Random.Range(0,10) > 5) ? Color.green : Color.red;
			GameObject bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y+0.5f ), Quaternion.identity) as GameObject;
			bullet.GetComponent<BulletEffect>().bulletStart(acutalAttackPeriod, color);
			acutalAttackPeriod += attackPeriod;
		}
	}
	
}
