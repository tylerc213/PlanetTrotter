using UnityEngine;

public enum InventoryItemType
{
    None, Resource1, Resource2, Ice
}

[System.Serializable]
public class InventoryItem
{
    public InventoryItemType itemType;

    public InventoryItem(InventoryItemType type)
    {
        itemType = type;
    }
}