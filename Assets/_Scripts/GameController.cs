using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class GameController : MonoBehaviour {
    public float score = 0;

    public bool useTiltControls = true;
	private bool useTiltControlsTmp = true;

    public bool LeftButtonPressed;
    public bool RightButtonPressed;

	public bool lost;
	public float speed;
	public float acceleration;

    public GameObject settingsContainer;


	public Slider inputSensitivity;
	Matrix4x4 baseMatrix = Matrix4x4.identity;

	private DataController dc;
	private static GameController instance;
	private float initialFixedUpdate;
	private Toggle useTap;
	/*void Awake() {
		Application.targetFrameRate = 60;
		if (instance == null) {
			instance = this;
		} else {
			DestroyObject (gameObject);
		}
		DontDestroyOnLoad (this);
	}*/

	// Use this for initializatin
	void Start() {
		initialFixedUpdate = Time.fixedDeltaTime;
		Application.targetFrameRate = 60;
		//Load settings
		dc = FindObjectOfType<DataController> ();
		dc.loadSettings ();
		useTiltControls = dc.settings.useTilt;
		inputSensitivity.value = dc.settings.accelerometerSensitivity;
		Calibrate (dc.settings.accelerometerOffsetX, dc.settings.accelerometerOffsetY, dc.settings.accelerometerOffsetZ);

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		score = 0;
		scoreBoard = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Text>();
		cogWheelClicked ();
		settingsContainer = GameObject.FindGameObjectWithTag ("SettingsContainer");
		useTap = GameObject.FindGameObjectWithTag ("UseTiltToggle").GetComponent<Toggle> ();
		useTap.isOn = !useTiltControls;
		outsideOfSettingsMenuClicked ();
	}

	// Update is called once per frame
	void Update () {
		if (!lost) {
			score += Time.deltaTime;
		}
        if (scoreBoard != null)
        {
            scoreBoard.text = score.ToString("F2");
        }
	}

	public void GoToMainMenu(){
		Time.timeScale = 1;
		SceneManager.LoadScene (0);
	}

	public void ResetGame(){
		Time.timeScale = 1;
		//GameObject.FindGameObjectWithTag ("RetryScreen").GetComponent<Text> ().text = "";
		score = 0;
		Time.fixedDeltaTime = initialFixedUpdate;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public GameObject retryPanel, mainMenuPanel;

	public void showResetStuff(){
		retryPanel.SetActive (true);
		mainMenuPanel.SetActive (true);
		FindObjectOfType <ObstacleScript> ().enabled = false;
		if (dc != null)
		{
			if (dc.newScore((int)score, score))
			{
				scoreBoard.text = "New high score!\n" + score.ToString("F2");
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
        useTiltControlsTmp = !value;
    }

    private Text scoreBoard;

	//Credit to user Polymorphik for his reply on thread in following link as accessed @17:00PM 23/03/2018
	//https://forum.unity.com/threads/input-acceleration-calibration.317121/
	//His code with slight adjustments used in both Calibrate and AdjustedAccelerometer
	//Sets value needed for the calibration of AdjustedAccelerometer function
	public void setCalibration () {
		Vector3 tmp = Input.acceleration;
		dc.settings.accelerometerOffsetX = tmp.x;
		dc.settings.accelerometerOffsetY = tmp.y;
		dc.settings.accelerometerOffsetZ = tmp.z;
	}
	public void Calibrate() {
		Calibrate (dc.settings.accelerometerOffsetX, dc.settings.accelerometerOffsetY, dc.settings.accelerometerOffsetZ);
		dc.saveSettings ();
	}
	//Overload to set saved 0-position setting
	public void Calibrate(float x, float y, float z) {
		Quaternion rotate = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), new Vector3 (x,y,z));

		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotate, new Vector3(1.0f, 1.0f, 1.0f));

		this.baseMatrix = matrix.inverse;
	}

	//Get accelerometer value adjusted for initial orientation
	public Vector3 AdjustedAccelerometer {
		get {
			return this.baseMatrix.MultiplyVector(Input.acceleration);
		}
	}

	public void saveSettings () {
		Calibrate ();
		useTiltControls = useTiltControlsTmp;
		dc.settings.accelerometerSensitivity = inputSensitivity.value;
		dc.settings.useTilt = useTiltControls;
		dc.saveSettings ();
	}
}
