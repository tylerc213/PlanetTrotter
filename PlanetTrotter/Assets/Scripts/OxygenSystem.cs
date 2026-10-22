using UnityEngine;

public class OxygenSystem : MonoBehaviour
{

    public float maxOxygen = 100f;
    public float currentOxygen = 100f;

    public float depletionRate = 5f;
    public float minimumDepletionRate = 1f;
    public float refillRate = 20f;

    public bool isInBase = true;

    public Vector3 baseRespawnPosition = new Vector3(15f, 1f, 0f);

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInBase)
        {
            currentOxygen += refillRate * Time.deltaTime;
        }
        else
        {
            float percentage = currentOxygen / maxOxygen;

            float currentDepletionRate = Mathf.Lerp(minimumDepletionRate, depletionRate, percentage);
            
            currentOxygen -= currentDepletionRate * Time.deltaTime;
        }

        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);

        if (currentOxygen <= 0f)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        Debug.Log("Oxygen Depleted, Respawning at Base");

        transform.position = baseRespawnPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        currentOxygen = maxOxygen;
        isInBase = true;
    }
}
