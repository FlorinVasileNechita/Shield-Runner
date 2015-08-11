using UnityEngine;
using System.Collections;
using Game;

public class GUI_MainManager : MonoBehaviour {
	public GameObject title;
	public GameObject startButton;
	public GameObject setting;
	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		gameManager = Camera.main.GetComponent<GameManager>();
	}
	
	public void gui_startGame() {
		title.SetActive(false);
		startButton.SetActive(false);
		gameManager.gameStart();
	}
}
