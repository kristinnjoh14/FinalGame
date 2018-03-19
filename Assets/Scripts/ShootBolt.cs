using UnityEngine;

public class ShootBolt : MonoBehaviour {

    public string key;
    public Rigidbody rb;
    public GameObject ammunition;
    public float shootForce = 100f;
    public float cooldownTimer;
    private float cooldownFull;

    // Use this for initialization
    void Start ()
    {
        cooldownFull = cooldownTimer; // To reset the cooldown
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(0 < cooldownTimer)
        {
            cooldownTimer -= Time.deltaTime;
        }
		
        if(Input.GetKeyDown(key) && cooldownTimer <= 0)
        {
            var newObject = Instantiate(ammunition, gameObject.transform); //Create the bullet
            newObject.transform.position = transform.position + -rb.transform.right;

            newObject.transform.LookAt(newObject.transform, -transform.right);
            Vector3 temp = new Vector3(-1,0,0);
            newObject.GetComponent<Rigidbody>().AddForce(temp * shootForce);

            Destroy(newObject, 3.0f); // Cleanup
            cooldownTimer = cooldownFull; // Cooldown reset
        }
	}
}
