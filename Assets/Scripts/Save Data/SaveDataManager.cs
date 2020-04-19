﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveDataManager {

    private static string path = Path.Combine(Application.persistentDataPath, "saveData.po");

    public static void Save(SaveData saveData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, saveData);
        stream.Close();
    }
	
    public static SaveData Load()
    {
        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            var result = (SaveData)binaryFormatter.Deserialize(stream);
            stream.Close();
            return result;
        }
        else
        {
            return new SaveData();
        }
    }
}