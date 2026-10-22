using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OverworldManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            Debug.Log("Transition to inventory");
            SceneManager.LoadScene(sceneName:"Inventory");
        }
    }
}
