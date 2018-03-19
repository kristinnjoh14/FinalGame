using UnityEngine;

public class BoltCollision : MonoBehaviour {

    public Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody.tag.Equals("Tree"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
