using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRow : MonoBehaviour
{
    public static float speed;
    public static bool isSpawning = true;
    [HideInInspector] public float ilkHiz = 0f;
    private bool newLineSpawned;
    private List<GameObject> obstacles;
    private Object[] objects;
    private GameObject coin;
    private Vector3 startPos, endPos;



    [SerializeField] private float speed_temp;



    void Awake()
    {
        // Set new line position to the top of the screen before anything else.
        transform.position = new Vector3(transform.position.x, 15, transform.position.z);
    }


    void Start()
    {
        LoadObstacles();
        LoadCoin();
        SpawnLineOfObstacles();

        startPos = transform.position;
        endPos = new Vector3(transform.position.x, -15, transform.position.z);


        float skoryuvarlama = (float)Score.GetAmount() / 10;
        speed = ilkHiz + skoryuvarlama;
    }


    void Update()
    {
        speed_temp = speed;

        // If line hasn't reached end position.
        if (Mathf.Abs(transform.position.y - endPos.y) > 5)
        {
            // If obstacle speed is higher than 0.
            if (speed != 0)
            {
                // Move line to the bottom of the screen.
                transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);

                // How much line has to travel to spawn the new line.
                if (!newLineSpawned && Mathf.Abs(transform.position.y - startPos.y) > ObstacleLineSpawner.instance.newspawn_deltaheight)
                {
                    if (isSpawning)
                    {
                        // Spawn new line.
                        ObstacleLineSpawner.instance.SpawnLine();
                        newLineSpawned = true;
                    }
                }
            }
        }
        else
        {
            // Destroy line when it reaches endPos.
            Destroy(gameObject);
        }
    }


    // ----------------------------------LOAD OBSTACLE AND LINE------------------------------------
    // Load obstacles from resources.
    private void LoadObstacles()
    {
        obstacles = new List<GameObject>();

        if (PlayerPrefs.GetInt("isPlanetUsed" + 0) == 1 || PlayerPrefs.GetInt("isPlanetUsed" + 2) == 1) //dunyaveyavenus ise
        {
            objects = Resources.LoadAll("ObstaclesTas") as Object[];
        }
        else if (PlayerPrefs.GetInt("isPlanetUsed" + 1) == 1)      //mars ise
        {
            objects = Resources.LoadAll("ObstaclesTasMars") as Object[];
        }
        else if (PlayerPrefs.GetInt("isPlanetUsed" + 3) == 1)      //uzay ise
        {
            objects = Resources.LoadAll("ObstaclesUydu") as Object[];
        }

        foreach (Object item in objects)
        {
            obstacles.Add(item as GameObject);
        }
    }


    // Load coin from resources.
    private void LoadCoin()
    {
        coin = Resources.Load("Coin") as GameObject;
    }
    // ----------------------------------LOAD OBSTACLE AND LINE------------------------------------


    // ----------------------------------SPAWN NEW LINE------------------------------------
    // Spawn obstacles into the line.
    private void SpawnLineOfObstacles()
    {
        int minObstacles = ObstacleLineSpawner.instance.minObstacles;
        int maxObstacles = ObstacleLineSpawner.instance.maxObstacles;

        // Get random amount of obstacles that should be spawned into the line.
        int obstaclesAmount = Random.Range(minObstacles, maxObstacles);

        // Get all available lanes.
        List<int> availableLanes = new List<int>() { -2, -1, 0, 1, 2 };
        for (int i = 0; i < obstaclesAmount; i++)
        {
            // Get random lane index.
            int randomLaneIndex = Random.Range(0, availableLanes.Count);
            // Spawn obstacle in available line.
            SpawnObstacle(availableLanes[randomLaneIndex]);
            // Remove line, in which new obstacle was spawned, from available lines list.
            availableLanes.RemoveAt(randomLaneIndex);
        }

        // Check if coin should be spawned in the line.
        if (Random.value < ObstacleLineSpawner.instance.coinSpawnRate)
        {
            //Get random available line index.
            int randomLaneIndex = Random.Range(0, availableLanes.Count);
            // Spawn coin in available line.
            SpawnCoin(availableLanes[randomLaneIndex]);
        }
    }
    // ----------------------------------SPAWN NEW LINE------------------------------------


    // ----------------------------------SPAWN OBSTACLE AND COIN IN LINE------------------------------------
    // Spawn obstacle in one of five lanes.
    private void SpawnObstacle(int lane)
    {
        int randomObstacleIndex = Random.Range(0, obstacles.Count);
        float randomObstacleOffest = Random.Range(0f, ObstacleLineSpawner.instance.randomizeObstaclesOffest);

        Instantiate(obstacles[randomObstacleIndex], new Vector3(lane, 7 + randomObstacleOffest, 0), Quaternion.identity, transform);
    }


    // Spawn coin in one of five lanes.
    private void SpawnCoin(int lane)
    {
        float randomCoinOffest = Random.Range(0, ObstacleLineSpawner.instance.randomizeObstaclesOffest);

        Instantiate(coin, new Vector3(lane, 7 + randomCoinOffest, 0), Quaternion.identity, transform);
    }
    // ----------------------------------SPAWN OBSTACLE AND COIN IN LINE------------------------------------
}
