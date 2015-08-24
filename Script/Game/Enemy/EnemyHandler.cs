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
	GameObject nextBullet;
	Animator anim;
	public GameObject redSign;
	public GameObject greenSign;



	// Use this for initialization
	void Start () {
		aiManager = Camera.main.GetComponent<EnemyAIManager>();
		attackPattern = Random.Range(1, attackPattern);
		anim = GetComponent<Animator>();
		mBoxCollider = GetComponent<BoxCollider2D>();
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		nextBullet = aiManager.attackMethod.getRandomBullet();
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

	void getNextBullet() {
		nextBullet = aiManager.attackMethod.getRandomBullet();
		string color = nextBullet.GetComponent<BulletEffect>().bulletColor;

		if (color == "green") {
			greenSign.GetComponent<SpriteRenderer>().enabled = true;
			redSign.GetComponent<SpriteRenderer>().enabled = false;
		} else {
			greenSign.GetComponent<SpriteRenderer>().enabled = false;
			redSign.GetComponent<SpriteRenderer>().enabled = true;
		}
	}
	
	void Fire() {
		if ( numAttack < attackPattern) {
			Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y+1);
			aiManager.attackMethod.shootBullet(spawnPosition, 0, nextBullet);
			numAttack++;
			getNextBullet();
		} else {
			goIdle();
		}
	}
}
