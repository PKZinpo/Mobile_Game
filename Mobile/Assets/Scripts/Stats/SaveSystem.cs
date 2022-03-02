using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SaveGameData(GameSaveData gameData) {
        string path = Application.persistentDataPath + "/game.data";
        using (FileStream stream = new FileStream(path, FileMode.Create)) {
            BinaryFormatter formatter = new BinaryFormatter();
            SaveData data = new SaveData(gameData);
            formatter.Serialize(stream, data);
        }
    }

    public static SaveData LoadGameData() {
        string path = Application.persistentDataPath + "/game.data";
        if (File.Exists(path)) {
            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                SaveData data = formatter.Deserialize(stream) as SaveData;
                return data;
            }
        }
        else {
            Debug.LogError("[SaveSystem] There is no file found in path " + path);
            return null;
        }

    }
}
