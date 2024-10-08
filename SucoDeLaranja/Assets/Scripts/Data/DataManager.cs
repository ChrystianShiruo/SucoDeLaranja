using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager {

    public static readonly string Path = $"{Application.persistentDataPath}/gameData.json";
    public static Action OnSaveGameData;

    public static bool SaveExists() {
        return File.Exists(Path);
    }

    public static void SaveGameDataJson(GameData gameData) {
        string data = JsonUtility.ToJson(gameData, true);

        System.IO.File.WriteAllText(Path, data);
        OnSaveGameData?.Invoke();
    }

    public static GameData LoadGameDataJson() {

        if(!File.Exists(Path)) {
            return null;
        }
        string data = File.ReadAllText(Path);
        if(string.IsNullOrEmpty(data)) {
            return null;
        }
        GameData loadedGameData = JsonUtility.FromJson<GameData>(data);
        return loadedGameData;
    }
}
