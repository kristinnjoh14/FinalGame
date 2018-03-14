using UnityEngine;

public class RagdollImpact : MonoBehaviour {

    public GameObject replacement;
    public GameObject original;
    public PlaneController planeController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag.Equals("Tree"))
        {   // if collided with the right object...
            // activate replacement at same position and with same rotation:

            original.SetActive(false);
            replacement.SetActive(true);
            planeController.scrollSpeed = 0;
            planeController.acceleration = 0;
        }
    }
}
