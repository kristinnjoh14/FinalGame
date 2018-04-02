using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {
	public Save save;
	public Settings settings;
	private static DataController instance;
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			DestroyObject (gameObject);
		}
		DontDestroyOnLoad (this);
	}

	public bool newScore (int score, float runLength) {
		bool ret = false;
		if (runLength > save.longestRun) {
			ret = true;
			save.longestRun = runLength;
		}
		if (score > save.highScore) {
			save.highScore = score;
			ret = true;
		}
		save.numberOfRuns++;
		saveScores ();
		return ret;
	}
		
	public void loadSave () {
		save = new Save ();
		if (PlayerPrefs.HasKey ("HighScore")) {
			save.highScore = PlayerPrefs.GetInt ("HighScore");
		}
		if (PlayerPrefs.HasKey ("RunCount")) {
			save.numberOfRuns = PlayerPrefs.GetInt ("RunCount");
		}
		if (PlayerPrefs.HasKey ("LongestRun")) {
			save.longestRun = PlayerPrefs.GetInt ("LongestRun");
		}
	}

	public void loadSettings () {
		settings = new Settings ();
		if (PlayerPrefs.HasKey ("UseTilt")) {	//Int to bool
			settings.useTilt = PlayerPrefs.GetInt ("UseTilt") == 1;
		}
		if (PlayerPrefs.HasKey ("AccelerometerSensitivity")) {
			settings.accelerometerSensitivity = PlayerPrefs.GetFloat ("AccelerometerSensitivity");
		}
		if (PlayerPrefs.HasKey ("AccelerometerOffsetX")) {
			settings.accelerometerOffsetX = PlayerPrefs.GetFloat ("AccelerometerOffsetX");
		}
		if (PlayerPrefs.HasKey ("AccelerometerOffsetY")) {
			settings.accelerometerOffsetY = PlayerPrefs.GetFloat ("AccelerometerOffsetY");
		}
		if (PlayerPrefs.HasKey ("AccelerometerOffsetZ")) {
			settings.accelerometerOffsetZ = PlayerPrefs.GetFloat ("AccelerometerOffsetZ");
		}
	}

	public void saveSettings () {
		PlayerPrefs.SetFloat ("AccelerometerSensitivity", settings.accelerometerSensitivity);
		if (settings.useTilt) {		//There is no bool functionality, so this is how we do
			PlayerPrefs.SetInt ("UseTilt", 1);
		} else {
			PlayerPrefs.SetInt ("UseTilt", 0);
		}
		PlayerPrefs.SetFloat ("AccelerometerOffsetX", settings.accelerometerOffsetX);
		PlayerPrefs.SetFloat ("AccelerometerOffsetY", settings.accelerometerOffsetY);
		PlayerPrefs.SetFloat ("AccelerometerOffsetZ", settings.accelerometerOffsetZ);
	}

	public void saveScores () {
		PlayerPrefs.SetInt ("HighScore", save.highScore);
		PlayerPrefs.SetInt ("RunCount", save.numberOfRuns);
		PlayerPrefs.SetFloat ("LongestRun", save.longestRun);
	}
}
