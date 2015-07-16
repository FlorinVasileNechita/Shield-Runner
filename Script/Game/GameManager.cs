using UnityEngine;
using System.Collections;

namespace Game {
	public class GameManager : MonoBehaviour {
		GameObject Bottom;
		PlayerManager Player;
		bool gameStart  = false;


		// Use this for initialization
		void Start () {
			Bottom = GameObject.Find("Bottom");
			Player = GameObject.Find("Player").GetComponent<PlayerManager>();
			PlayerReposition();
		}
		
		// Update is called once per frame
		void Update () {

		}



		void PlayerReposition() {

			for (int i = 0; i < 10; i++) {
				Vector2 basicPointA = new Vector2(Player.transform.position.x, Player.transform.position.y + i);

				if (!Physics2D.OverlapCircle(basicPointA, 1, ConstantVariable.platformLayer)) {
					Player.transform.position = basicPointA;
					return;
				}
			}

		}

	}
}