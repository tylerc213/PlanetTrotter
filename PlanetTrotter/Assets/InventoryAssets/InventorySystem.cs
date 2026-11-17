using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    // Number of inventory slots
    public const int SlotCount = 25;

    // Stores the item in each inventory slot
    private InventoryItem[] slots = new InventoryItem[SlotCount];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        InitializeStartingInventory();
    }

    // Populate the inventory with sample resources
    void InitializeStartingInventory()
    {
        slots[0] = new InventoryItem(InventoryItemType.Resource1);
        slots[1] = new InventoryItem(InventoryItemType.Resource1);
        slots[2] = new InventoryItem(InventoryItemType.Resource2);
        slots[5] = new InventoryItem(InventoryItemType.Ice);
        slots[6] = new InventoryItem(InventoryItemType.Resource2);
        slots[7] = new InventoryItem(InventoryItemType.Ice);
    }

    // Retrieve the item in a particular slot
    public InventoryItem GetItem(int slotIndex)
    {
        if (!IsValidSlot(slotIndex))
        {
            return null;
        }

        return slots[slotIndex];
    }

    // Move or swap items between two slots
    public void MoveItem(int fromIndex, int toIndex)
    {
        if (!IsValidSlot(fromIndex) || !IsValidSlot(toIndex))
        {
            return;
        }

        if (fromIndex == toIndex)
        {
            return;
        }

        InventoryItem temporaryItem = slots[fromIndex];

        slots[fromIndex] = slots[toIndex];
        slots[toIndex] = temporaryItem;
    }

    // Check whether a slot is valid
    private bool IsValidSlot(int slotIndex)
    {
        return slotIndex >= 0 && slotIndex < SlotCount;
    }
}
