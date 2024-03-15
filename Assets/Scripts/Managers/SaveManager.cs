using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public SavesScriptableObject _saveData;
    public string DefaultSaveLocation = "/Saves/";
    public enum Modes
    {
        Campaign,
        Endless,
        Testmode,
        Multiplayer
    }

    public void SaveGame()
    {
        string playerData = JsonConvert.SerializeObject(_saveData._playerData, Formatting.Indented);

        StreamWriter savedata = new StreamWriter(Application.persistentDataPath + DefaultSaveLocation + "SaveData.json", false);



        savedata.Write(playerData);

        savedata.Close();
    }

    private void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        Debug.Log(DefaultSaveLocation + " " + Application.persistentDataPath);
        string filePath = Path.Combine(Application.persistentDataPath + DefaultSaveLocation);
        // Check if the file exists before creating StreamReader
        if (Directory.Exists(filePath))
        {
            StreamReader playerData = new StreamReader(Application.persistentDataPath + DefaultSaveLocation + "SaveData.json");
            _saveData._playerData = JsonConvert.DeserializeObject<SavesScriptableObject.PlayerData>(playerData.ReadToEnd());

            playerData.Close();
        }
        else
        {
            Debug.Log("SaveData was not foud: Creating new one.");
            Directory.CreateDirectory(filePath);
            File.Create(filePath + "SaveData.json");
            SaveGame();
        }
    }

    

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
