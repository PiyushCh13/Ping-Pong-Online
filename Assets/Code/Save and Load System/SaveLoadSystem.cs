using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadSystem
{
    public static readonly string SAVE_FOLDER_NONEDITOR = Application.persistentDataPath + "/Saves/";

    public static void InitialiseData()
    {
        bool directoryExists;
        directoryExists = Directory.Exists(SAVE_FOLDER_NONEDITOR);

        if (!directoryExists)
        {
            Directory.CreateDirectory(SAVE_FOLDER_NONEDITOR);

            SaveData saveData = new SaveData
            {
                HIGH_SCORE = 0,
                SFX_SLIDER = 0.5f,
                MUSIC_SLIDER = 0.5f,
            };

            string json = JsonUtility.ToJson(saveData);
            SaveData(json);
        }
    }



    public static void SaveData(string json)
    {
        File.WriteAllText(SAVE_FOLDER_NONEDITOR + "/Save.json", json);
    }

    public static string LoadData()
    {
        if (File.Exists(SAVE_FOLDER_NONEDITOR + "/Save.json"))
        {
            string savestring = File.ReadAllText(SAVE_FOLDER_NONEDITOR + "/Save.json");
            return savestring;
        }
        else
        {
            return null;
        }
    }
}
