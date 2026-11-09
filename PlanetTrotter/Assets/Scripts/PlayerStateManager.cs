using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerStateManager : MonoBehaviour
{

    public static PlayerStateManager Instance;

    public Vector3 savedPosition;
    public Quaternion savedRotation;
    public float savedOxygen;
    public bool savedIsInBase;

    public bool hasSavedState = false;

    private SaveData pendingLoad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if(Instance == this)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    
    private void OnDisable()
    {
        if(Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerState()
    {

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();

        OxygenSystem oxygen = FindAnyObjectByType<OxygenSystem>();

        if (player == null || oxygen == null)
        {
            Debug.Log("Player or OxygenSystem not found");
            hasSavedState = false;
            return;
        }

        savedPosition = player.transform.position;
        savedRotation = player.transform.rotation;
        savedOxygen = oxygen.currentOxygen;
        savedIsInBase = oxygen.isInBase;

        hasSavedState = true;

        Debug.Log("Saved");
    }

    public void RestorePlayerState()
    {
        if (!hasSavedState)
        {
            return;
        }

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();

        OxygenSystem oxygen = FindAnyObjectByType<OxygenSystem>();

        if (player == null || oxygen == null)
        {
            Debug.Log("Cannot restore.");
            return;
        }

        //oxygen.transform.position = savedPosition;
        //oxygen.currentOxygen = savedOxygen;
        //oxygen.isInBase = savedIsInBase;

        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.position = savedPosition;
            rb.rotation = savedRotation;

            if (!rb.isKinematic)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            player.transform.SetPositionAndRotation(savedPosition, savedRotation);
        }

        oxygen.currentOxygen = savedOxygen;
        oxygen.isInBase = savedIsInBase;
        Debug.Log("Restored");
        //Debug.LogWarning("PlayerStateManager restored player to: " + savedPosition);
    }

    public bool SaveSlot(int slot)
    {
        SavePlayerState();

        if(!hasSavedState)
        {
            return false;
        }

        SaveData data = new SaveData();

        data.sceneName = SceneManager.GetActiveScene().name;
        data.savedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        data.playerPosition = savedPosition;
        data.playerRotation = savedRotation;
        data.oxygen = savedOxygen;
        data.isInBase = savedIsInBase;

        return SaveFileSystem.Save(slot, data);
    }

    public bool LoadSlot(int slot)
    {
        SaveData data = SaveFileSystem.Load(slot);

        if(data == null)
        {
            Debug.Log("Save slot empty or invalid.");
            return false;
        }

        if(!Application.CanStreamedLevelBeLoaded(data.sceneName))
        {
            Debug.Log("Scene " + data.sceneName + " cannot be loaded.");
            return false;
        }

        pendingLoad = data;

        Time.timeScale = 0f;
        SceneManager.LoadScene(data.sceneName);

        return true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pendingLoad != null && scene.name == pendingLoad.sceneName)
        {
            StartCoroutine(RestoreAfterSceneStart());
        }
    }

    private IEnumerator RestoreAfterSceneStart()
    {
        yield return null;
        savedPosition = pendingLoad.playerPosition;
        savedRotation = pendingLoad.playerRotation;
        savedOxygen = pendingLoad.oxygen;
        savedIsInBase = pendingLoad.isInBase;

        hasSavedState = true;

        RestorePlayerState();

        pendingLoad = null;
        Time.timeScale = 1f;
    }
}
