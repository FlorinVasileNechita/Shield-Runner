using UnityEngine;
using System.Collections;

public class EnemyAIManager : MonoBehaviour {
	private int spawnRatio = 6;
	private GameObject buzzsawPrefab;

	private GameObject barrierParent;

	// Use this for initialization
	void Start () {
		buzzsawPrefab = Resources.Load<GameObject>("Game/buzzsaw");
		barrierParent = GameObject.Find("Barriers");
	}


	private void spawn(Vector2 centerPoint, bool isBuzzsaw, GameObject prefab, float radio) {
		int spawnDistance = 16;
		int additionDistance = 3;
		if (isBuzzsaw) centerPoint.x  += additionDistance;
		for (int k = -6; k <= 10; k++) {
			Vector2 basicPointA = new Vector2(centerPoint.x + spawnDistance, centerPoint.y + k);
			
			if (!Physics2D.OverlapCircle(basicPointA, radio, ConstantVariable.platformLayer)) {
				GameObject gameObject = Instantiate(prefab, basicPointA, Quaternion.identity) as GameObject;
				gameObject.transform.parent = barrierParent.transform;
				return;
			}
		}
	}

	private void generate(Vector2 centerPoint) {
		float spawnSawRatio = Random.Range(0 , 20);
		float spawnEnemyRatio = Random.Range(0 , 10);

		//SpawnSaw
		if (spawnSawRatio < 6) {
			spawn(centerPoint, true, buzzsawPrefab, 1);
		}

		//SpawnEnemy
		if (spawnEnemyRatio < 6) {
			//spawn(centerPoint, false, buzzsaw);
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
