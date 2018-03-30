using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Vector3 camoffset;
	public SkierMovement skier;
	public Transform head;
	// Use this for initialization
	void Start () {
		skier = FindObjectOfType<SkierMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (skier.transform.position);
		camoffset.z = -6*(skier.rb.velocity.z / skier.maxSpeed);
		transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position + camoffset;
		transform.LookAt (head.position + new Vector3 (0,2,0));
	}
}
