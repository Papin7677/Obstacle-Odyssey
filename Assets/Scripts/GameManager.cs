using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance{get; private set;}

    public static int gamePlayed = 1;
    static int score;
    static float spawnRate;

    void Awake(){
        if(Instance ==null&& Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Debug.Log(score);
    }

    // Update is called once per frame
    void Update()
    {
        if(gamePlayed%3==0){
            gamePlayed = 1;
        }
    }
    public static void Restart()
    {
        score = GameObject.Find("Score").GetComponent<ScoreSystem>().score;
        spawnRate = GameObject.Find("Obstacle Generator").GetComponent<ObstacleManager>().spawnRate;
        Debug.Log(score);
    }
}
