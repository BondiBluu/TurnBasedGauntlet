using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class InvenObject : ScriptableObject
{
    public List<InvenSlot> container = new List<InvenSlot>();

    //adds an item to the inventory
    public void AddItem(ItemObject _item, int _amount)
    {
        //checks if the item is already in the inventory
        bool hasItem = false;

        //if the item is already in the inventory, add the amount to the existing item
        foreach (InvenSlot slot in container)
        {
            if (slot.item == _item)
            {
                slot.AddAmount(_amount);
                hasItem = true;
                break;
            }
        }

        //if the item is not in the inventory, add the item to the inventory
        if (!hasItem)
        {
            container.Add(new InvenSlot(_item, _amount));
        }
    }
}

[System.Serializable]
public class InvenSlot
{
    public ItemObject item;
    public int amount;
    public InvenSlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
