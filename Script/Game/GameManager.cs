using UnityEngine;
using System.Collections;

namespace Game {
	public class GameManager : MonoBehaviour {
		GameObject Bottom;
		PlayerManager Player;
		CameraManager Camera;
		public bool gameOn  = false;


		// Use this for initialization
		void Start () {
			Bottom = GameObject.Find("Bottom");
			Player = GameObject.Find("Player").GetComponent<PlayerManager>();
			Camera = gameObject.GetComponent<CameraManager>();
			PlayerReposition();
		}

		public void gameStart() {
			gameOn = true;
			Player.currentStatus = PlayerManager.Status.Run;
			Camera.cameraSpeed = 4;
		}
		
		public void pause() {
			
		}
		
		public void resume() {
			
		}
		void PlayerReposition() {
			for (int i = -5; i < 10; i++) {
				Vector2 basicPointA = new Vector2(Player.transform.position.x, Player.transform.position.y + i);

				if (!Physics2D.OverlapCircle(basicPointA, 1, ConstantVariable.platformLayer)) {
					Player.transform.position = basicPointA;
					return;
				}
			}
		}

	}
}