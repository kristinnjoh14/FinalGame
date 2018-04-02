using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Vector3 camoffset;
	public SkierMovement skier;
	public Transform head;
    
	public float minZ;
	public float maxZ;
	// Use this for initialization
	public float t = 0.5f;
	public float zOffsetFactor = 100f;

	void Start () {
		skier = FindObjectOfType<SkierMovement> ();
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log (skier.transform.position);
		//current position =
		Vector3 cCameraPos = transform.position;
		//next-ideal position
		camoffset.z = Mathf.Clamp(-(head.position.z * zOffsetFactor), minZ, maxZ);

		Vector3 nextPos = head.gameObject.transform.position + camoffset;

		transform.position = Vector3.Lerp (cCameraPos, nextPos, t);
		transform.LookAt (head.position + new Vector3 (0,2,0));
	}
}
