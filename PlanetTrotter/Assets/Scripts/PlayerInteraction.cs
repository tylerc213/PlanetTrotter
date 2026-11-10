using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    public float interactionDistance = 3f;

    private Interactable currentInteractable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindInteractable();

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {

            Debug.Log("Space Key Pressed");

            if (currentInteractable != null)
            {
                Debug.Log("Interactable Found!");

                currentInteractable.Interact();
            }
            else
            {
                Debug.Log("No Interactable Found");
            }
        }
    }

    void FindInteractable()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, interactionDistance);
        currentInteractable = null;
        float closestDistance = interactionDistance;

        foreach (Collider nearbyObject in nearbyObjects)
        {
            Interactable interactable = nearbyObject.GetComponentInParent<Interactable>();

            if (interactable == null)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentInteractable = interactable;
            }

        }
    }
}
