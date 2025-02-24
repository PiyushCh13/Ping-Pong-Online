using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadSystem
{
    public static readonly string SAVE_FOLDER_NONEDITOR = Application.persistentDataPath + "/Saves/";
    public static readonly string SAVE_FOLDER_EDITOR = Application.dataPath + "/Saves/";

    public static void InitialiseData()
    {
        bool directoryExists;

        #if UNITY_EDITOR
        directoryExists = Directory.Exists(SAVE_FOLDER_EDITOR)
        #endif

        #if !UNITY_EDITOR
        directoryExists = Directory.Exists(SAVE_FOLDER_NONEDITOR)
        #endif

        ;

        if (!directoryExists)
        {
            #if UNITY_EDITOR
            Directory.CreateDirectory(SAVE_FOLDER_EDITOR);
            #endif

            #if !UNITY_EDITOR
            Directory.CreateDirectory(SAVE_FOLDER_NONEDITOR);
            #endif

            SaveData saveData = new SaveData
            {
                IS_ALREADY_REGISTERED = false,
            };

            string json = JsonUtility.ToJson(saveData);
            SaveData(json);
        }
    }



    public static void SaveData(string json)
    {
        #if UNITY_EDITOR
        File.WriteAllText(SAVE_FOLDER_EDITOR + "/Save.json", json);
        #endif

        #if !UNITY_EDITOR
        File.WriteAllText(SAVE_FOLDER_NONEDITOR + "/Save.json", json);
        #endif
    }

    public static string LoadData()
    {
        #if UNITY_EDITOR
        bool checkFile = File.Exists(SAVE_FOLDER_EDITOR + "/Save.json");
        #endif

        #if !UNITY_EDITOR
        bool checkFile = File.Exists(SAVE_FOLDER_NONEDITOR + "/Save.json");
        #endif

        if (checkFile)
        {
            #if UNITY_EDITOR
            string savestring = File.ReadAllText(SAVE_FOLDER_EDITOR + "/Save.json");
            #endif

            #if !UNITY_EDITOR
            string savestring = File.ReadAllText(SAVE_FOLDER_NONEDITOR + "/Save.json");
            #endif
            
            return savestring;
        }
        else
        {
            return null;
        }
    }
}
