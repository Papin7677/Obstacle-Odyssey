using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class ScoreSystem : MonoBehaviour
{
    public GameObject highScoreObject;
    public GameObject scoreTextObject;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    
    private int speed;
    int scoreValueForUpdate = 20;
    public int score = 0;
    


    void Start()
    {
        Invoke("updateScore", 5);
        score = PlayerPrefs.GetInt("Score", 0);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        displayScore();
    }
    
    public void updateScore()
    {
        scoreValueForUpdate = scoreValueForUpdate + 5;
        Invoke("updateScore", 15);

    }
    public void displayScore()
    {
        scoreText.text = score.ToString("D7");
    }
    public void addScore()
    {
        score = score + scoreValueForUpdate;
    }

    public void die()
    {
        
            int highScore = Load();
            if(highScore<score){
                highScore = score;
            }
            highScoreText = highScoreObject.GetComponent<TextMeshProUGUI>();
            highScoreText.text = "HIGHSCORE \n" +highScore.ToString("D7");
            Save(highScore);
            
        
    }
    public void restart()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }










    public void Save(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, score);
        stream.Close();
    }


    public int Load()
    {
        string path = Application.persistentDataPath + "/score.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)formatter.Deserialize(stream);
            stream.Close();
            return data;

        }
        else
        {
            Debug.Log("save file not found!!!");
            return 0;
        }
    }
    
}
