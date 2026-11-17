using UnityEngine;

public class BlockInteractScript : MonoBehaviour
{
    public Transform player;

    public GameObject promptUI;

    public float interactionDistance = 3f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (player == null || promptUI == null)
        {
            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);

        if(distance <= interactionDistance)
        {
            promptUI.SetActive(true);

            if(mainCamera != null)
            {
                promptUI.transform.rotation = mainCamera.transform.rotation;
            }
        }
        else 
        {
            promptUI.SetActive(false);
        }
    }
}
