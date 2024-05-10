using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : ScriptableObject
{
    public enum ItemType
    {
        Restorative,
        PermStatBoost,
        Equipment,
        Weapon,
        Tool,
        Default,
    }

    [SerializeField] string itemName;

    [SerializeField] string itemPlural;

    [SerializeField] ItemType type;

    [TextArea(15, 20)]
    [SerializeField] string itemDescription;
    [SerializeField] int itemPrice;
    [SerializeField] bool ableToSell;

    public string ItemName{ get { return itemName; } }
    public string ItemPlural{ get { return itemPlural; } }
    
    public ItemType Type{ get { return type; } set { type = value; } }
    public string ItemDescription{ get { return itemDescription; } }
    public int ItemPrice{ get { return itemPrice; } }
    public bool AbleToSell{ get { return ableToSell; } }

}
