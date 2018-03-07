using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkierMovement : MonoBehaviour {
	public float speed;
	public Text retryText;
	public float bodyTilt;
	public float maxSpeed;
    private bool lost;
	private float orgZrot;
	private Quaternion orgRotation;
	private Rigidbody rb;
    /// <summary>
    /// How close should a tree be until we start slowing our dude down
    /// </summary>
    public float slowDownProximity;

    /// <summary>
    /// The minimum time scale.
    /// </summary>
    public float minTimeScale = 0.20f;

    public List<Collider> nearList;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		orgRotation = rb.rotation;
		orgZrot = orgRotation.eulerAngles.z;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Tree"))
        {
            if (!nearList.Contains(other))
            {
                nearList.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Tree"))
        {
            if (nearList.Contains(other))
            {
                nearList.Remove(other);
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (!lost)
        {
            /* <!-- THIS IS THE PART WHERE THE SLOW MOTION EFFECT IS DONE!!! --> */
            GameObject nearestTree = null;
            float minimumDistance = float.MaxValue;


            
            //nearlist is a list of only gameobjects with the tag 'Tree'
            foreach (Collider item in nearList)
            {

                float dist = Vector3.Distance(this.transform.position, item.transform.position);
                if (dist < minimumDistance)
                {
                    nearestTree = item.gameObject;
                    minimumDistance = dist;
                }

            }


            if (nearestTree != null)
            {
                //Debug.Log(minimumDistance);
                //do some shit
                if(minimumDistance < 20f)
                {
                    float d = minimumDistance / 40f;
                    Time.timeScale = Mathf.Clamp(d, minTimeScale, 1f);
                    Time.fixedDeltaTime = Time.timeScale * 0.02f;
                }   else
                {
                    Time.timeScale = 1f;
                }
            
            } else
            {
                Time.timeScale = 1;
            }
            /* <!-- ABOVE IS THE PART WHERE THE TREE-DODGE SLOW MOTUION EFFECT --> */
        }

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
	public void OnCollisionEnter (Collision c2) {
        /*Debug.Log(Vector3.Distance(c2.transform.position, transform.position));
		Time.timeScale = 0;
        lost = true;
		retryText.text = "Tap to retry";
        */
	}
}
