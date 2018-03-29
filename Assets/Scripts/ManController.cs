﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public HingeJoint leftLegHinge, rightLegHinge;

    public List<Pair> rbs;
    /*
    public Rigidbody chestRb, hipRb, neckRb,
        lLegRb, rLegRb, 
        lShinRb, rShinRb,
        lFootRb, rFootRb,
        lArmRb, rArmRb,
        lForeArmRb, rForeArmRb,
        lHandRb, rHandRb;
    Vector3 iChestRotation, iHipRotation, iNeckRotation,
        iLeftLegRotation, iRightLegRotation,
        iLeftShinRotation, iRightShinRotation,
        iLeftFootRotation, iRightFootRotation,
        iLeftArmRotation, iRightArmRotation,
        iLeftForeArmRotation, iRightForeArmRotation,
        iLeftHandRotation, iRightHandRotation;

    */
    void updateBodyPart(Pair p)
    {

        //get rb current rotation;
        Quaternion currentRotation = p.rb.rotation;
        //find the difference btw, 
        // diff =  currentRotation - p.rot;
        //apply that difference as torque wrt. deltatime and factor to rigidbody
        p.rb.rotation = Quaternion.Slerp(p.rot, currentRotation, factor);
        ///rb.AddRelativeTorque(diff * Time.deltaTime * factor);

    }


	// Use this for initialization
	void Start () {

        //don't grade us on this please.
        //get all rigidbodys in chillen
        Rigidbody[] rs = gameObject.GetComponentsInChildren<Rigidbody>();
        rbs = new List<Pair>();
        
        foreach( Rigidbody r in rs)
        {
            Pair t = new Pair(r, r.rotation);
            Debug.Log(t.rot);
            rbs.Add(t);
           
        }

        //loop through rbs and assign them to the correct rbs, and initial rotations. 
        /*
        foreach(Rigidbody r in rs)
        {
            Debug.Log(r.gameObject.name);

            Vector3 rot = r.rotation.eulerAngles;

            switch ( r.gameObject.name )
            {

                case "Hips":
                    hipRb = r;
                    iHipRotation = rot;
                    break;

                case "Chest":
                    chestRb = r;
                    iChestRotation = rot;
                    break;

                case "arm.l":
                    lArmRb = r;
                    iLeftArmRotation = rot;
                    break;

                case "arm.r":
                    rArmRb = r;
                    iRightArmRotation = rot;
                    break;

                case "foreArm.l":
                    lForeArmRb = r;
                    iLeftForeArmRotation = rot;
                    break;

                case "foreArm.r":
                    rForeArmRb = r;
                    iRightForeArmRotation = rot;
                    break;

                case "hand.l":
                    lHandRb = r;
                    iLeftHandRotation = rot;
                    break;

                case "hand.r":
                    rHandRb = r;
                    iRightHandRotation = rot;
                    break;

                case "leg.l":
                    lLegRb = r;
                    iLeftLegRotation = rot;
                    break;

                case "leg.r":
                    rLegRb = r;
                    iRightLegRotation = rot;
                    break;

                case "shin.l":
                    lShinRb = r;
                    iLeftShinRotation = rot;
                    break;

                case "shin.r":
                    rShinRb = r;
                    iRightShinRotation = rot;
                    break;

                case "foot.l":
                    lFootRb = r;
                    iLeftFootRotation = rot;
                    break;

                case "foot.r":
                    rFootRb = r;
                    iRightFootRotation = rot;
                    break;
                    
                default:
                    break;
            }
            

        }
        */


    }

    void releaseME()
    {

    }

	// Update is called once per frame
	void Update () {
		foreach(Pair p in rbs)
        {
            updateBodyPart(p);
        }
	}
}
