using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {
	public float scrollSpeed;
	public float referenceSpeed;
	public float acceleration;
	public GameObject[] planes = new GameObject[2];
	public float planeWidth;

    public GameObject treekiller;

    public GameObject activePlane;

	private float oldZCenter;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		oldZCenter = 0;
		referenceSpeed = scrollSpeed;
        //set the further-away plane as the active one.
        activePlane = planes[1];
        treekiller.transform.position = new Vector3(treekiller.transform.position.x, transform.position.y, oldZCenter);
	}
	
	// Update is called once per frame
	void Update () {
		movePlanes ();
		shiftPlanes ();
	}


	public void movePlanes () {
		//Apply acceleration and move planes
		scrollSpeed += acceleration * Time.deltaTime;
		referenceSpeed += acceleration * Time.deltaTime;
		for (int i = 0; i < 2; i++) {
			Vector3 tmp = planes [i].transform.position;
			tmp.x += scrollSpeed * Time.deltaTime;
			planes[i].transform.position = tmp;
		}
	}

	public void shiftPlanes () {
		//Moves the set of planes far behind the player far in front of the player
		for (int i = 0; i < 2; i++) {

			if (planes [i].transform.position.x > 260) {
                
				Vector3 temp = planes [i].transform.position;
				temp.x -= 1000;
				planes [i].transform.position = temp;
                //Set the further away plane as the active plane.
                activePlane = planes[i];
			}

		}
		//Moves the right plane left or vice versa depending on where the player is
		for (int j = 0; j < 3; j++) {
            //if the player's position on the z axis is 
			if (player.transform.position.z + planeWidth/2 < oldZCenter && 
				planes[0].transform.GetChild (j).transform.position.z > oldZCenter) {

                Vector3 temp = planes [0].transform.GetChild (j).transform.position;
				temp.z -= 3*planeWidth;
				planes [0].transform.GetChild (j).transform.position = temp;
				temp = planes [1].transform.GetChild (j).transform.position;
				temp.z -= 3*planeWidth;
				planes [1].transform.GetChild (j).transform.position = temp;
				oldZCenter -= planeWidth;

                treekiller.transform.position = new Vector3(treekiller.transform.position.x, 
					treekiller.transform.position.y, treekiller.transform.position.z - planeWidth);

            }
            else if (player.transform.position.z - planeWidth/2 > oldZCenter && 
				planes[0].transform.GetChild (j).transform.position.z < oldZCenter) {

                Vector3 temp = planes [0].transform.GetChild (j).transform.position;
				temp.z += 3*planeWidth;
				planes [0].transform.GetChild (j).transform.position = temp;
				temp = planes [1].transform.GetChild (j).transform.position;
				temp.z += 3*planeWidth;
				planes [1].transform.GetChild (j).transform.position = temp;
				oldZCenter += planeWidth;

                treekiller.transform.position = new Vector3(treekiller.transform.position.x, 
					treekiller.transform.position.y, treekiller.transform.position.z + planeWidth);

            }
        }
	}

	/*Texture2D calcNoise() {
		return null;
	}*/
}