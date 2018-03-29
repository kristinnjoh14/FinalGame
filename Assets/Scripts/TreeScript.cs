using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeScript : MonoBehaviour {
	public float acceleration;
	public float speed;
    public ObstacleScript obstacleScript;
	//private GameController gc;
	private Text scoreBoard;


 
    //check for the 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("treeFilter"))
        {
            gameObject.SetActive(false);
            obstacleScript.availableTrees.Add(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		//gc = FindObjectOfType<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
    
	}
}
