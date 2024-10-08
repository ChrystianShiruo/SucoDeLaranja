using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataManager {


    public static void SaveGameDataJson(GameData gameData) {
        string data = JsonUtility.ToJson(gameData, true);

        System.IO.File.WriteAllText($"{Application.persistentDataPath}/gameData.json", data);
    }

    public static GameData LoadGameDataJson() {
        string path = $"{Application.persistentDataPath}/gameData.json";
        if(!File.Exists(path)) {
            return null;
        }
        string data = File.ReadAllText(path);
        if(string.IsNullOrEmpty(data)) {
            return null;
        }
        GameData loadedGameData = JsonUtility.FromJson<GameData>(data);
        return loadedGameData;
    }
}
