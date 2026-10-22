using UnityEngine;

public class ItemQuantity
{
    Item item;
    int amount;

    public ItemQuantity(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public ItemQuantity(ItemID itemID, int amount)
    {
        item = Item.makeItem(itemID);
        this.amount = amount;
    }

    public Item getItem()
    {
        return item;
    }

    public ItemID getID()
    {
        return item.getID();
    }

    public int getAmount()
    {
        return amount;
    }

    public void setAmount(int amount)
    {
        this.amount = amount;
    }
}