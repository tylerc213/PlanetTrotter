using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class SaveController : MonoBehaviour
{
    public Transform player;

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.sceneName = SceneManager.GetActiveScene().name;
        data.PlayerX = player.position.x;
        data.PlayerY = player.position.y;
        data.PlayerZ = player.position.z;
        SaveManager saveManager = new SaveManager();
        saveManager.SaveGame(data);
    }

    public void LoadGame()
    {
        SaveManager saveManager = new SaveManager();
        SaveData data = saveManager.LoadGame();
        if (data != null)
        {
            SceneManager.LoadScene(data.sceneName);
            player.position = new Vector3(data.PlayerX, data.PlayerY, data.PlayerZ);
        }
        else
        {
            Debug.LogError("No save data found!");
        }

        if (data.sceneName != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(data.sceneName);
        }

        //player.position = new Vector3(data.PlayerX, data.PlayerY, data.PlayerZ);
        Vector3 savedPosition = new Vector3(
    data.PlayerX,
    data.PlayerY,
    data.PlayerZ);

        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.position = savedPosition;

            if (!rb.isKinematic)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            player.position = savedPosition;
        }


        Debug.Log("Player position restored");
    }


    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.f5Key.wasPressedThisFrame)
        {
            SaveGame();
        }

        if (Keyboard.current.f9Key.wasPressedThisFrame)
        {
            LoadGame();
        }
    }
}

