using UnityEngine;
using System.Collections;

namespace Game {
	public class StatusManager : MonoBehaviour {
		public RaycastDetector raycastDetector;
		public PlayerManager playerManager;
		private float maxClimbFallSpeed = -0.1f;

		void Start () {
			raycastDetector = gameObject.AddComponent<RaycastDetector>();
			playerManager = GetComponent<PlayerManager>();

		}

		void checkWallSlide() {
			if (raycastDetector.bottomHits.Count < 3 && raycastDetector.sideHits.Count > 0) {


			} else {

			}
		}

		void checkIsLand() {
			if (raycastDetector.bottomHits.Count > 0) {
				playerManager.jumpNum = 0;
				playerManager.isLand = true;
				if (playerManager.mRigidBody.velocity.y < -10) {
					Debug.Log("Too High");
				}
			} else {
				playerManager.isLand = false;
			}
		}

		void Update() {
			checkWallSlide();
			checkIsLand();
		}

	}
}