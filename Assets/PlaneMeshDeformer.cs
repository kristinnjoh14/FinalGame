using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMeshDeformer : MonoBehaviour {

    private Mesh mesh;

    private void Awake()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
    }


    public void PerlinMesh(float xOffset, float yOffset, float scale)
    {

        Vector3[] tempArray = mesh.vertices;
        
        for (int i = 0; i < tempArray.Length; i++)
        {
                tempArray[i].y += Mathf.PerlinNoise((xOffset + tempArray[i].x) / scale, (yOffset + tempArray[i].z) / scale) * 100;
        }

        mesh.vertices = tempArray;
        mesh.RecalculateNormals();

    }


    // Use this for initialization
    void Start () {
        PerlinMesh(1f, 1f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
