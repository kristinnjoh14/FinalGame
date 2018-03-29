using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
	private static CanvasController instance;
	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
