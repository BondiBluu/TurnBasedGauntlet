using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGenerator : MonoBehaviour
{
    public GameObject partyCharacterPrefab;
    public GameObject invenCharacterPrefab;
    public Transform partyContainer;
    public Transform invenContainer;
    public float buttonSpacing;

    PartyManager partyManager;
    ShopButtonController shopButtonController;
    PartyStatsManager partyStatsManager;

    public void Start()
    {
        partyManager = FindObjectOfType<PartyManager>();
        shopButtonController = FindObjectOfType<ShopButtonController>();
        partyStatsManager = FindObjectOfType<PartyStatsManager>();
    }
    public void GenerateParty() //used in unity editor as button
    {
        //clear the party container before generating new party members
        foreach (Transform partyCharacter in partyContainer)
        {
            Destroy(partyCharacter.gameObject);
        }

        //clear the inventory container before generating new party members
        foreach (Transform invenCharacter in invenContainer)
        {
            Destroy(invenCharacter.gameObject);
        }

        Debug.Log("Generating Party");
        //generate all 4 party members from the party manager to the party container using the party character prefab
        foreach (CharacterTemplate character in partyManager.currentParty)
        {
            PrefabGenerator(partyCharacterPrefab, partyContainer, character);
        }

        //generate all party members from the party manager to the inventory container using the inventory character prefab
        foreach (CharacterTemplate character in partyManager.partyInventory)
        {
            PrefabGenerator(invenCharacterPrefab, invenContainer, character);
            Debug.Log("Generating Inventory");
        }
         
    }

    //generate party members whether from roster or inventory
    public void PrefabGenerator(GameObject partyCharacterPrefabs, Transform container, CharacterTemplate character){
        

        GameObject partyCharacter = Instantiate(partyCharacterPrefabs, container);            
            
        //make space for the character in the party container using the currentPosY
        RectTransform rect = partyCharacter.GetComponent<RectTransform>();
        Button button = partyCharacter.GetComponent<Button>();

        //on click method that brings up the stats panel
        button.onClick.AddListener(() => partyStatsManager.SaveCharacterData(character));
    }
}


