using UnityEngine;

public class OxygenRefillZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        OxygenSystem oxygen = other.GetComponent<OxygenSystem>();

        if (oxygen != null)
        {
            oxygen.isInBase = true;
            Debug.Log("Entered base");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OxygenSystem oxygen = other.GetComponent<OxygenSystem>();

        if (oxygen != null)
        {
            oxygen.isInBase = false;
            Debug.Log("Left base");
        }
    }
}
