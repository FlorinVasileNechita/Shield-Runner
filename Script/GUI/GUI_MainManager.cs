using UnityEngine;
using System.Collections;
using Game;
using UnityEngine.UI;

public class GUI_MainManager : MonoBehaviour {
	public GameObject title;
	public GameObject startButton;
	public GameObject setting;
	
	private GameManager gameManager;
	public int score = 0;
	private int highScore;
	private float shieldChangePoint;
	private PlayerManager player;
	public GameObject scoreText;
	public GameObject highScoreGameobject;
	public GameObject highScoreText;
	public GameObject accelerometerGameObject;
	public GameObject accelerometerImage;
	public GameObject settingPanel;
	// Use this for initialization
	void Start () {
		gameManager = Camera.main.GetComponent<GameManager>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
		showHighScore();
		showAccelerometer();
	}

	void Update() {

	}
	
	public void pause() {
		Time.timeScale = 0;
		settingPanel.SetActive(true);
		hideMainGUIPanel(false);	
	}
	
	public void resume() {
		Time.timeScale = 1;
		settingPanel.SetActive(false);
		if (!gameManager.gameOn) {
			hideMainGUIPanel(true);
		}
	}
	

	private void showHighScore() {
		highScoreText.GetComponent<Text>().text = PlayerPrefs.GetInt("HighScore", 0).ToString();
	}

	private void showAccelerometer() {
		accelerometerGameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("ChangeShieldPoint", -0.5f) * -10 ;
		player.shieldChangePoint = PlayerPrefs.GetFloat("ChangeShieldPoint", -0.5f);
	}

	public void changeAccelerometer() {
		shieldChangePoint = (accelerometerGameObject.GetComponent<Slider>().value / 10) * -1;
		PlayerPrefs.SetFloat("ChangeShieldPoint", shieldChangePoint);
		player.shieldChangePoint = shieldChangePoint;
	}

	public void changeShield(Color color) {
		accelerometerImage.GetComponent<Text>().color = color;
	}

	public void addScore(int i) {
		score += i;
		scoreText.GetComponent<Text>().text = score.ToString();
	}

	private void hideMainGUIPanel(bool hide) {
		title.SetActive(hide);
		startButton.SetActive(hide);
		highScoreGameobject.SetActive(hide);
		highScoreText.SetActive(hide);
		
	}

	public void gui_startGame() {
		hideMainGUIPanel(false);
		gameManager.gameStart();
	}
}
