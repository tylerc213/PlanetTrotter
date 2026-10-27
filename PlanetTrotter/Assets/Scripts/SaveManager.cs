using UnityEngine;
using System.IO;


public class SaveManager
{
    private string saveFilePath = Path.Combine(Application.persistentDataPath, "Trotterfile.json");

    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        try
        {
            File.WriteAllText(saveFilePath, json);
            Debug.Log($"Game saved to {saveFilePath}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to save game: {e.Message}");
        }
    }

    public SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                Debug.Log($"Game loaded from {saveFilePath}");
                return data;
            }
            catch (IOException e)
            {
                Debug.LogError($"Failed to load game: {e.Message}");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return null;
        }
    }
}
