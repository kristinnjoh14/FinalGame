using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Vector3 camoffset;
	public SkierMovement skier;
	public Transform head;
    
	public float camShakeFactor = 0.5f;

	public float minZ;
	public float maxZ;
	// Use this for initialization
	public float t = 0.5f;
	public float zOffsetFactor = 100f;
	public float deathSnowdown;

	void Start () {
		skier = FindObjectOfType<SkierMovement> ();
    }

	float time = 0f;



	IEnumerator vigorous(object[] o){
		float seconds = (float)o[0];
		float intensity = (float)o[1];

		//float prevFactor = camShakeFactor;
		float perframe = (intensity / Time.unscaledDeltaTime);
		camShakeFactor = intensity;
		/*while (seconds > 0f && camShakeFactor > 0f ) {
			seconds -= (Time.unscaledDeltaTime);
			Debug.Log (seconds);
			camShakeFactor -= perframe;
			yield return null;
		}*/
		while (camShakeFactor > 0) {
			camShakeFactor -= Time.deltaTime * 3;
			yield return null;
		}
	}



	public void VigourousShake(float seconds, float intensity){
		object[] o = new object[2]{ seconds, intensity };
		StartCoroutine ("vigorous", o);
	}

	void Update () {
		if(Time.timeScale != 0) {
			time = (time += Time.deltaTime * 100) % 255f;

			float xOffSet = Mathf.PerlinNoise (time, 0f) - 0.5f;
			float yOffSet = Mathf.PerlinNoise (time, 125f) - 0.5f;

			transform.position += new Vector3 (0f, yOffSet*camShakeFactor, xOffSet*camShakeFactor);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log (skier.transform.position);
		//current position =
		Vector3 cCameraPos = transform.position;
		//next-ideal position
		camoffset.z = Mathf.Clamp(-(skier.rb.velocity.z * zOffsetFactor), minZ, maxZ);

		Vector3 nextPos = head.gameObject.transform.position + camoffset;

		transform.position = Vector3.Lerp (cCameraPos, nextPos, t);
		transform.LookAt (head.position + new Vector3 (0,3,0));

		if (skier.gc.lost) {
			var tmp = GetComponentInChildren<ParticleSystem> ().main;
			tmp.simulationSpeed = deathSnowdown;
		}
	}
}
