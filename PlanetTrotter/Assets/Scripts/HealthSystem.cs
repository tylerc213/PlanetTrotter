using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float damageRate = 10f;

    private OxygenSystem oxygenSystem;
    private SuitIntegrity suitIntegrity;
    private Rigidbody rb;

    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oxygenSystem = GetComponent<OxygenSystem>();
        suitIntegrity = GetComponent<SuitIntegrity>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || oxygenSystem == null || suitIntegrity == null)
        {
            return;
        }

        if (oxygenSystem.isInBase)
        {
            return;
        }

        if (oxygenSystem.currentOxygen <= 0f || suitIntegrity.currentIntegrity <= 0f)
        {
            currentHealth -= damageRate * Time.deltaTime;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        transform.position = oxygenSystem.baseRespawnPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        currentHealth = maxHealth;
        oxygenSystem.currentOxygen = oxygenSystem.maxOxygen;
        suitIntegrity.currentIntegrity = suitIntegrity.maxIntegrity;

        oxygenSystem.isInBase = true;

        isDead = false;
    }
}
