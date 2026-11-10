using UnityEngine;

public class ResourceOne : MonoBehaviour, Interactable
{

    [Header("Mining Settings")]
    public float integrityCost = 5f;

    private SuitIntegrity suitIntegrity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        suitIntegrity = FindAnyObjectByType<SuitIntegrity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (suitIntegrity == null)
        {
            return;
        }

        suitIntegrity.currentIntegrity -= integrityCost;
        suitIntegrity.currentIntegrity = Mathf.Clamp(suitIntegrity.currentIntegrity, 0f, suitIntegrity.maxIntegrity);

        Destroy(gameObject);
    }
}
