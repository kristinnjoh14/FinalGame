using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public float score;

	private Text scoreBoard;
	void Awake () {
		Application.targetFrameRate = 60;
	}
	// Use this for initializatin
	void Start () {
		score = 0;
		scoreBoard = GameObject.FindGameObjectWithTag ("ScoreCounter").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		score += Time.deltaTime;
		scoreBoard.text = score.ToString ("F2");
		if (Time.timeScale == 0) {
			FindObjectOfType <ObstacleScript> ().enabled = false;
			if (Input.GetKeyDown (KeyCode.Space) || Input.touchCount > 0) {
				Time.timeScale = 1;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}
}
