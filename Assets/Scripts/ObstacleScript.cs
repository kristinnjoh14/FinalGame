﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
	public float spawnRate;
	public float spawnOffset;
    public int TreePoolSize = 100;
	public GameObject[] trees = new GameObject[3];
    //public List<Transform> trees = new List<Transform> ();
    
    public List<GameObject> availableTrees;
    public PlaneController pc;
	//private float planeSpeed;
	private float Zrange;
	private GameObject player;
	private float spawnCountdown;
	// Use this for initialization
	void Start () {

        for(int i = 0; i < TreePoolSize; i++)
        {
            //create a random tree.
            GameObject tree = Instantiate(trees[Random.Range(0, 3)]);
            tree.SetActive(false);
            //set this as the new trees' obstacle controller 
            tree.GetComponent<TreeScript>().obstacleScript = this;
            //add new tree to availables list
            availableTrees.Add(tree);
        }

        

		spawnCountdown = spawnRate;
		player = GameObject.FindGameObjectWithTag ("Player");
		Zrange = (pc.planeWidth * 3) / 2;

	}


	// Update is called once per frame
	void Update () {
		//planeSpeed = GetComponentInParent<PlaneController> ().scrollSpeed;
		spawnCountdown -= Time.deltaTime;
		if (spawnCountdown < 0) {	//Once countdown reaches 0, spawn a tree at a random offset from the player
			

            if(availableTrees.Count != 0)

            {
                //get first tree in list.
                GameObject tree = availableTrees[0];
                //remove tree from list.
                availableTrees.RemoveAt(0);
                // set the transform
                tree.transform.position = new Vector3(-spawnOffset, 0, player.transform.position.z - Zrange + Random.value * 2 * Zrange);
                //set the tree as a 
                tree.transform.SetParent(pc.activePlane.transform);
                //enable the tree
                tree.SetActive(true);

                spawnCountdown = spawnRate;

            }

        }
		
	}
}
