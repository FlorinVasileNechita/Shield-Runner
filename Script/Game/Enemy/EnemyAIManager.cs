using UnityEngine;
using System.Collections;

public class EnemyAIManager : MonoBehaviour {
	private int spawnRatio = 6;
	private GameObject buzzsawPrefab;
	private GameObject monsterPrefab;
	private GameObject player;
	private GameObject barrierParent;
	public AttackMethod attackMethod;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		buzzsawPrefab = Resources.Load<GameObject>("Game/buzzsaw");
		monsterPrefab = Resources.Load<GameObject>("Game/Enemy");
		barrierParent = GameObject.Find("Barriers");
		attackMethod = gameObject.AddComponent<AttackMethod>();
	}

	private void spawn(Vector2 centerPoint, bool isBuzzsaw, GameObject prefab, float radio) {
		int spawnDistance = 22;
		int additionDistance = Random.Range(2, 7);
		if (!isBuzzsaw) centerPoint.x  += additionDistance;
		for (int k = -9; k <= 10; k ++) {
			Vector2 basicPointA = new Vector2(centerPoint.x + spawnDistance, centerPoint.y + k);
			
			if (!Physics2D.OverlapCircle(basicPointA, radio, ConstantVariable.platformLayer) &&
			    Physics2D.Linecast (basicPointA, new Vector2(basicPointA.x, basicPointA.y -1.8f), ConstantVariable.platformLayer)) {
				GameObject gameObject = Instantiate(prefab, basicPointA, Quaternion.identity) as GameObject;
				gameObject.transform.parent = barrierParent.transform;
				return;
			}
		}
	}

	private void remoteAttacker() {
		EnemyHandler[] enemys = barrierParent.GetComponentsInChildren<EnemyHandler>();	
		foreach (EnemyHandler enemy in enemys) {
			if (enemy.gameObject.transform.position.x > player.transform.position.x ) return;			
		}
		int attackPattern = Random.Range(2, 5);
		for (int i =0; i < attackPattern; i++) {
			attackMethod.shootBullet(Vector3.zero, i * 0.5f, attackMethod.getRandomBullet());
		}
	}

	private void generate(Vector2 centerPoint) {
		float spawnSawRatio = Random.Range(0 , 15);
		float spawnEnemyRatio = Random.Range(0 , 12);

		//SpawnSaw
		if (spawnSawRatio < 6) {
			spawn(centerPoint, true, buzzsawPrefab, 1);
		}

		//SpawnEnemy
		if (spawnEnemyRatio < 6) {
			spawn(centerPoint, false, monsterPrefab, 1f);
		}
	}

	private void delete(Vector2 centerPoint) {
		foreach (Transform child in barrierParent.transform) {
			int deleteDistance = 20;
			float distance = centerPoint.x - child.position.x;
			if (distance > deleteDistance ) {
				Destroy(child.gameObject);
			}
		}
	}

	public void refresh(Vector2 centerPoint) {
		generate(centerPoint);
		delete(centerPoint);
		remoteAttacker();
	}
	
	
}
