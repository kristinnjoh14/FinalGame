using UnityEngine;

public class RagdollImpact : MonoBehaviour {

    public GameObject replacement;
    public GameObject original;
    public static Transform[] underBodies;
    private GameObject dead;

	// Use this for initialization
	void Start () {

        underBodies = GetComponentsInChildren<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag.Equals("Tree"))
        {   // if collided with the right object...
            // create replacement at same position and with same rotation:

            dead = Instantiate(replacement, original.transform.position, original.transform.rotation);

            // copy position and rotation to the children recursively:
            CopyTransformsRecurse(original.transform, replacement.transform);
            // destroy the original object:
            //Destroy(original);
            original.SetActive(false);
        }
    }
    static void CopyTransformsRecurse(Transform src, Transform dst)
    {
        dst.position = src.position;
        dst.rotation = src.rotation;
        dst.gameObject.active = src.gameObject.active;
        Debug.Log("Is Underbodies null? " + (underBodies == null));

        foreach (Transform body in underBodies)
        {
            Debug.Log("Is this one null? " + (body == null));
            // match the transform with the same name
            var curSrc = src.Find(body.name);
            if (curSrc) CopyTransformsRecurse(curSrc, body);
        }
    }
}
