using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 5.0f;
    public float obstacleSpeed = 2.0f;
    public GameObject wall1;
    public GameObject wall2;
    private float nextSpawnTime = 0f;
    private float wallOffset;
    float leftSide;
    float rightSide;
    public BackgroundDatabase backgroundDB;
    float maxSpawnRate= 0;

    void Start()
    {
         selectObstacle();
         wallOffset = wall1.GetComponent<BoxCollider2D>().bounds.size.x;
         leftSide = wall1.GetComponent<Transform>().position.x;
         rightSide = wall2.GetComponent<Transform>().position.x;
         Invoke("increaseSpawnRate", 5);

    }
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
        if(spawnRate>maxSpawnRate){
            maxSpawnRate = spawnRate;
        }

    }
    void selectObstacle()
    {
        int data = 0;
        string path = Application.persistentDataPath + "/background.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = (int)formatter.Deserialize(stream);
            stream.Close();
           

        }
        else
        {
            Debug.Log("save file not found!!!");
            return;
        }
        Background bg = backgroundDB.GetBackground(data);
        obstaclePrefab = bg.obstacle;
    }

    void SpawnObstacle()
    {
        
        
        float spawnX = Random.Range(leftSide+0.2f+wallOffset, rightSide - 0.2f-wallOffset); // Adjust the range based on your game layout
        Vector3 spawnPosition = new Vector3(spawnX, 7f, 0f);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // Adjust the obstacle speed
        Rigidbody2D obstacleRb = obstacle.GetComponent<Rigidbody2D>();
        if (obstacleRb != null)
        {
            obstacleRb.velocity = new Vector2(0f, -obstacleSpeed);
        }
    }
    void increaseSpawnRate()
    {
        spawnRate = spawnRate + 0.1f;
        Invoke("increaseSpawnRate", 3);
    }
   
}
