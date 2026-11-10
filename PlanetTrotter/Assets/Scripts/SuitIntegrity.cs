using UnityEngine;

public class SuitIntegrity : MonoBehaviour
{
    [Header("Suit Integrity")]
    public float maxIntegrity = 100f;
    public float currentIntegrity = 100f;
    public float coldThreshold = -30f;
    public float depletionRate = 1f;

    private WorldEnvironment worldEnvironment;
    private OxygenSystem oxygenSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        worldEnvironment = FindAnyObjectByType<WorldEnvironment>();
        oxygenSystem = GetComponent<OxygenSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (worldEnvironment == null ||  oxygenSystem == null) return;

        if (oxygenSystem.isInBase)
        {
            return;
        }

        if (worldEnvironment.currentTemperature < coldThreshold)
        {
            currentIntegrity -= depletionRate * Time.deltaTime;
        }

        currentIntegrity = Mathf.Clamp(currentIntegrity, 0f, maxIntegrity);
    }
}
