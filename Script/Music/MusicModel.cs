using UnityEngine;
using System.Collections;

public class MusicModel : MonoBehaviour {

	public AudioClip jump;
	public AudioClip greenHit;
	public AudioClip redHit;
	public AudioClip changeShield;
	public AudioClip hitTrap;
	public AudioClip hitBall;
	public AudioClip powerUp;

	// Use this for initialization
	void Start () {
		jump = Resources.Load<AudioClip>("Music/Game/jump");
		greenHit = Resources.Load<AudioClip>("Music/Game/greenHit");
		redHit = Resources.Load<AudioClip>("Music/Game/redHit");
		changeShield = Resources.Load<AudioClip>("Music/Game/changeShield");
		hitTrap = Resources.Load<AudioClip>("Music/Game/hitTrap");
		hitBall = Resources.Load<AudioClip>("Music/Game/hitBall");
		powerUp = Resources.Load<AudioClip>("Music/Game/powerUp");
	}
}
