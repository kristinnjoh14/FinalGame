﻿using UnityEngine;

public class ShootBolt : MonoBehaviour {

    public string key;
    public Rigidbody rb;
    public GameObject ammunition;
    public float shootForce = 100f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if(Input.GetKeyDown(key))
        {
            var newObject = Instantiate(ammunition, gameObject.transform); //Create the bullet
            newObject.transform.position = transform.position + -rb.transform.right;

            newObject.transform.LookAt(newObject.transform, -transform.right);
            newObject.GetComponent<Rigidbody>().AddForce(-transform.right * shootForce);
            Destroy(newObject, 3.0f);
        }
	}
}
