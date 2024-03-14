using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class BackgroundStore : MonoBehaviour
{
    private bool[] unlockedBackground = new bool[3]{true, false,false};
    
    public static int cost;
    public int coinNum;
    
    public GameObject locked;
    public GameObject select;
    public GameObject buyPanel;
    public GameObject backgroundPanel;
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
        
        if(unlockedBackground[BackgroundManager.index])
        {
            locked.SetActive(false);
            select.SetActive(true);
            return;
        }
        locked.SetActive(true);
        select.SetActive(false);
    }


    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/unlocked.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, unlockedBackground);
        stream.Close();
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/unlocked.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            unlockedBackground = (bool[])formatter.Deserialize(stream);
            if(unlockedBackground ==null){
                unlockedBackground = new bool[3]{true,false,false};
            }
            stream.Close();
            

        }
        else
        {
            Debug.Log("save file not found!!!");
            
        }
    }
    public void unlock()
    {
        buyPanel.SetActive(true);
        backgroundPanel.SetActive(false);
        buyText.text =cost.ToString();
    }
    public void deny()
    {
         buyPanel.SetActive(false);
        backgroundPanel.SetActive(true);
    }
    public void confirm()
    {
        if(coinNum<cost)
        {   Debug.Log("falied");
            //sound of faliure
            return;
        }
        coinNum = coinNum-cost;
        unlockedBackground[BackgroundManager.index] = true;
        Debug.Log(unlockedBackground[BackgroundManager.index]);
        Coin.GetComponent<CoinManager>().removeCoin(cost);
        SaveSystem.SaveData(Coin.GetComponent<CoinManager>());
        Save();
        unlocked();
        deny();
        
    }

     
        
    

}
