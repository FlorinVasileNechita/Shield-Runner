using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

namespace Game {
	public class GameManager : MonoBehaviour {
		GameObject Bottom;
		PlayerManager player;
		CameraManager camera;
		GUI_MainManager guiManager;
		
		public bool gameOn  = false;
	
		void Awake() {
			PlayGamesPlatform.Activate();
		}
		
		private void googlePlayLogin () 
		{
			if (PlayerPrefs.GetInt("user_id", 0) == 0) {
				Social.localUser.Authenticate((bool success) => {
					string UserName  = Social.localUser.userName; // UserName
					string UserID      = Social.localUser.id; // UserID
					PlayerPrefs.SetString("name", UserName);
					PlayerPrefs.SetString("google_id", UserID );
				});
			}
		}

		
		// Use this for initialization
		void Start () {
			guiManager = GameObject.Find("Canvas").GetComponent<GUI_MainManager>();

			Bottom = GameObject.Find("Bottom");
			player = GameObject.Find("Player").GetComponent<PlayerManager>();
			camera = gameObject.GetComponent<CameraManager>();
			PlayerReposition();

			googlePlayLogin();
		}

		public void gameOver() {
			int pastScore = PlayerPrefs.GetInt("HighScore", 0);

			if (pastScore < guiManager.score) {
				PlayerPrefs.SetInt("HighScore", guiManager.score);
				new WebManager().saveHighScore();
			}

			Application.LoadLevel(Application.loadedLevel);
		}

		public void gameStart() {
			gameOn = true;
			player.currentStatus = PlayerManager.Status.Run;
			camera.cameraSpeed = 4;
		}
		
		void positioning(GameObject targetObject ) {
			for (float k = -9; k <= 10; k += 0.5f) {
				Vector2 basicPointA = new Vector2(targetObject.transform.position.x, targetObject.transform.position.y  + k);
				
				if (!Physics2D.OverlapCircle(basicPointA, 1f, ConstantVariable.platformLayer) && 
				    Physics2D.Linecast (basicPointA, new Vector2(basicPointA.x, basicPointA.y -1.5f), ConstantVariable.platformLayer)) {
						targetObject.transform.position = basicPointA;
						return;

				}
			}
			Debug.Log("None");

		}


		void PlayerReposition() {
			GameObject tutorialRed = GameObject.Find("tutorial_red");
			GameObject tutorialGreen = GameObject.Find("tutorial_green");
			positioning(tutorialGreen);
			positioning(tutorialRed);

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