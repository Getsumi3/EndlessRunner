using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public static Room instance;
    public GameObject[] obstacles;
    public GameObject[] coins;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        SpawnObjects();
    }
    public void SpawnObjects()
    {
        int randObstacle = Random.Range(0, obstacles.Length);

        
            obstacles[randObstacle].SetActive(true);
        
        int randCoin = Random.Range(0, coins.Length);
        
            coins[randCoin].SetActive(true);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //spawn next room when player enter this one
            Spawner.Instance.SpawnRooms();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("DestroyRoom", 1f);
        }
    }

    void DestroyRoom()
    {
        //push back to pool when player exit this room
        Spawner.Instance.PooledRooms.Push(gameObject);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
        foreach (GameObject coin in coins)
        {
            coin.SetActive(false);
        }
        CancelInvoke();
    }
}
