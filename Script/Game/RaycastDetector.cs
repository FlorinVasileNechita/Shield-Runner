using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game {
	public class RaycastDetector : MonoBehaviour {
		private StatusManager self; 
		private Vector2 boxSize;
		private bool testMode = true;
		private float horizontalGap;
		private float verticalGap;
		private int raycastLine = 4;
		private int raycastLayer = ConstantVariable.platformLayer | ConstantVariable.enemyLayer;
		public List<RaycastHit2D> sideHits = new List<RaycastHit2D>();
		public List<RaycastHit2D> topHits = new List<RaycastHit2D>();
		public List<RaycastHit2D> bottomHits = new List<RaycastHit2D>();

		// Use this for initialization
		void Start () {
			self = GetComponent<StatusManager>();
			boxSize = self.playerManager.mBoxCollider.size;
			horizontalGap = boxSize.y / raycastLine;
			verticalGap = boxSize.x / raycastLine;
		}
		
		// Update is called once per frame
		void Update () {
			setSideHits();
			setVerticalHits();
		}

//		void OnDrawGizmos() {
//			setSideHits();
//			setVerticalHits();
//		}

		void setVerticalHits() {
			float baseWidth = self.transform.position.x + (verticalGap*1.5f);
			float y = (boxSize.y/2) + self.transform.position.y;
			topHits.Clear();
			bottomHits.Clear();

			for (int i = 0; i < raycastLine; i++) {
				Vector2 orginPos  = new Vector2(baseWidth, y);
				Vector2 topTargetPos = new Vector2(baseWidth, y + (1.8f*1.15f));
				Vector2 bottomTargetPos = new Vector2(baseWidth, y + (-1.8f*1.15f));

				//Top
				if (Physics2D.Linecast(orginPos, topTargetPos, ConstantVariable.platformLayer))
					topHits.Add(Physics2D.Linecast(orginPos, topTargetPos, ConstantVariable.platformLayer));
				//Gizmos.color = Color.yellow;
				//Gizmos.DrawLine(orginPos, topTargetPos);
				//Bottom
				if (Physics2D.Linecast(orginPos, bottomTargetPos, ConstantVariable.platformLayer)) 
					bottomHits.Add(Physics2D.Linecast(orginPos, bottomTargetPos, ConstantVariable.platformLayer));

				//Gizmos.color = Color.green;
				//Gizmos.DrawLine(orginPos, bottomTargetPos);

				baseWidth -= verticalGap;
			}
		}

		void setSideHits() {
			int faceSide = 1;
			sideHits.Clear();
			//Gizmos.color = Color.blue;
			float y = (boxSize.y/2) + self.transform.position.y;

			float baseHeight = y + (horizontalGap*1.5f);
			for (int i = 0; i < raycastLine; i++) {
				Vector2 orginPos  = new Vector2(self.transform.position.x, baseHeight);
				Vector2 targetPos = new Vector2(self.transform.position.x + (faceSide/1f), baseHeight);
				if (Physics2D.Linecast(orginPos, targetPos, ConstantVariable.platformLayer)) 
					sideHits.Add( Physics2D.Linecast(orginPos, targetPos, ConstantVariable.platformLayer) );
				//Gizmos.DrawLine(orginPos, targetPos);

				baseHeight -= horizontalGap;
			}
		}
	}
}