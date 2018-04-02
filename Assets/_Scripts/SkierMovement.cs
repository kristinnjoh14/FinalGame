using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkierMovement : MonoBehaviour {
    public GameController gc;
	public float speed;
	public Text retryText;
	public float bodyTilt;
	public float maxSpeed;
	public float maxBodyTilt;
	public float boardTiltFactor;

	//public float maxSpeedDiff;		//Maximum difference between "current" scrollspeed and actual scrollspeed (Current keeps being incremented)
	public bool onGround;
	private float orgZrot;
	private Quaternion orgRotation;
	public Rigidbody rb;
	private SkiSoundManager sm;
    /// <summary>
    /// How close should a tree be until we start slowing our dude down
    /// </summary>
    public float slowDownProximity;
    /// <summary>
    /// this is the curve that decides how the slow motion effect kicks in.
    /// </summary>
    public AnimationCurve sloMoCurve;
    private float baseFov = 70f;
    public float fovChange = 10f;

	private PlaneController pc;
    
    /// <summary>
    /// The minimum time scale.
    /// </summary>
    public float minTimeScale = 0.20f;

    public List<Collider> nearList;
	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		orgRotation = rb.rotation;
		orgZrot = orgRotation.eulerAngles.z;
		onGround = true;
		sm = GetComponent<SkiSoundManager> ();
		gc = FindObjectOfType<GameController> ();
		pc = FindObjectOfType<PlaneController> ();
		//retryText = GameObject.FindGameObjectWithTag ("RetryScreen").GetComponent<Text> ();
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

    private void addZForceLeft(float deltaVel)
    {
        if (rb.velocity.z > 0)	//If movement is opposite velocity, force is greater proportional to how fast you are going
        {
			sm.hardTurnSound (deltaVel/maxSpeed);
            deltaVel = 2 * maxSpeed - deltaVel;
        }
        rb.AddForce(0, 0, -speed * deltaVel);
    }

    private void addZForceRight(float deltaVel)
    {
        if (rb.velocity.z < 0)
        {
			sm.hardTurnSound (deltaVel/maxSpeed);
            deltaVel = 2 * maxSpeed - deltaVel;
        }
        rb.AddForce(0, 0, speed * deltaVel);
    }

    // Update is called once per frame
    void Update () {
		maxSpeed = pc.scrollSpeed;
        bool isInput = false;
		float tiltX = Mathf.Clamp(gc.inputSensitivity.value*gc.AdjustedAccelerometer.x, -1, 1);
		//float tiltZ = gc.AdjustedAccelerometer.z;

		//Tree stuff if not paused
		if (!gc.lost && !gc.settingsContainer.activeSelf)
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
                if(minimumDistance < slowDownProximity)
                {
                    float curveValue = sloMoCurve.Evaluate(minimumDistance / slowDownProximity);

                    float dee = (1f - minTimeScale) * curveValue;
                    dee += minTimeScale;
                    Time.timeScale = dee;
                    Time.fixedDeltaTime = Time.timeScale * 0.01f;
                }
				else 
				{
                    Time.timeScale = 1f;
                }
            } 
			else {
                Time.timeScale = 1;
            }
            /* <!-- ABOVE IS THE PART WHERE THE TREE-DODGE SLOW MOTION EFFECT HAPPENS --> */
        }

        float deltaVel = maxSpeed - rb.velocity.magnitude;
		if (Time.timeScale > 0) {	//All input
			//Dev buttons for testing in editor
			if (Application.isEditor) {	
	            if (Input.GetKey (KeyCode.A) || ( gc.LeftButtonPressed && !gc.useTiltControls ) ) {
	                addZForceLeft(deltaVel);
	                isInput = true;
					bodyTilt -= Time.deltaTime * 100;
				} else if (Input.GetKey (KeyCode.D) || (gc.RightButtonPressed && !gc.useTiltControls) ) {
	                addZForceRight(deltaVel);
	                isInput = true;
					bodyTilt += Time.deltaTime * 100;
	            }
	            /*else if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.A)) {
					rb.rotation = orgRotation;
				}*/
			} else {
				//Actual device controls
	            if (gc.useTiltControls) {		
					//Tilt controls
					if ((rb.velocity.z > 0 && tiltX < 0) || (rb.velocity.z < 0 && tiltX > 0)) {	//If movement is opposite velocity, force is greater proportional to how fast you are going
						sm.hardTurnSound (deltaVel / maxSpeed);
					}
					bodyTilt += Time.deltaTime * 100 * tiltX;
					rb.AddForce(0, 0, tiltX * speed * deltaVel);
					/*pc.scrollSpeed += tiltZ*Time.deltaTime*speed;
					if(pc.scrollSpeed < (pc.referenceSpeed - maxSpeedDiff)) {
						pc.scrollSpeed = pc.referenceSpeed - maxSpeedDiff;
					}
					rb.rotation = Quaternion.Euler (bodyTilt*tiltX, 0, bodyTilt*tiltZ);
					Camera.main.fieldOfView += (tiltZ*Time.deltaTime*speed);
					Camera.main.fieldOfView = Mathf.Clamp (Camera.main.fieldOfView, 60, 100);*/
	            } else {
					//Tap controls
					//rb.rotation = orgRotation;
					if (gc.LeftButtonPressed) {
						addZForceLeft (deltaVel);
						isInput = true;
						bodyTilt -= Time.deltaTime * 100;
					} else if (gc.RightButtonPressed) {
						addZForceRight (deltaVel);
						isInput = true;
						bodyTilt += Time.deltaTime * 100;
					}
	            }
	        }
		}
		//rb.velocity -= rb.velocity * 2.5f * Time.deltaTime;
		if (!gc.settingsContainer.activeSelf) {
			bodyTilt -= 0.09f * bodyTilt;
		}
		//Rotation by current speed
		if (bodyTilt > maxBodyTilt) {
			bodyTilt = maxBodyTilt;
		} else if (bodyTilt < -maxBodyTilt) {
			bodyTilt = -maxBodyTilt;
		}
		rb.rotation = Quaternion.Euler(Mathf.Clamp(bodyTilt + (rb.velocity.z / maxSpeed)*boardTiltFactor, -35, 35), Mathf.Rad2Deg * Mathf.Atan(rb.velocity.z / pc.scrollSpeed), orgZrot);
	}

	/*public void FixedUpdate () {
		//Rotation by current speed
		if (bodyTilt > maxBodyTilt) {
			bodyTilt = maxBodyTilt;
		} else if (bodyTilt < -maxBodyTilt) {
			bodyTilt = -maxBodyTilt;
		}
		rb.rotation = Quaternion.Euler(bodyTilt, Mathf.Rad2Deg * (rb.velocity.z / maxSpeed), orgZrot);
	}*/




	//Called upon collision, stops time and sets retry text
	/*public void OnCollisionEnter (Collision c2) {
        //Debug.Log(Vector3.Distance(c2.transform.position, transform.position));
		if (c2.gameObject.CompareTag ("Tree")) {
			Time.timeScale = 0;
	        gc.lost = true;
			retryText.text = "Tap to retry";
		}
	}*/
}
