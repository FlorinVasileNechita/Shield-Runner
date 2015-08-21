using UnityEngine;
using System.Collections;

public class EnemyHandler : MonoBehaviour {
	int speed;	
	BoxCollider2D mBoxCollider;
	int attackPerPeriod = 3;
	int attackPattern = 8;
	int numAttack;
	EnemyAIManager aiManager;
	SkeletonAnimation skeletonAnimation;
	Animator anim;

	// Use this for initialization
	void Start () {
		aiManager = Camera.main.GetComponent<EnemyAIManager>();
		attackPattern = Random.Range(1, attackPattern);
		anim = GetComponent<Animator>();
		mBoxCollider = GetComponent<BoxCollider2D>();
		skeletonAnimation = GetComponent<SkeletonAnimation>();
	}


	void goIdle() {
		anim.SetBool("isIdle", false);
		numAttack = 0;
		StartCoroutine(resumeAttack());
	}

	IEnumerator resumeAttack() {
		yield return new WaitForSeconds(attackPerPeriod);
		anim.SetBool("isIdle", true);
	}
	
	void Fire() {
		if ( numAttack < attackPattern) {
			Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y+0.5f);
			GameObject ballObject;
			string color;

			if (Random.Range(0,10) >= 5) {
				ballObject = aiManager.greenBullet;
				color = "green";
			} else {
				ballObject = aiManager.redBullet;
				color = "red";
			}
			GameObject bullet = Instantiate(ballObject, spawnPosition, Quaternion.identity) as GameObject;
			bullet.GetComponent<BulletEffect>().bulletStart(0, color);
		} else {
			goIdle();
		}
	}
}
