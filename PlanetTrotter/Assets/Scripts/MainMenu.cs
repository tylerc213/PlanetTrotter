using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void NewGame()
    {
        Debug.Log("New Game.");
        SceneManager.LoadScene("TestHub");
    }

    public void LoadGame()
    { 
        Debug.Log("Load Game.");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game.");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
