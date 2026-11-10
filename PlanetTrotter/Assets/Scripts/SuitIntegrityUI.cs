using UnityEngine;

public class SuitIntegrityUI : MonoBehaviour
{

    public RectTransform integrityFill;

    private SuitIntegrity suitIntegrity;
    private float fullWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        suitIntegrity = FindAnyObjectByType<SuitIntegrity>();
        fullWidth = integrityFill.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (suitIntegrity == null)
        {
            return;
        }

        float percentage = suitIntegrity.currentIntegrity / suitIntegrity.maxIntegrity;

        integrityFill.sizeDelta = new Vector2(fullWidth * percentage, integrityFill.sizeDelta.y);
    }
}
