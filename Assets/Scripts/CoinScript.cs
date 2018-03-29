using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    public float spawnRate;
    public float spawnOffset;
    public int coinPoolSize = 100;
    public GameObject[] coins = new GameObject[3];
    public int coinCollect = 0;
    //public List<Transform> trees = new List<Transform> ();

    public List<GameObject> availableCoins;
    public PlaneController pc;
    //private float planeSpeed;
    private float Zrange;
    private GameObject player;
    private float spawnCountdown;
    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < coinPoolSize; i++)
        {
            //create a random tree.
            GameObject coin = Instantiate(coins[Random.Range(0, 3)]);
            coin.SetActive(false);
            //add new tree to availables list
            availableCoins.Add(coin);
        }



        spawnCountdown = spawnRate;
        player = GameObject.FindGameObjectWithTag("Player");
        Zrange = (pc.planeWidth * 3) / 2;

    }

    // Update is called once per frame
    void Update()
    {
        //planeSpeed = GetComponentInParent<PlaneController> ().scrollSpeed;
        spawnCountdown -= Time.deltaTime;
        if (spawnCountdown < 0)
        {   //Once countdown reaches 0, spawn a tree at a random offset from the player


            if (availableCoins.Count != 0)

            {
                //get first tree in list.
                GameObject coin = availableCoins[0];
                //remove tree from list.
                availableCoins.RemoveAt(0);
                // set the transform
                coin.transform.position = new Vector3(-spawnOffset, 0.5f, player.transform.position.z - Zrange + Random.value * 2 * Zrange);
                //set the tree as a 
                coin.transform.SetParent(pc.activePlane.transform);
                //enable the tree
                coin.SetActive(true);

                spawnCountdown = spawnRate;

            }
        }
    }
}