
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(CoinManager coinManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data23.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(coinManager);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data LoadData()
    {
        string path = Application.persistentDataPath + "/data23.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            return data;

        }
        else
        { 
            Debug.Log("save file not found!!!");
            return null;
        }
    }
    
}
