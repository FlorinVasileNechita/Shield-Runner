using UnityEngine;
using System.Collections;

public class TumbleweedMotion : MonoBehaviour {
	
	float wind = 7;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = new Vector3(0,Random.value * 2,0);
	}
	
	
	void Update () {
		GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(wind, 2, 0), transform.position);
		if (transform.position.x > 30) Destroy (gameObject);
	}
}
