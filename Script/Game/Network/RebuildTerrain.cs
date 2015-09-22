using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RebuildTerrain : MonoBehaviour {
	string floatListString = "5.25 4.270467 3.437455 3.167483 2.550271 1.905826 0.3581473 0.9905986 0.6209137 0.01826411 -0.6277528";
	// Use this for initialization
	void Start () {
		//Debug.Log(stringToFloatList(floatListString).Count);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<float> stringToFloatList(string s) {
		return s.Split(' ').Select(i => float.Parse(i)).ToList();
	}
}
