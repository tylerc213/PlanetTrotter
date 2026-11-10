using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   

    public void NewGame()
    {
        Time.timeScale = 1f;
        Debug.Log("New Game.");

        PlayerStateManager.Instance.PrepareForNewGame();

        SceneManager.LoadScene("Overworld");
    }

    public void LoadGame()
    { 
        Debug.Log("Load Game.");
        SceneManager.LoadScene("SaveFileScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game.");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void BackToMainMenu()
    {
        ResourceSpawner resourceSpawner = FindAnyObjectByType<ResourceSpawner>();

        if (resourceSpawner != null)
        {
            resourceSpawner.ClearGeneratedResources();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("StartUpMenu");
    }
}
