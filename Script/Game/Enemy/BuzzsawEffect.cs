using UnityEngine;
using System.Collections;

public class BuzzsawEffect : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.GetComponent<Game.PlayerManager>().damage();
			Destroy(gameObject, 0.5f);
		}
	}
}
