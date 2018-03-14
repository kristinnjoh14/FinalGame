using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiSoundManager : MonoBehaviour {
	public AudioClip slidingSound;
	public SkierMovement skier;
	private AudioSource audioSource;
	private Rigidbody skierrb;
	// Use this for initialization
	void Start () {
		skier = GetComponent<SkierMovement> ();
		skierrb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = slidingSound;
		audioSource.time = 10f;
		audioSource.loop = true;
		audioSource.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (audioSource.time > 11f) {
			audioSource.time = 10.2f;
		}
		if (skier.onGround) {
			audioSource.volume = skierrb.velocity.magnitude / 10;
			//audioSource.pitch = Mathf.Abs (skierrb.velocity.magnitude) / 50;
		} else {
			audioSource.volume -= 7*Time.deltaTime;
		}
	}
}
