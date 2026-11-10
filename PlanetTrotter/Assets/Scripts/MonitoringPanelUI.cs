using UnityEngine;
using TMPro;

public class MonitoringPanelUI : MonoBehaviour
{

    public WorldEnvironment worldEnvironment;

    public TMP_Text temperatureText;
    public TMP_Text oxygenText;

    void OnEnable()
    {
        if (worldEnvironment == null)
        {
            worldEnvironment = FindAnyObjectByType<WorldEnvironment>();
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (worldEnvironment == null)
        {
            return;
        }

        temperatureText.text = "Temperature: " + worldEnvironment.currentTemperature.ToString("F1");
        oxygenText.text = "Atmospheric Oxygen: " + worldEnvironment.atmosphericOxygen.ToString("F1") + "%";
    }
}
