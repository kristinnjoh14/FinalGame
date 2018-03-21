using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxController : MonoBehaviour {

    public GameObject player;

    public GameObject plane;
    public MeshRenderer mr;

    public float scrollSpeed = 10f;

    private float oldZCenter;

    public float planeWidth;

    public float scale;

    public int textureHeight;
    public int textureWidth;

    private float yCoord = 0;
    private float xCoord = 0;
    public Material magicMaterial;

    public AnimationCurve lightnessCurve;

	public AnimationCurve whiteOut;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame

    private void Update()
    {

        Vector3 tmp = transform.position;

        float dz = Camera.main.transform.position.z - tmp.z ;
        tmp.z = Camera.main.transform.position.z;

        transform.position = tmp;

        xCoord += dz * 0.1f;

        yCoord += scrollSpeed;

        Texture2D tex = new Texture2D(textureWidth, textureHeight);
        tex.SetPixels(doNoise(xCoord, yCoord));
        tex.Apply();

        mr.material.mainTexture = tex;

    }

    Color[] doNoise(float xOffset, float yOffset)
    {
        Color[] pix = new Color[textureWidth * textureHeight];



        int y = 0;
        while (y < textureHeight)
        {
            int x = 0;

            while (x < textureWidth)
            {

                float _xCoord = xOffset + (float)x;

                
                float _yCoord = yOffset + (float)y;

                float yFactor = (float)(textureHeight - y) / (float)textureHeight;

                

                float sample = Mathf.PerlinNoise(_xCoord / scale, _yCoord / scale);

				Color white = new Color (1f, 1f, 1f);

				//float other = 

                
                pix[y * textureWidth + x] = new Color(
					/*red*/         (Mathf.Clamp(0.85f-sample, 0f,   1f)),//     * lightnessCurve.Evaluate(yFactor),
					/*green*/       (Mathf.Clamp(1f, 0.5f,    1f)),//     * lightnessCurve.Evaluate(yFactor),
					/*blue*/        (Mathf.Clamp(1f-sample, 0f, 1f))//     * lightnessCurve.Evaluate(yFactor)
                                            );
                x++;
            }
            y++;
        }

        return pix;
    }


}
