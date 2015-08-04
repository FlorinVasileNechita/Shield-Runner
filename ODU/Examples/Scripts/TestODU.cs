using UnityEngine;
using System.Collections;

/// <summary>
/// Class for testing the Debugger.
/// </summary>
public class TestODU : MonoBehaviour {


	// Use this for initialization
	void Start () {
		for (int i = 0; i < 1; i++) {
			Debugger.Log("Fuck it...");
			Debugger.LogWarning("HOLA!");
			Debugger.LogError("I'm at a placed called vertigo");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
