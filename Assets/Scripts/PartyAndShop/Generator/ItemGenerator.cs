using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour{
    [SerializeField] Transform itemContainer;
    [SerializeField] GameObject itemPrefab;

    //for item hover
    public TMP_Text itemName;
    public TMP_Text itemType;
    public TMP_Text itemAttackOrRecovery;    
    public TMP_Text itemDescription;

    public InvenObject inventory;    

    public void GeneratePotions(){
        //if the items are potions
        GenerateItems(ItemObject.ItemType.Restorative);  
    }

    public void GenerateWeapons(){
        GenerateItems(ItemObject.ItemType.Weapon);
    }

    public void GenerateEquips(){
        GenerateItems(ItemObject.ItemType.Equipment);
    }

    public void GenerateBoosts(){
        GenerateItems(ItemObject.ItemType.PermStatBoost);
    }

    public void GenerateTools(){
        GenerateItems(ItemObject.ItemType.Tool);
    }

    public void GenerateTreasures(){}

    public void GenerateSpecial(){}


    public void GenerateItems(ItemObject.ItemType type){
        //empty the item container
        foreach (Transform child in itemContainer.transform){
            Destroy(child.gameObject);
        }

        //grab all from inventory
        foreach (InvenSlot slot in inventory.container){
            if (slot.item.Type == type){
            //create a new item prefab
            GameObject item = Instantiate(itemPrefab, itemContainer);
            TMP_Text itemName = item.GetComponentInChildren<TMP_Text>();
            itemName.text = $"{slot.item.ItemName} x {slot.amount}";
            }
            
        }
    }
}