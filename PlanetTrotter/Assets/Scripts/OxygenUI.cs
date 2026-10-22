using UnityEngine;

public class OxygenUI : MonoBehaviour
{

    public OxygenSystem oxygenSystem;
    public RectTransform oxygenFill;

    private float fullWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (oxygenFill == null)
        {
            Debug.LogError("Oxygen Fill unassigned");
            return;
        }

        fullWidth = oxygenFill.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (oxygenSystem == null || oxygenFill == null)
        {
            return;
        }

        if (oxygenSystem.maxOxygen <= 0f)
        {
            return;
        }

        float percentage = oxygenSystem.currentOxygen / oxygenSystem.maxOxygen;
        percentage = Mathf.Clamp01(percentage);

        oxygenFill.sizeDelta = new Vector2(fullWidth * percentage, oxygenFill.sizeDelta.y);

        Debug.Log("Oxygen: " + oxygenSystem.currentOxygen);
    }
}
