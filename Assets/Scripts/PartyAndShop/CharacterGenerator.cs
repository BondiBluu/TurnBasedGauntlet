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

    public void Start()
    {
        partyManager = FindObjectOfType<PartyManager>();
        shopButtonController = FindObjectOfType<ShopButtonController>();
    }
    public void GenerateParty()
    {
        //clear the party container before generating new party members
        foreach (Transform partyCharacter in partyContainer)
        {
            Destroy(partyCharacter.gameObject);
        }

        float currentPosY = 100f;

        Debug.Log("Generating Party");
        //generate all 4 party members from the party manager to the party container using the party character prefab
        foreach (CharacterTemplate character in partyManager.currentParty)
        {
            GameObject partyCharacter = Instantiate(partyCharacterPrefab, partyContainer);            
            
            //make space for the character in the party container using the currentPosY
            RectTransform rect = partyCharacter.GetComponent<RectTransform>();
            Button button = partyCharacter.GetComponent<Button>();
            //give space between buttons so that the next button goes below the previous one
            rect.anchoredPosition = new Vector2(0, currentPosY);
            currentPosY -= buttonSpacing + rect.sizeDelta.y; 

            //on click method that brings up the stats panel
            button.onClick.AddListener(() => shopButtonController.OpenStats(character));
        }
         
    }
}
