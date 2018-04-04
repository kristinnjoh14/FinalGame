using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour {
	bool c = true;
	CameraController cc;
	// Use this for initialization
	void Start () {
		cc = GameObject.FindObjectOfType<CameraController> ();
	}

	void OnCollisionEnter(Collision coll){
		if( coll.gameObject.tag.Equals("ground")){
			if(c){
				c = false;
				if (cc != null) {
					cc.VigourousShake (0.2f, Mathf.Clamp(coll.impulse.magnitude * 2, 0f, 0.5f));
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
