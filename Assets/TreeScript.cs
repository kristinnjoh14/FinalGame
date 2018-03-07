using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeScript : MonoBehaviour {
	public float acceleration;
	public float speed;

	private GameController gc;
	private Text scoreBoard;
	// Use this for initialization
	void Start () {
		acceleration = FindObjectOfType<PlaneController> ().acceleration;
		speed = FindObjectOfType<PlaneController> ().scrollSpeed;
		scoreBoard = GameObject.FindGameObjectWithTag ("ScoreCounter").GetComponent<Text> ();
		gc = FindObjectOfType<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x > 10) {
			//gc.score += 1;
			//scoreBoard.text = gc.score.ToString ();
			Destroy (gameObject);
		}
		speed += acceleration;
		GetComponent<Rigidbody> ().velocity = new Vector3 (speed, 0, 0);
	}
}
