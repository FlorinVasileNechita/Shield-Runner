using UnityEngine;
using System.Collections;

public class AttackMethod : MonoBehaviour {
	public GameObject redBullet;
	public GameObject greenBullet;

	void Start() {
		redBullet = Resources.Load<GameObject>("Game/red_bullet");
		greenBullet = Resources.Load<GameObject>("Game/green_bullet");
	}

	public void shootBullet(Vector2 spawnPosition,float activateTime, GameObject ballObject) {
		GameObject bullet = Instantiate(ballObject, spawnPosition, Quaternion.identity) as GameObject;
		bullet.GetComponent<BulletEffect>().bulletStart(activateTime);		
	}

	public GameObject getRandomBullet() {
		if (Random.Range(0,10) >= 5) {
			return greenBullet;
		} else {
			return redBullet;
		}
	}
}
