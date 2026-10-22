using UnityEngine;

public class Item
{
    private ItemID itemID;
    private string name;
    private double mass;
    private string itemSprite;

    public static Item makeItem(ItemID itemID)
    {
        switch(itemID)
        {
            case ItemID.ItemA:
                return new Item(itemID, 1.0);
            case ItemID.ItemBaby:
                return new Item(itemID, 2.0);
            case ItemID.ItemC:
                return new Item(itemID, 3.0);
            case ItemID.ItemD:
                return new Item(itemID, 4.0);
            case ItemID.ItemE:
                return new Item(itemID, 5.0);
            default:
                return new Item(itemID, 0.0);

        }
    }

    private Item(ItemID itemID, string name, double mass)
    {
        this.itemID = itemID;
        this.name = name;
        this.mass = mass;
        itemSprite = itemID.ToString();
    }

    private Item(ItemID itemID, double mass) : this(itemID, itemID.ToString(), mass) {}

    public ItemID getID()
    {
        return itemID;
    }
}