using UnityEngine;
using System.Collections;

public class EnemyAIManager : MonoBehaviour {
	private int spawnRatio = 6;
	private GameObject buzzsawPrefab;
	private GameObject monsterPrefab;
	
	private GameObject barrierParent;

	// Use this for initialization
	void Start () {
		buzzsawPrefab = Resources.Load<GameObject>("Game/buzzsaw");
		monsterPrefab = Resources.Load<GameObject>("Game/Test/Enemy");
		
		barrierParent = GameObject.Find("Barriers");
	}

	private void spawn(Vector2 centerPoint, bool isBuzzsaw, GameObject prefab, float radio) {
		int spawnDistance = 23;
		int additionDistance = 3;
		if (isBuzzsaw) centerPoint.x  += additionDistance;
		for (int k = -9; k <= 10; k++) {
			Vector2 basicPointA = new Vector2(centerPoint.x + spawnDistance, centerPoint.y + k);
			
			if (!Physics2D.OverlapCircle(basicPointA, radio, ConstantVariable.platformLayer) &&
			    Physics2D.Linecast (basicPointA, new Vector2(basicPointA.x, basicPointA.y -1.8f), ConstantVariable.platformLayer)) {
				GameObject gameObject = Instantiate(prefab, basicPointA, Quaternion.identity) as GameObject;
				gameObject.transform.parent = barrierParent.transform;
				return;
			}
		}
	}

	private void generate(Vector2 centerPoint) {
		float spawnSawRatio = Random.Range(0 , 20);
		float spawnEnemyRatio = Random.Range(0 , 15);

		//SpawnSaw
		if (spawnSawRatio < 6) {
			spawn(centerPoint, true, buzzsawPrefab, 1);
		}

		//SpawnEnemy
		if (spawnEnemyRatio < 6) {
			spawn(centerPoint, false, monsterPrefab, 1.5f);
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
	}
}
