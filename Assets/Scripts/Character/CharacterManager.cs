using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public GameObject player;
    public static int index = 0;
    public int selectedCharacter;
    public GameObject parentOfCharacter;

    void Start()
    {
       
        selectedCharacter = Load();
        index = selectedCharacter;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Game")
        {
            loadPlayer();
        }
        else
        {
            UpdateCharacter(index);
        }
        
    }
    public void loadPlayer()
    {
        Destroy(player);
        Character character = characterDB.GetCharacter(index);
        player = character.playableCharacter;
        player = Instantiate(player, transform.position, Quaternion.identity);
    }
    public void NextOption()
    { 
        index++;
        if(index >= characterDB.CharacterCount)
        {
            index = 0;
        }
        UpdateCharacter(index);
    }
    public void BackOption()
    {
        index--;
        if (index <0)
        {
            index = characterDB.CharacterCount-1;
        }
        UpdateCharacter(index);
    }
    public void UpdateCharacter(int index)
    {
        parentOfCharacter = GameObject.Find("SelectedCharacter");

        Destroy(player);
        Character character = characterDB.GetCharacter(index);
        player = character.character;
        player = Instantiate(player, parentOfCharacter.transform);
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Store" ) 
        {
            player.GetComponent<Rigidbody2D>().isKinematic = true;
        }
                CharacterStore.cost = character.cost;


    }
    public void SelectCharacter()
    {
        selectedCharacter = index;
        Save();
    }
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/character.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, selectedCharacter);
        stream.Close();
    }
    public int Load()
    {
        string path = Application.persistentDataPath + "/character.fun";
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

   
