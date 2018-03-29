using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour {

    public Text scoreText;
    private GameObject coinController;

    // Use this for initialization
    void Start () {
        coinController = GameObject.FindWithTag("CoinController");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void coinVanish(Collider other)
    {
        other.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin"))
        {
            coinController.GetComponent<CoinScript>().coinCollect++;
            other.GetComponent<Renderer>().enabled = false;
            scoreText.text = coinController.GetComponent<CoinScript>().coinCollect.ToString();
            Invoke("coinVanish(other)", 2.0f);
        }
    }
}
