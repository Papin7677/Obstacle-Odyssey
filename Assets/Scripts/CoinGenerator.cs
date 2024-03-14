using System.Collections;
using UnityEngine;



public class CoinGenerator : MonoBehaviour
{
    public GameObject coinPrefab;
    
    public float spawnInterval = 10f;
    public GameObject wall1;
    public GameObject wall2;
    private float wallOffset;
    float leftSide;
    float rightSide;

    private void Start()
    {
        wallOffset = wall1.GetComponent<BoxCollider2D>().bounds.size.x;
        leftSide = wall1.GetComponent<Transform>().position.x;
        rightSide = wall2.GetComponent<Transform>().position.x;
        Invoke("SpawnCoin", 8);
    }

    
    

    void SpawnCoin()
    {
        float randomX = UnityEngine.Random.Range(leftSide + 0.2f + wallOffset, rightSide - 0.2f - wallOffset);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);

        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        Invoke("SpawnCoin", spawnInterval);

    }
    public void die()
    {
        CancelInvoke("SpawnCoin");
    }
}
