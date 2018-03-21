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

	private static GameController instance;

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
    }
    /// <summary>
    /// this is an event that happens when the user clicks outside of the settingspanel.
    /// </summary>
    public void outsideOfSettingsMenuClicked()
    {
        settingsContainer.SetActive(false);
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
    // Use this for initializatin
    void Start() {
        score = 0;
        scoreBoard = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Text>();
		cogWheelClicked ();
		settingsContainer = GameObject.FindGameObjectWithTag ("SettingsContainer");
		outsideOfSettingsMenuClicked ();
		if(!Application.isEditor) {
			deviceOrgRot = Quaternion.Euler (Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
		}
    }

    void SettingsButtonClicked()
    {
        
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
}
