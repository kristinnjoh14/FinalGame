using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiSoundManager : MonoBehaviour {
	public AudioClip slidingSound;
	public AudioClip hardTurn;
	public AudioClip windSound;
	public SkierMovement skier;
	public float windVolume;
	private AudioSource audioSource;
	private AudioSource hardTurnSource;
	private AudioSource windSource;
	private Rigidbody skierrb;
	private bool turningHard = false;
	// Use this for initialization
	void Start () {
		skier = GetComponent<SkierMovement> ();
		skierrb = GetComponent<Rigidbody> ();

		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = slidingSound;
		audioSource.loop = true;
		audioSource.Play ();

		windSource = gameObject.AddComponent<AudioSource> ();
		windSource.clip = windSound;
		windSource.loop = true;
		windSource.volume = windVolume;
		windSource.Play ();

		hardTurnSource = gameObject.AddComponent<AudioSource> ();
		hardTurnSource.clip = hardTurn;
	}

	// Update is called once per frame
	void Update () {
		hardTurnSource.volume -= 0.5f*Time.deltaTime;
		windSource.volume = Camera.main.fieldOfView / 150;
		audioSource.pitch = Time.timeScale;
		hardTurnSource.pitch = Time.timeScale;
        windSource.pitch = Time.timeScale;
		if (!turningHard)  {
			hardTurnSource.volume -= 0.7f*Time.deltaTime;
			if (hardTurnSource.volume < 0.05f) {
				hardTurnSource.Stop ();
			}
		}
	}
		
	public void hardTurnSound (float deltaVel) {
		hardTurnSource.volume = 1-0.7f*deltaVel;
		if (!hardTurnSource.isPlaying && deltaVel <= 0.95f) {
			hardTurnSource.time = 0;//Random.value/2;
			hardTurnSource.Play ();
			turningHard = true;
		} else if (deltaVel > 0.95f) {
			turningHard = false;
		}
	}
}
