using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkierMovement : MonoBehaviour {
	public float speed;
	public Text retryText;
	public float bodyTilt;
	public float maxSpeed;

	private float orgZrot;
	private Quaternion orgRotation;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		orgRotation = rb.rotation;
		orgZrot = orgRotation.eulerAngles.z;
        retryText = GameObject.FindGameObjectWithTag("RetryText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		float deltaVel = maxSpeed - rb.velocity.magnitude;
		if (Application.isEditor) {
			if (Input.GetKey (KeyCode.A)) {
				if (rb.velocity.z > 0) {
					deltaVel = 2*maxSpeed - deltaVel;
				}
				rb.AddForce (0, 0, -speed*0.5f*deltaVel);
				rb.rotation = Quaternion.Euler (-bodyTilt, 0, orgZrot);
			} else if (Input.GetKey (KeyCode.D)) {
				if (rb.velocity.z < 0) {
					deltaVel = 2*maxSpeed - deltaVel;
				}
				rb.AddForce (0, 0, speed*0.5f*deltaVel);
				rb.rotation = Quaternion.Euler (bodyTilt, 0, orgZrot);
			} else if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.A)) {
				rb.rotation = orgRotation;
			}
		} else {
			rb.AddForce (0, 0, Input.acceleration.x * speed * deltaVel);
			rb.rotation = Quaternion.Euler (bodyTilt*Input.acceleration.x, 0, orgZrot);
		}
		//Debug.Log (deltaVel);
	}

	//Called upon collision, stops time and sets retry text
	public void OnCollisionEnter (Collision coll) {
        if(coll.gameObject.tag.Equals("Tree"))
        {
            retryText.text = "Tap to retry";
        }
	}
}
