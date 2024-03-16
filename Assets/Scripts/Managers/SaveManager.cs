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
        Application.targetFrameRate = 30;

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
            SavesScriptableObject.OwnedSegments l;
            l._segmentType = GameManager.SegmentType.Cannon;
            l._segmentLevel = 1;
            _saveData._playerData._ownedSegments.Add(l);
            SaveGame();
        }
    }

    public bool DeductApples(int amount)
    {
        if (_saveData._playerData._apples >= amount)
        {
            _saveData._playerData._apples -= amount;
            return true;
        }
        return false;
    }
    public int GetApples()
    {
        return _saveData._playerData._apples;
    }

    public void AddApples(int amount)
    {
        _saveData._playerData._apples += amount;
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
