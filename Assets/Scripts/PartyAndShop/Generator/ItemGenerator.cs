using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour{
    [SerializeField] Transform itemContainer;
    [SerializeField] GameObject itemPrefab;

    public InvenObject inventory;    

    public void GeneratePotions(){
        //empty the item container
        foreach (Transform child in itemContainer.transform){
            Destroy(child.gameObject);
        }

        //grab all from inventory
        foreach (InvenSlot slot in inventory.container){
            //create a new item prefab
            GameObject item = Instantiate(itemPrefab, itemContainer);
            //name the item
            
        }
        
    }

    public void GenerateWeapons(){}

    public void GenerateEquips(){}

    public void GenerateTools(){}

    public void GenerateTreasures(){}

    public void GenerateSpecial(){}
}