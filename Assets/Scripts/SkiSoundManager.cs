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
		audioSource.pitch = Time.timeScale;
		hardTurnSource.pitch = Time.timeScale;
		if (skier.onGround) {
			//audioSource.volume = skierrb.velocity.magnitude / 10;
			//audioSource.pitch = Mathf.Abs (skierrb.velocity.magnitude) / 50;
		} else {
			audioSource.volume -= 7*Time.deltaTime;
		}
	}

	public void hardTurnSound (float deltaVel) {
		hardTurnSource.volume = 1-0.7f*deltaVel;
		if (!hardTurnSource.isPlaying) {
			hardTurnSource.time = 0;
			hardTurnSource.Play ();
		}
	}
}
