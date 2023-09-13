using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{


    public static void SaveFish(CollisiononWater obj)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saved.fish";
        Stream stream = new FileStream(path, FileMode.Create);

        FishDat data = new FishDat(obj);

        binaryFormatter.Serialize(stream, data);
        stream.Close();

    }

    public static FishDat LoadFish()
    {
        
        string path = Application.persistentDataPath + "/saved.fish";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();


            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

            FishDat data = binaryFormatter.Deserialize(stream) as FishDat;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save File Not Found in " + path);
            return null;
        }

        

    }



}
