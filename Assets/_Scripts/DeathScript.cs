using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour {
	private GameController gc;
	private Text retryText;
	private Rigidbody board;
	// Use this for initialization
	void Start () {
		retryText = GameObject.FindGameObjectWithTag ("RetryScreen").GetComponent<Text> ();
		gc = FindObjectOfType<GameController> ();
		board = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter (Collision c2) {
		//Debug.Log ("ow");
		//Debug.Log(Vector3.Distance(c2.transform.position, transform.position));
		if (c2.gameObject.CompareTag ("Tree")) {
			Time.timeScale = 0.2f;
			gc.lost = true;
			retryText.text = "Tap to retry";
			board.constraints = RigidbodyConstraints.None;
		}
	}
}
