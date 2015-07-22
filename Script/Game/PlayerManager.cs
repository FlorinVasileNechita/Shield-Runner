using UnityEngine;
using System.Collections;


namespace Game {
	public class PlayerManager : MonoBehaviour {
		public enum Status {Run, Jump, BeHit};
		public Status currentStatus = Status.Run;
		public string currentShieldStatus = "red";
	
		public bool isLand;
		public int jumpNum = 0;
		public int speed = 5;
		private int maxJumpNum = 2;
		private float stunTime = 1f;
		
		//1 = walk, 2 = jump, 3 = land
		public Animator mAnim;
		public Rigidbody2D mRigidBody;
		public BoxCollider2D mBoxCollider;
		public GameObject landParticle;
		public GameObject jumpParticle;
		// Use this for initialization
		void Start () {
			mRigidBody = GetComponent<Rigidbody2D>();
			mBoxCollider = GetComponent<BoxCollider2D>();
			mAnim = GetComponent<Animator>();
			
		}

		void Update() {
			
			if (Input.GetMouseButtonDown(0)) Jump();
			if (Input.GetKeyDown(KeyCode.Z)) shieldHandler("red");
			if (Input.GetKeyDown(KeyCode.X)) shieldHandler("green");
					
		}

		// Update is called once per frame
		void FixedUpdate () {
			if (currentStatus != Status.BeHit) Move ();
		}

		void Jump() {
			int jumpPower = 10;
			float actualPower = (jumpNum == 0) ? jumpPower : jumpPower * 0.7f;
			if (jumpNum < maxJumpNum) {
				if (jumpNum == 0) {
					mRigidBody.velocity = new Vector2(mRigidBody.velocity.x, actualPower);					
				} else {
					mRigidBody.velocity = new Vector2(mRigidBody.velocity.x, actualPower);
				}
				particleSwitcher(jumpParticle, true);
				jumpNum++;		
			}
		}
		
		void Move() {
			transform.Translate(transform.right * speed *  Time.deltaTime);
		}
		
		
		
		void shieldHandler(string color) {
			
			
		}
		
		public void particleSwitcher(GameObject particleObject, bool play) {
			particleObject.SetActive(play);
		}
	}
}