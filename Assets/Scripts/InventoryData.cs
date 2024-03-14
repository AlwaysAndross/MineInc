using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class InventoryData : MonoBehaviour
{
    public List<InventoryItem> inventory;

    void Start()
    {
        inventory = new List<InventoryItem>();
    }


    public void printInventory()
    {
        string result = " Inventory : ";
        foreach (InventoryItem item in inventory)
        {
            if (item.type == InventoryItemType.ORE)
            {
                result += ((InventoryOreItem)item).oreType + " (" + item.amount + "), ";

            }
            else
            {
                result += item.type + " (" + item.amount + "), ";
            }
        }
        Debug.Log(result);
    }

    public bool CanAddOre()
    {
        int capacity = 5;

        foreach (InventoryItem item in inventory)
        {
            if (item.type == InventoryItemType.ORE)
            {
                capacity -= item.amount;
            }
        }
        return capacity > 0;
    }

    public void AddOre(OreType oreType)
    {
        InventoryOreItem foundExistingOreItem = null;
        foreach (InventoryItem item in inventory)
        {
            if (item.type == InventoryItemType.ORE)
            {
                // If that ore item in inventory is of our type
                if (((InventoryOreItem)item).oreType == oreType)
                {
                    foundExistingOreItem = ((InventoryOreItem)item);
                }
            }
        }
        if (foundExistingOreItem != null)
        {
            foundExistingOreItem.amount += 1;
        }
        else
        {
            InventoryOreItem oreItem = new InventoryOreItem(1, oreType);
            inventory.Add(oreItem);
        }
    }

}

public class InventoryItem
{
    public int amount;
    public InventoryItemType type;

    public InventoryItem(int amount, InventoryItemType type)
    {
        this.amount = amount;
        this.type = type;
    }
}

public class InventoryOreItem : InventoryItem
{
    public OreType oreType;
    public InventoryOreItem(int amount, OreType oreType) : base(amount, InventoryItemType.ORE)
    {
        this.oreType = oreType;
    }

}

public enum InventoryItemType
{
    ORE,
    MONEY,
}