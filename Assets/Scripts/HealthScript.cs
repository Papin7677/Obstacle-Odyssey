using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public GameObject player;
    [SerializeField] private GameObject ObstacleGenerator;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private ParticleSystem heartExplosion3;
    [SerializeField] private ParticleSystem heartExplosion2;
    [SerializeField] private ParticleSystem heartExplosion1;
    [SerializeField] private GameObject CoinGenerator;
    private CoinManager coinManager;


    public void Start()
    {
        health = 3;
    }
   
    public void takeDamage()
    {
        health--;
        if (health == 2)
        {
            heartExplosion3.Play();
            Destroy(heart3);

        }
        if (health == 1)
        {
            heartExplosion2.Play();
            Destroy(heart2);
        }
        if (health == 0)
        {
            Die();
        }
    }
    public void Die()
    {
        coinManager = GameObject.Find("CoinsText").GetComponent<CoinManager>();
        GameObject.Find("Coin Generator").GetComponent<CoinGenerator>().die();
        GameObject.Find("Score").GetComponent<ScoreSystem>().die();
        heartExplosion1.Play();
        Destroy(heart1);
        ObstacleGenerator.GetComponent<ObstacleManager>().spawnRate = 0f;
        CoinGenerator.SetActive(false);
        DeathPanel.SetActive(true);
        SaveSystem.SaveData(coinManager);

        GameObject[] coins = GameObject.FindGameObjectsWithTag("coin");

        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
        
    }

}
