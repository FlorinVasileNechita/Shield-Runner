using UnityEngine;
using System.Collections;


namespace Game {
	public class CameraManager : MonoBehaviour {
		public GameObject followObject;
		public float maxXOffset = -5;

		float screenWidth;
		float camX;
		
		void Start      () {
			screenWidth = GetViewSizeAtDistance(Mathf.Abs(followObject.transform.position.z - transform.position.z)).x;
		}
		void FixedUpdate () {
			if (followObject.transform.position.x < camX - screenWidth / 2) {
				//Restart
//				followObject.transform.position = new Vector3(0, 9, 0);
//				transform.position = new Vector3(0, 9, transform.position.z);
//				camX = 0;
				Application.LoadLevel(Application.loadedLevel);
			}
			
			camX += GetSpeed() * Time.deltaTime;
			if (followObject.transform.position.x > camX + maxXOffset) {
				camX = followObject.transform.position.x - maxXOffset;
			}
			transform.position = new Vector3(camX, followObject.transform.position.y + 1, transform.position.z);
		}
		
		float GetSpeed() {
			return 4;
		}
		
		public static Vector2 GetViewSizeAtDistance(float aDist) {
			float frustumHeight = 2f * aDist * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
			return new Vector2(frustumHeight * Camera.main.aspect, frustumHeight);
		}
	}
}