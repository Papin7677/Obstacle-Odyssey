using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ObastacleBehaviour : MonoBehaviour
{
    public GameObject score;
    private ScoreSystem scoreSystem;


    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Game")
        {
            score = GameObject.Find("Score");
            scoreSystem = score.GetComponent<ScoreSystem>();
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the obstacle collides with an object tagged as "Floor"
        if (other.CompareTag("Floor"))
        {
            // Destroy the obstacle when it hits the floor
            Destroy(gameObject);
            scoreSystem.addScore();
        }
    }
}
