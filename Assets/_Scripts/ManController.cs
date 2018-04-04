using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManController : MonoBehaviour {

    public struct Pair
    {
        public Rigidbody rb;
        public Quaternion rot;
        public float factor;

        public Pair(Rigidbody r, Quaternion rot)
        {
            this.rb = r;
            this.rot = rot;
            this.factor = 1f;
        }

    }
    [Range(0, 1)]
    public float factor = 1f;

    public bool doMeOnce = true;

    public HingeJoint leftLegHinge, rightLegHinge, hipHinge;
	public List<HingeJoint> allJoints;
    public List<Pair> rbs;

	private GameController gc;
 
    void updateBodyPart(Pair p)
    {
        //get rb current rotation
        Quaternion currentRotation = p.rb.rotation;
        p.rb.AddTorque((p.rot.eulerAngles-  p.rb.rotation.eulerAngles) * Time.deltaTime);
        p.rb.rotation = Quaternion.Slerp(p.rot, currentRotation, factor);
        
    }

	// Use this for initialization
	void Start () {
        //don't grade us on this please.
        //get all rigidbodys in chillen
        Rigidbody[] rs = gameObject.GetComponentsInChildren<Rigidbody>();
        rbs = new List<Pair>();
        
        foreach( Rigidbody r in rs)
        {

			BodyPartController bpc = new BodyPartController ();
			r.gameObject.AddComponent<BodyPartController> ();

            r.gameObject.tag = "model";
            Pair t = new Pair(r, r.rotation);
            //Debug.Log(t.rot);
            rbs.Add(t);
			r.maxDepenetrationVelocity = 1/111000;
			foreach(HingeJoint j in r.gameObject.GetComponents<HingeJoint> ()) {
				allJoints.Add (j);
			}
        }
		gc = FindObjectOfType<GameController> ();
    }

    public void releaseME()
    {
        Destroy(leftLegHinge);
        Destroy(rightLegHinge);
        Destroy(hipHinge);



        this.factor = 0.9f;

        foreach (Pair p in rbs)
        {
			BoxCollider b = p.rb.gameObject.GetComponent<BoxCollider> ();
			if (b != null) {
				b.enabled = (true);
			}
			//p.rb.maxDepenetrationVelocity = 0.000001f;
			//p.rb.maxAngularVelocity = 1;
			p.rb.drag = 0.2f;
			//p.rb.mass = 5;
			//p.rb.angularDrag = 1000;
			p.rb.useGravity = true;
			//p.rb.AddExplosionForce (5000,new Vector3 (-1,0,0), 10);
            if (!p.rb.gameObject.name.Equals("Armature") && !p.rb.gameObject.name.Equals("Hips"))
            {
                JointSpring j = new JointSpring();
                j.spring = 10;
                p.rb.gameObject.GetComponent<HingeJoint>().spring = j;
            }
			p.rb.AddForce(new Vector3(-5500, 150, p.rb.velocity.z/5));
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByBuildIndex (0) || !gc.lost) {
			foreach(Pair p in rbs)
	        {
	            updateBodyPart(p);
	        }
		}
	}
}
