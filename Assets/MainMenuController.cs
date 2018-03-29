using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class MainMenuController : MonoBehaviour {
	private int highScore;
	// Use this for initialization
	void Start () {
		//loadData ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Loads main scene
	public void startGame () {
		//SceneManager.LoadScene (getMainScene ());
	}

	//Gets main scene TODO:set to correct scene before deployment
	/*public Scene getMainScene () {
		SceneManager.GetSceneByName ("KiddiCpy");
	}

	private void loadData () {
		Stream stream = File.Open("MySavedGame.gamed", FileMode.Open);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new SerializationBinder(); 
		Debug.Log ("Reading Data");
		highScore = (int)bformatter.Deserialize(stream);
		stream.Close();
	}*/
}
