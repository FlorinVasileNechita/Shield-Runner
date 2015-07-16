using UnityEngine;
using System.Collections;


namespace Game {
	public class PlayerManager : MonoBehaviour {
		public bool isLand;
		public int jumpNum;
		public int maxJump = 1;
		public int speed = 5;
		private int maxJumpNum = 1;

		public Rigidbody2D mRigidBody;
		public BoxCollider2D mBoxCollider;

		// Use this for initialization
		void Start () {
			mRigidBody = GetComponent<Rigidbody2D>();
			mBoxCollider = GetComponent<BoxCollider2D>();
		}

		void Update() {
			if (Input.GetMouseButtonDown(0)) {
				Jump();
			}

		}

		// Update is called once per frame
		void FixedUpdate () {
			Move ();
		}

		void Jump() {
			if (jumpNum < maxJumpNum) {
				mRigidBody.velocity = new Vector2(mRigidBody.velocity.x, 10);
				jumpNum++;
			}
		}

		void Move() {
			transform.Translate(transform.right * speed *  Time.deltaTime);
			//mRigidBody.AddForce(transform.right * speed * 5);
		}


	}
}