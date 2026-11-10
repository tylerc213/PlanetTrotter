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

    public float savedHealth;
    public float savedSuitIntegrity;
    public float savedTimeOfDay;

    public bool hasSavedState = false;

    public bool restoreAfterSceneChange = false;
    private bool pendingNewGame = false;

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

        HealthSystem health = FindAnyObjectByType<HealthSystem>();
        SuitIntegrity integrity = FindAnyObjectByType<SuitIntegrity>();
        DayNightCycle dayNight = FindAnyObjectByType<DayNightCycle>();

        if (health != null)
        {
            savedHealth = health.currentHealth;
        }

        if (integrity != null)
        {
            savedSuitIntegrity = integrity.currentIntegrity;
        }

        if (dayNight != null)
        {
            savedTimeOfDay = dayNight.timeOfDay;
        }

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

        HealthSystem health = FindAnyObjectByType<HealthSystem>();
        SuitIntegrity integrity = FindAnyObjectByType<SuitIntegrity>();
        DayNightCycle dayNight = FindAnyObjectByType<DayNightCycle>();

        if (health != null)
        {
            health.currentHealth = savedHealth;
        }

        if (integrity != null)
        {
            integrity.currentIntegrity = savedSuitIntegrity;
        }

        if (dayNight != null)
        {
            dayNight.timeOfDay = savedTimeOfDay;
        }

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

        data.health = savedHealth;
        data.suitIntegrity = savedSuitIntegrity;
        data.timeOfDay = savedTimeOfDay;

        ResourceSpawner resourceSpawner = FindAnyObjectByType<ResourceSpawner>();

        if (resourceSpawner != null)
        {
            data.resources = resourceSpawner.GetRemainingResources();
        }
        else
        {
            return false;
        }

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
        restoreAfterSceneChange = false;

        Time.timeScale = 0f;
        SceneManager.LoadScene(data.sceneName);

        return true;
    }

    public void PrepareForOverworldReturn()
    {
        restoreAfterSceneChange = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pendingLoad != null && scene.name == pendingLoad.sceneName)
        {
            StartCoroutine(RestoreAfterSceneStart());
        }

        else if (pendingNewGame && scene.name == "Overworld")
        {
            ResourceSpawner resourceSpawner = FindAnyObjectByType<ResourceSpawner>();

            if (resourceSpawner != null)
            {
                resourceSpawner.GenerateNewResources();
            }

            pendingNewGame = false;

        }

        else if (restoreAfterSceneChange && scene.name == "Overworld")
        {
            StartCoroutine(RestoreTemporaryPlayerState());
        }
    }

    public void PrepareForNewGame()
    {
        pendingNewGame = true;
        pendingLoad = null;
        restoreAfterSceneChange = false;
        hasSavedState = false;
    }

    private IEnumerator RestoreAfterSceneStart()
    {
        yield return null;

        savedPosition = pendingLoad.playerPosition;
        savedRotation = pendingLoad.playerRotation;
        savedOxygen = pendingLoad.oxygen;
        savedIsInBase = pendingLoad.isInBase;

        savedHealth = pendingLoad.health;
        savedSuitIntegrity = pendingLoad.suitIntegrity;
        savedTimeOfDay = pendingLoad.timeOfDay;

        hasSavedState = true;

        RestorePlayerState();

        ResourceSpawner resourceSpawner = FindAnyObjectByType<ResourceSpawner>();

        if (resourceSpawner != null)
        {
            resourceSpawner.RestoreResources(pendingLoad.resources);
        }

        pendingLoad = null;
        Time.timeScale = 1f;
    }

    private IEnumerator RestoreTemporaryPlayerState()
    {
        yield return null;

        RestorePlayerState();
        restoreAfterSceneChange = false;
    }
}
