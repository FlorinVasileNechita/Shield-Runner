using UnityEngine;
using System.Collections;

public class BulletEffect : MonoBehaviour {
	int bulletSpeed = 7;
	bool on = false;
	AudioClip catchAudio;
	string bulletColor;
	MusicHandler musicHandler;
	GUI_MainManager guiManager;

	public void bulletStart(float waitS, Color color) {
		gameObject.SetActive(true);
		Destroy(gameObject, 8);
		gameObject.GetComponent<MeshRenderer>().material.color = color;
		bulletColor = (color == Color.red) ? "red" : "green";
		catchAudio = Resources.Load<AudioClip>("Music/Game/catch_" + bulletColor);
		musicHandler = GameObject.Find("Barriers").GetComponent<MusicHandler>();
		guiManager = GameObject.Find("Canvas").GetComponent<GUI_MainManager>();
		StartCoroutine(bulletIsOn(waitS));
	}
	
	IEnumerator bulletIsOn(float waitS) {
		yield return new WaitForSeconds(waitS);
		on = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (on) {
			transform.Translate(-transform.right * bulletSpeed * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Game.PlayerManager player = other.GetComponent<Game.PlayerManager>();
			if (player.currentShieldStatus != bulletColor) {
				player.damage();
				musicHandler.playSound(player.mMusicModel.hitBall);
			} else {
				musicHandler.playSound(catchAudio);
				guiManager.addScore(1);
			}
			Destroy(gameObject);
		}
	}
	
}
