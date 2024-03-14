using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    public BackgroundDatabase backgroundDB;
    public GameObject backgroundImage;
    public GameObject obstacle;
    public GameObject ground;
    public static int index = 0;
    public  int selectedBackground;
    public Transform background;
    public Transform sampleBackground;
    public GameObject backgroundImageN;
    public GameObject select;
    
    private float yOffset;
   
    

    void Awake()
    {
        selectedBackground = Load();
        index = selectedBackground;
    }
    void Start()
    {
        loadBackground();
        spawnObstacle(index);
        loadGround();
    }

    void spawnObstacle(int index)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Store")
        {
            return;
        }

            Destroy(obstacle);
        Background bg = backgroundDB.GetBackground(index);
        obstacle = bg.obstacle;
        obstacle = Instantiate(obstacle, sampleBackground);
        obstacle.transform.localPosition = Vector2.zero;

        // Get the Rigidbody2D component of the obstacle
        Rigidbody2D obstacleRb = obstacle.GetComponent<Rigidbody2D>();

        // Disable the Rigidbody2D
        if (obstacleRb != null)
        {
            obstacleRb.simulated = false;
        }

        // Double the size of the obstacle
        obstacle.transform.localScale *= 2f;
    }
    

    void updateBackground(int index)
    {
        Destroy(backgroundImage);
        Background bg = backgroundDB.GetBackground(index);
        backgroundImage = bg.backgroundImage;
        backgroundImage = Instantiate(backgroundImage, sampleBackground);
        backgroundImage.transform.localPosition = Vector2.zero;
        yOffset = bg.yOffset;
        backgroundImage.transform.localPosition = new Vector2(0, yOffset);
        // Get the SpriteRenderer component of the backgroundImage
        SpriteRenderer spriteRenderer = backgroundImage.GetComponent<SpriteRenderer>();

        // Set the sorting order to -0.5
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = -1;
        }
        spawnObstacle(index);
        BackgroundStore.cost = bg.cost;
    }

    public void Next()
    {
        index++;
        if (index >= backgroundDB.BackgroundCount)
        {
            index = 0;
        }
        updateBackground(index);
        
    }
    public void SelectBackground()
    {
        selectedBackground = index;
        Save();
        loadBackground();
    }
    public void Previous()
    {
        index--;
        if (index < 0)
        {
            index = backgroundDB.BackgroundCount - 1;
        }
        updateBackground(index);
        
    }
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/background.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, selectedBackground);
        stream.Close();
    }
    public int Load()
    {
        string path = Application.persistentDataPath + "/background.fun";
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
    public void loadBackground()
    {

        Destroy(backgroundImageN);
        Background bg = backgroundDB.GetBackground(selectedBackground);
        backgroundImageN = bg.backgroundImage;
        yOffset = bg.yOffset;
        backgroundImageN = Instantiate(backgroundImageN, background);
        backgroundImageN.transform.localPosition = new Vector2(0, yOffset);

    }
    public void loadGround()
    {
        Scene scene = SceneManager.GetActiveScene();
        Background bg = backgroundDB.GetBackground(selectedBackground);
        if(scene.name == "Main Menu")
        {
            ground = bg.menuGround;
            Instantiate(ground);
            return;
        }
        if (scene.name != "Game")
        {
            return;
        }
        ground = bg.ground;
        ground = Instantiate(ground);
    }
    
    
    

    

}
