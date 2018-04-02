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
    public SkierMovement player;

 
    //check for the 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("treeFilter"))
        {
            gameObject.SetActive(false);
            obstacleScript.availableTrees.Add(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("model"))
        {
            player.treeCollision();
        }
        //Debug.Log(collision.gameObject.tag);
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("SnowBoardbone").GetComponent<SkierMovement>();
		//gc = FindObjectOfType<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
    
	}
}
