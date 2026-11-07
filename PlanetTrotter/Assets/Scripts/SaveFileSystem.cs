
using System;
using System.IO;
using UnityEngine;

public static class SaveFileSystem
{
    private static string GetSavePath(int slot)
    {
        if (slot < 1 || slot > 3)
        {
            throw new ArgumentOutOfRangeException(nameof(slot));
        }

        return Path.Combine(
            Application.persistentDataPath,
            "save_slot_" + slot + ".json"
        );
    }

    public static bool Save(int slot, SaveData data)
    {
        try
        {
            string path = GetSavePath(slot);

            Directory.CreateDirectory(
                Application.persistentDataPath
            );

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);

            Debug.Log("Game saved to: " + path);
            return true;
        }
        catch (Exception error)
        {
            Debug.LogError("Save failed: " + error.Message);
            return false;
        }
    }

    public static SaveData Load(int slot)
    {
        try
        {
            string path = GetSavePath(slot);

            if (!File.Exists(path))
                return null;

            string json = File.ReadAllText(path);

            SaveData data =
                JsonUtility.FromJson<SaveData>(json);

            if (data == null ||
                string.IsNullOrEmpty(data.sceneName))
            {
                return null;
            }

            return data;
        }
        catch (Exception error)
        {
            Debug.LogError("Load failed: " + error.Message);
            return null;
        }
    }
}

