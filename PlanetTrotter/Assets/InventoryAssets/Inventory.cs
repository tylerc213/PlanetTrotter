using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<ItemQuantity> items;

    private double maxWeight;
    private double currWeight;
    private int maxSlots;
    //private int currSlots; this i sjust the size of items

    public Inventory(double maxWeight, int maxSlots)
    {
        items = new List<ItemQuantity>();

        this.maxWeight = maxWeight;
        currWeight = 0;
        this.maxSlots = maxSlots;
    }

    private int findItem(ItemID itemID)
    {
        int index = 0;
        foreach(ItemQuantity item in items)
        {
            if(item.getID() >= itemID)
            {
                return (item.getID() == itemID) ? index : -1 * index;
            }
            index++;
        }
        return -1 * index;
    }

    public bool hasItem(ItemID itemID, int quantity)
    {
        int index = findItem(itemID);
        if(index >= 0)
        {
            return items[index].getAmount() >= quantity;
        }
        return false;
    }

    public bool hasItem(ItemID itemID)
    {
        return hasItem(itemID, 1);
    }

    public void addItem(ItemQuantity newItem)
    {
        int index = findItem(newItem.getID());
        if(index >= 0)
        {
            ItemQuantity listItem = items[index];
            listItem.setAmount(listItem.getAmount() + newItem.getAmount());
        } else
        {
            items.Insert(-1 * index, newItem);
        }
    }

    // if removing the item quantity will cause it to go negative, the item will just be removed to aviod errors
    public void removeItem(ItemQuantity negItem)
    {
        int index = findItem(negItem.getID());
        if(index >= 0)
        {
            ItemQuantity listItem = items[index];
            if(listItem.getAmount() < negItem.getAmount())
            {
                items.RemoveAt(index);
            } else
            {
                listItem.setAmount(listItem.getAmount() - negItem.getAmount());
            }
        }
    }
}
