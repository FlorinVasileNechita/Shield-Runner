using UnityEngine;
using System.Collections;

public class AttackMethod : MonoBehaviour {
	float attackPeriod = 0.6f;
	int attackPattern = 6;
	GameObject bulletPrefab;
	
	void Start() {
		attackPattern = Random.Range(1, attackPattern);
		bulletPrefab = Resources.Load<GameObject>("Game/Test/Bullet");
	}

	public void shootBullet(Vector2 spawnPosition, int attackPattern) {
		float acutalAttackPeriod = attackPeriod;
		for (int i = 0; i < attackPattern; i++) {
			Color color = (Random.Range(0,10) > 5) ? Color.green : Color.red;
			GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity) as GameObject;
			bullet.GetComponent<BulletEffect>().bulletStart(acutalAttackPeriod, color);
			acutalAttackPeriod += attackPeriod;
		}
	}
}
