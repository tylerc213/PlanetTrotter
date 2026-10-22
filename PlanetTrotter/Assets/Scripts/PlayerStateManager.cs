using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    public static PlayerStateManager Instance;

    public Vector3 savedPosition;
    public float savedOxygen;
    public bool savedIsInBase;

    public bool hasSavedState = false;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerState()
    {
        OxygenSystem oxygen = FindAnyObjectByType<OxygenSystem>();

        if (oxygen == null)
        {
            Debug.Log("OxygenSystem not found");
            return;
        }

        savedPosition = oxygen.transform.position;
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

        OxygenSystem oxygen = FindAnyObjectByType<OxygenSystem>();

        if (oxygen == null)
        {
            Debug.Log("OxygenSystem not found");
            return;
        }

        oxygen.transform.position = savedPosition;
        oxygen.currentOxygen = savedOxygen;
        oxygen.isInBase = savedIsInBase;

        Rigidbody rb = oxygen.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("Restored");
    }
}
