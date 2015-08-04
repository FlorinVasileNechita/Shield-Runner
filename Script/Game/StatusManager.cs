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


		void checkIsLand() {
			if (raycastDetector.bottomHits.Count > 0) {
				playerManager.currentStatus = (playerManager.currentStatus == PlayerManager.Status.BeHit) ? PlayerManager.Status.BeHit : PlayerManager.Status.Run;
				if (playerManager.mRigidBody.velocity.y <= 0) {
					playerManager.jumpNum = 0;
					playerManager.isLand = true;
					playerManager.mAnim.SetInteger("Status", 1);
				}
				if (playerManager.mRigidBody.velocity.y < -8) {
					//Instantiate(playerManager.landParticle, playerManager.jumpParticle.transform.position, transform.rotation);
					playerManager.particleSwitcher(playerManager.landParticle, true);
					playerManager.landParticle.transform.position = playerManager.jumpParticle.transform.position;
				}
			} else {
				playerManager.isLand = false;
				playerManager.currentStatus = (playerManager.currentStatus == PlayerManager.Status.BeHit) ? PlayerManager.Status.BeHit : PlayerManager.Status.Jump;
				if (playerManager.mRigidBody.velocity.y <= 0) {
					playerManager.mAnim.SetInteger("Status", 3);
				} else {
					playerManager.mAnim.SetInteger("Status", 2);	
				}
			}
		}

		void Update() {
			checkIsLand();
		}

	}
}