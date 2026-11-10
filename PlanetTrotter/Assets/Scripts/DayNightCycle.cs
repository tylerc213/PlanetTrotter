using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    [Header("Time Settings")]
    public float cycleDurationMinutes = 24f;
    public float timeOfDay = 12f;
    public bool isDayTime;

    private WorldEnvironment worldEnvironment;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        worldEnvironment = FindAnyObjectByType<WorldEnvironment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (worldEnvironment == null)
        {
            return;
        }

        AdvanceTime();
        UpdateEnvironment();
    }

    void AdvanceTime()
    {
        float hoursPerSecond = 24f / (cycleDurationMinutes * 60f);
        timeOfDay += hoursPerSecond * Time.deltaTime;

        if (timeOfDay >= 24f)
        {
            timeOfDay -= 24f;
        }

        isDayTime = timeOfDay >= 6f && timeOfDay < 18f;
    }

    void UpdateEnvironment()
    {
        float daylight = 0.5f + 0.5f * Mathf.Cos((timeOfDay - 12f) * Mathf.PI / 12f);

        worldEnvironment.currentTemperature = Mathf.Lerp(worldEnvironment.nighttimeTemperature, worldEnvironment.daytimeTemperature, daylight);
    }
}
