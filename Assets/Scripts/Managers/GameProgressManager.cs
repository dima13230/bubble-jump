using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public struct GameProgressManager
{
    public const string SAVE_FILENAME = "progress.dat";

    public static string SavePath
    {
        get
        { return Application.persistentDataPath + "/" + SAVE_FILENAME; }
    }

    public static void SaveValues(int Record)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(SavePath);

        GameProgress progress = new GameProgress();
        progress.Record = Record;

        formatter.Serialize(file, progress);
        file.Close();
    }

    public static GameProgress LoadValues()
    {
        if (File.Exists(SavePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(SavePath, FileMode.Open);

            GameProgress progress = (GameProgress)formatter.Deserialize(file);
            file.Close();

            return progress;
        }
        else
            return new GameProgress();
    }
}

[Serializable]
public struct GameProgress
{
    public int Record;
}