using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public float score;

    public bool useTiltControls = true;

    public bool LeftButtonPressed;
    public bool RightButtonPressed;

    public GameObject settingsContainer; 

	public Quaternion deviceOrgRot;
	Matrix4x4 baseMatrix = Matrix4x4.identity;

	private static GameController instance;

	// Use this for initializatin
	void Start() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		score = 0;
		scoreBoard = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Text>();
		cogWheelClicked ();
		settingsContainer = GameObject.FindGameObjectWithTag ("SettingsContainer");
		outsideOfSettingsMenuClicked ();
		Calibrate ();
	}

	// Update is called once per frame
	void Update () {
		score += Time.deltaTime;
		scoreBoard.text = score.ToString ("F2");
		if (Time.timeScale == 0) {
			FindObjectOfType <ObstacleScript> ().enabled = false;
			if (Input.GetKeyDown (KeyCode.Space) || Input.touchCount != 0) {
				Time.timeScale = 1;
				GameObject.FindGameObjectWithTag ("RetryScreen").GetComponent<Text> ().text = "";
				score = 0;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

    /// <summary>
    /// events for the button presses.???
    /// </summary>
    public void leftButtonPointerDown()
    {
        this.LeftButtonPressed = true;
    }
    public void leftButtonPointerUp()
    {
        this.LeftButtonPressed = false;
    }
    public void rightButtonPointerDown()
    {
        this.RightButtonPressed = true;
    }
    public void rightButtonPointerUp()
    {
        this.RightButtonPressed = false;
    }

    /// <summary>
    /// event for when the cog-wheel is clicked. 
    /// </summary>
    public void cogWheelClicked()
    {
        //show the settings panel
        settingsContainer.SetActive(true);
		Time.timeScale = 0;
    }
    /// <summary>
    /// this is an event that happens when the user clicks outside of the settingspanel.
    /// </summary>
    public void outsideOfSettingsMenuClicked()
    {
        settingsContainer.SetActive(false);
		Time.timeScale = 1;
        //hide the settings panel
    }
    /// <summary>
    /// set the control scheme.
    /// </summary>
    /// <param name="value"></param>
    public void setUseButtonControls(bool value)
    {
        useTiltControls = !value;
    }

    private Text scoreBoard;
    void Awake() {
        Application.targetFrameRate = 60;
		if (instance == null) {
			instance = this;
		} else {
			DestroyObject (gameObject);
		}
		DontDestroyOnLoad (this);
    }

	//Credit to user Polymorphik for his reply on thread in following link as accessed @17:00PM 23/03/2018
	//https://forum.unity.com/threads/input-acceleration-calibration.317121/
	//His code with slight adjustments used in both Calibrate and AdjustedAccelerometer
	//Sets value needed for the calibration of AdjustedAccelerometer function
	public void Calibrate() {
		Quaternion rotate = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), Input.acceleration);

		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotate, new Vector3(1.0f, 1.0f, 1.0f));

		this.baseMatrix = matrix.inverse;
	}

	//Get accelerometer value adjusted for initial orientation
	public Vector3 AdjustedAccelerometer {
		get {
			return this.baseMatrix.MultiplyVector(Input.acceleration);
		}
	}

    void SettingsButtonClicked()
    {
        
    }
}
