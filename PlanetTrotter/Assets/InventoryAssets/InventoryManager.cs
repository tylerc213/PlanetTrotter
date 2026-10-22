using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("The escape key was pressed.");
            SceneManager.LoadScene(sceneName:"Overworld");
        }
    }
}
