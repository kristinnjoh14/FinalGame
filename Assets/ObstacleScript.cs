using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
	public float spawnRate;
	public float spawnOffset;
	public Transform[] trees = new Transform[3];
	//public List<Transform> trees = new List<Transform> ();

	//private float planeSpeed;
	private float Zrange;
	private GameObject player;
	private float spawnCountdown;
	// Use this for initialization
	void Start () {
		spawnCountdown = spawnRate;
		player = GameObject.FindGameObjectWithTag ("Player");
		Zrange = (GetComponentInParent<PlaneController> ().planeWidth * 3) / 2;
	}

	// Update is called once per frame
	void Update () {
		//planeSpeed = GetComponentInParent<PlaneController> ().scrollSpeed;
		spawnCountdown -= Time.deltaTime;
		if (spawnCountdown < 0) {	//Once countdown reaches 0, spawn a tree at a random offset from the player
			Transform tree = trees[Random.Range (0, 3)];
			Transform newTree = Instantiate (tree, new Vector3 (-spawnOffset, 0, player.transform.position.z - Zrange + Random.value*2*Zrange), Quaternion.Euler (0, Random.Range (0, 360), 0));
			//trees.Add (newTree);
			//newTree.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (planeSpeed, 0, 0);
			//Reset counter, += to include what little time was overkill for counter to reach 0
			spawnCountdown += spawnRate;
		}
		/*foreach (Transform tree in trees.ToArray ()) {
			tree.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (planeSpeed, 0, 0);
			if (tree.position.x > 5) {
				trees.Remove (tree);
				Destroy (tree.gameObject);
			}
		}*/
	}
}
