using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : ScriptableObject
{
    public enum ItemType
    {
        Restorative,
        PermStatBoost,
        Armor,
        Weapon,
        KeyItem,
        Tool,
        Default,
        Joke
    }

    [SerializeField] string itemName;

    [SerializeField] string itemPlural;

    [TextArea(15, 20)]
    [SerializeField] string itemDescription;
    [SerializeField] int itemPrice;
    [SerializeField] bool ableToSell;

}
