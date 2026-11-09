
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject saveSlotsPanel;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        saveSlotsPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (saveSlotsPanel.activeSelf)
            {
                BackToPause();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        pausePanel.SetActive(true);
        saveSlotsPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        pausePanel.SetActive(false);
        saveSlotsPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ShowSaveSlots()
    {
        pausePanel.SetActive(false);
        saveSlotsPanel.SetActive(true);
    }

    public void BackToPause()
    {
        saveSlotsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartUpMenu");
    }

    public void QuitApplication()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}

