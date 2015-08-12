using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour {
	AudioSource mAudioSource;

	void Start() {
		mAudioSource = GetComponent<AudioSource>();
	}

	public void playSound(AudioClip clip) {
		mAudioSource.clip = clip;
		mAudioSource.time = 0;
		mAudioSource.Play();
	}
}
