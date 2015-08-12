using UnityEngine;
using System.Collections;

public class BuzzsawEffect : MonoBehaviour {
	public MusicHandler musicHandler;

	void Start() {
		musicHandler = GameObject.Find("Barriers").GetComponent<MusicHandler>();
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Game.PlayerManager player = other.GetComponent<Game.PlayerManager>();
			player.damage();
			musicHandler.playSound(player.mMusicModel.hitTrap);
			Destroy(gameObject, 0.5f);
		}
	}
}
