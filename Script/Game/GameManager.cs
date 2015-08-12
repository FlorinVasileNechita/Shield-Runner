using UnityEngine;
using System.Collections;

namespace Game {
	public class GameManager : MonoBehaviour {
		GameObject Bottom;
		PlayerManager player;
		CameraManager camera;
		public bool gameOn  = false;



		// Use this for initialization
		void Start () {
			Bottom = GameObject.Find("Bottom");
			player = GameObject.Find("Player").GetComponent<PlayerManager>();
			camera = gameObject.GetComponent<CameraManager>();
			PlayerReposition();
		}

		public void gameStart() {
			gameOn = true;
			player.currentStatus = PlayerManager.Status.Run;
			camera.cameraSpeed = 4;
		}
		
		public void pause() {
			
		}
		
		public void resume() {
			
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