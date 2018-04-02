using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TMPro;

public class MainMenuController : MonoBehaviour {
	private DataController dc;
	private TextMeshProUGUI scoreSheet;
	private Slider sensitivitySlider;
	private Toggle useTiltToggle;
	private Vector3 orientation = Vector3.zero;
	// Use this for initialization
	void Start () {
		dc = FindObjectOfType<DataController> ();
		dc.loadSave ();
		dc.loadSettings ();
		initScores ();
		initSettings ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void initScores () {
		scoreSheet = GameObject.FindGameObjectWithTag ("ScoreSheet").GetComponent<TextMeshProUGUI> ();
		scoreSheet.text += dc.save.highScore.ToString () + "\nLongest run: ";
		scoreSheet.text += dc.save.longestRun.ToString () + "\n\nTotal number of runs: ";
		scoreSheet.text += dc.save.numberOfRuns;
		GameObject.FindGameObjectWithTag ("StatsContainer").SetActive (false);
	}

	public void initSettings () { 
		sensitivitySlider = GameObject.FindGameObjectWithTag ("SensitivitySlider").GetComponent<Slider> ();
		useTiltToggle = GameObject.FindGameObjectWithTag ("UseTiltToggle").GetComponent<Toggle> ();
		if (dc.settings.accelerometerSensitivity != 0) {
			sensitivitySlider.value = dc.settings.accelerometerSensitivity;
		}
		useTiltToggle.isOn = dc.settings.useTilt;
		GameObject.FindGameObjectWithTag ("SettingsContainer").SetActive (false);
	}

	//Loads main scene
	public void startGame () {
		SceneManager.LoadScene ("KiddiModel");
	}

	public void calibrate () {
		orientation = Input.acceleration;
	}

	public void saveSettings () {
		dc.settings.useTilt = !useTiltToggle.isOn;
		dc.settings.accelerometerSensitivity = sensitivitySlider.value;
		dc.settings.accelerometerOffsetX = orientation.x;
		dc.settings.accelerometerOffsetY = orientation.y;
		dc.settings.accelerometerOffsetZ = orientation.z;
		dc.saveSettings ();
	}
}
