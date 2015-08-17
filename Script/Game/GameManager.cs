using UnityEngine;
using System.Collections;

namespace Game {
	public class GameManager : MonoBehaviour {
		GameObject Bottom;
		PlayerManager player;
		CameraManager camera;
		GUI_MainManager guiManager;
		public bool gameOn  = false;

		// Use this for initialization
		void Start () {
			guiManager = GameObject.Find("Canvas").GetComponent<GUI_MainManager>();

			Bottom = GameObject.Find("Bottom");
			player = GameObject.Find("Player").GetComponent<PlayerManager>();
			camera = gameObject.GetComponent<CameraManager>();
			PlayerReposition();
		}

		public void gameOver() {
			int pastScore = PlayerPrefs.GetInt("HighScore", 0);

			if (pastScore < guiManager.score) {
				PlayerPrefs.SetInt("HighScore", guiManager.score);
			}

			Application.LoadLevel(Application.loadedLevel);
		}

		public void gameStart() {
			gameOn = true;
			player.currentStatus = PlayerManager.Status.Run;
			camera.cameraSpeed = 4;
		}
		
		
		void PlayerReposition() {
			for (int i = -5; i < 10; i++) {
				Vector2 basicPointA = new Vector2(player.transform.position.x, player.transform.position.y + i);

				if (!Physics2D.OverlapCircle(basicPointA, 1, ConstantVariable.platformLayer)) {
					player.transform.position = basicPointA;
					return;
				}
			}
		}

	}
}