﻿using UnityEngine;
using System.Collections;

public class BulletEffect : MonoBehaviour {
	public string bulletColor;
	int bulletSpeed = 7;
	bool on = false;
	AudioClip catchAudio;
	MusicHandler musicHandler;
	GUI_MainManager guiManager;
	Game.PlayerManager player;

	public void bulletStart(float waitS) {
		Destroy(gameObject, 8);
		catchAudio = Resources.Load<AudioClip>("Music/Game/catch_" + bulletColor);
		musicHandler = GameObject.Find("Barriers").GetComponent<MusicHandler>();
		guiManager = GameObject.Find("Canvas").GetComponent<GUI_MainManager>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Game.PlayerManager>();
		StartCoroutine(bulletIsOn(waitS));
	}
	
	IEnumerator bulletIsOn(float waitS) {
		yield return new WaitForSeconds(waitS);
		on = true;
		if (transform.position == Vector3.zero) {
			transform.position = new Vector2 (player.transform.position.x + 26, player.transform.position.y+1);
			checkCollision();
		}
	}

	private void checkCollision() {
		RaycastHit2D[] hits = Physics2D.LinecastAll (new Vector2(transform.position.x - 2f,transform.position.y),
		                                             new Vector2(transform.position.x + 2f,transform.position.y),
		                                             ConstantVariable.bulletLayer);
		if (hits.Length > 1) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per fram
	void Update () {
		if (on) {
			transform.Translate(-transform.right * bulletSpeed * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (player.currentShieldStatus != bulletColor) {
				player.damage();
				musicHandler.playSound(player.mMusicModel.hitBall);
				guiManager.combo = 0;
			} else {
				musicHandler.playSound(catchAudio);
				guiManager.addScore(1);
				guiManager.addCombo();
			}
			Destroy(gameObject);
		}
	}
	
}
