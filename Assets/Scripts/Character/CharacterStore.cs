using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class CharacterStore : MonoBehaviour
{
    public static int cost;
    public int coinNum;
    public bool[] unlockedCharacter = new bool[4]{true,false,false,false};
    public GameObject buyPanel;
    public GameObject CharacterPanel;
    public GameObject select;
    public GameObject locked;
    public GameObject Coin;
    public TextMeshProUGUI buyText;
    void Awake()
    {
        Load();
    }

    void Start()
    {
        unlocked();
        coinNum = SaveSystem.LoadData().coins;
    
    }

    public void unlocked()
    {
        if(unlockedCharacter[CharacterManager.index])
        {
            locked.SetActive(false);
            select.SetActive(true);
            return;
        }
        locked.SetActive(true);
        select.SetActive(false);
    }
    public void unlock()
    {
        buyPanel.SetActive(true);
        CharacterPanel.SetActive(false);
        buyText.text =cost.ToString();
    }

    public void deny()
    {
        buyPanel.SetActive(false);
        CharacterPanel.SetActive(true);
    }
    public void confirm()
    {
        if(coinNum<cost)
        {   
            //sound of faliure
            return;
        }
        coinNum = coinNum-cost;
        unlockedCharacter[CharacterManager.index] = true;
        
        Coin.GetComponent<CoinManager>().removeCoin(cost);
        SaveSystem.SaveData(Coin.GetComponent<CoinManager>());
        Save();
        unlocked();
        deny();
        
    }



    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/unlockedChar.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, unlockedCharacter);
        stream.Close();
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/unlockedChar.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            unlockedCharacter = (bool[])formatter.Deserialize(stream);
            if(unlockedCharacter ==null){
                unlockedCharacter = new bool[4]{true,false,false,false};
            }
            stream.Close();
            

        }
        else
        {
            Debug.Log("save file not found!!!");
            
        }
    }

    
}
