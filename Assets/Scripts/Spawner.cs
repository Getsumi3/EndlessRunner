using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //room prefab
    public GameObject roomPrefab;
    private GameObject currentRoom;
    //amount of rooms that will be created and added to pool at start;
    public int rooms = 50;
    //room length, so the next room will spawn in position+=roomLength
    public float roomLength = 12.6f;
    //reference to player
    public GameObject player;
    private Vector3 _spawnPoint = Vector3.zero;
    //list of rooms in the pool
    private Stack<GameObject> pooledRooms = new Stack<GameObject>();

    public Stack<GameObject> PooledRooms
    {
        get
        {
            return pooledRooms;
        }

        set
        {
            pooledRooms = value;
        }
    }


    public static Spawner instance;
    public static Spawner Instance
    {
        get
        {
            return instance;
        }
    }
    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        CreateInstance();
    }
    // Use this for initialization
    void Start () {

        currentRoom = roomPrefab;
        CreateRooms();
        for (int i = 0; i < rooms; i++)
        {
            SpawnRooms();  
        }

	}

    /// <summary>
    /// Spawn created rooms from the pool
    /// </summary>
    public void SpawnRooms()
    {
        //if no rooms in pool, create new rooms
        if (PooledRooms.Count == 0)
        {
            CreateRooms();
        }

        //get new room from pool
        //set the new room active
        //set new room position at prev room pos+roomLength
        //and set new room as current room
        GameObject room = PooledRooms.Pop();
        room.SetActive(true);
        room.transform.position = new Vector3(currentRoom.transform.position.x + roomLength, 0, 0);
        currentRoom = room;

        ////spawn obstacles in the room;
        //int spawnObstacle = Random.Range(0, Room.instance.obstacles.Length);

        //currentRoom.transform.GetChild(0).transform.GetChild(spawnObstacle).gameObject.SetActive(true);

        //int spawnCoins = Random.Range(0, 3);
        //currentRoom.transform.GetChild(1).transform.GetChild(spawnCoins).gameObject.SetActive(true);

    }

    /// <summary>
    /// Create new rooms and add them to pool
    /// </summary>
    public void CreateRooms()
    {
        for (int i = 0; i < rooms; i++)
        {
            GameObject room = Instantiate(roomPrefab, _spawnPoint, Quaternion.identity, gameObject.transform) as GameObject;
            PooledRooms.Push(room);
            PooledRooms.Peek().SetActive(false);
        }
    }
	
}
