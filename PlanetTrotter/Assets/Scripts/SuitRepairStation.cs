using UnityEngine;

public class SuitRepairStation : MonoBehaviour, Interactable
{

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

        suitIntegrity.currentIntegrity = suitIntegrity.maxIntegrity;
    }
}
