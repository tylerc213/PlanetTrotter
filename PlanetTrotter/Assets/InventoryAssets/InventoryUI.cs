using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public InventorySlotUI[] inventorySlots;

    private InventorySystem inventorySystem;

    private int selectedSlot = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventorySystem = GetComponent<InventorySystem>();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].slotIndex = i;
            inventorySlots[i].Initialize(inventorySystem, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSlot(int slotIndex)
    {
        // No slot is currently selected
        if (selectedSlot == -1)
        {
            // Only select occupied slots
            if (inventorySystem.GetItem(slotIndex) != null)
            {
                selectedSlot = slotIndex;
                Debug.Log("Selected inventory slot: " + slotIndex);
            }

            return;
        }

        // Clicking the selected slot again deselects it
        if (selectedSlot == slotIndex)
        {
            selectedSlot = -1;
            Debug.Log("Inventory selection cancelled.");
            return;
        }

        // Move or swap the items
        inventorySystem.MoveItem(selectedSlot, slotIndex);

        Debug.Log("Moved item from slot " + selectedSlot + " to slot " + slotIndex);

        // Clear selection
        selectedSlot = -1;

        // Update all slot displays
        RefreshAllSlots();
    }

    private void RefreshAllSlots()
    {
        foreach (InventorySlotUI slot in inventorySlots)
        {
            slot.RefreshDisplay();
        }
    }
}
