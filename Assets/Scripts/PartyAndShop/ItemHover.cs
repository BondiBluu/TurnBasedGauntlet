using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class ItemHover : MonoBehaviour//, IPointerEnterHandler
{
    ItemGenerator generator;

    public ItemObject Item {get; set;}

    void Awake()
    {
        generator = FindObjectOfType<ItemGenerator>();
    }
    
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     DisplayItemData(Item);
    // }

    // public void OnSelect(BaseEventData eventData)
    // {
    //     DisplayItemData(Item);
    // }

    public void DisplayItemData(ItemObject item){

        generator.itemName.text = item.ItemName;
        generator.itemType.text = item.Type.ToString();
        generator.itemDescription.text = item.ItemDescription;

        //if the item type is tool or weapon, display attack power
        switch(item.Type){
            case ItemObject.ItemType.Tool:
                ToolObject tool = (ToolObject)item;
                generator.itemAttackOrRecovery.text = $"Attack Power:  + {tool.AtkPwr}, Magic Power: {tool.MagPwr}";
                break;
            case ItemObject.ItemType.Weapon:
                WeaponObject weapon = (WeaponObject)item;
                generator.itemAttackOrRecovery.text = $"Strength: {weapon.AtkBoost}, Magic: {weapon.MagBoost}";
                break;
            case ItemObject.ItemType.Restorative:
                RestorativeObject restore = (RestorativeObject)item;
                generator.itemAttackOrRecovery.text = $"HP: {restore.HpRestore}, MP: {restore.MpRestore}";
                break;
            default:
                generator.itemAttackOrRecovery.text = "-";
                break;
        }
    }

    
}