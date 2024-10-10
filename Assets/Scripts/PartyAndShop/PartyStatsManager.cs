using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PartyStatsManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text characterOrigin;
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text characterHp;
    [SerializeField] TMP_Text characterMp;
    [SerializeField] TMP_Text characterLvl;
    [SerializeField] TMP_Text characterAtk;
    [SerializeField] TMP_Text characterDef;
    [SerializeField] TMP_Text characterMag;
    [SerializeField] TMP_Text characterRes;
    [SerializeField] TMP_Text characterEff;
    [SerializeField] TMP_Text characterSki;
    [SerializeField] TMP_Text characterSpd;
    [SerializeField] TMP_Text characterAbility;

    [Header("Buttons")]
    [SerializeField] Button closeStatsButton;
    [SerializeField] Button closePartyButton;
    [SerializeField] Button viewCharacterButton;
    [SerializeField] Button switchCharactersButton;
    [SerializeField] Button removeCharacterButton; //only available when choosing a character in the party
    [SerializeField] Button addCharacterButton; //only available when choosing a character in the inventory
    //[SerializeField] Button equipCharacterButton;
    
    [Header("Panels")]
    CharacterTemplate chosenCharacter;

    PartyManager partyManager;
    ShopButtonController shopButtonController;

    private void Start()
    {
        partyManager = FindObjectOfType<PartyManager>();
        shopButtonController = FindObjectOfType<ShopButtonController>();
    }

    public void SetCharacterStats(CharacterTemplate character)
    {
        character.SetBaseStats(); //TO BE REMOVED

        //set the character’s stats in the stats panel
        Debug.Log(character.characterData.CharaStatList.CharacterName);
        //characterImage.sprite = character.characterData.CharaSprite;
        //characterOrigin.text = character.characterData.CharaStatList.CharacterOrigin;
        characterName.text = character.characterData.CharaStatList.CharacterName;
        characterHp.text = $"HP: {StatsDifference(character.maxHP, character.currentHP)}";
        characterMp.text = $"MP: {StatsDifference(character.maxMP, character.currentMP)}";
        characterLvl.text = $"LVL: {character.currentLevel}";
        characterAtk.text = $"Attack: {StatsDifference(character.maxAttack, character.currentAttack)}";
        characterDef.text = $"Defense: {StatsDifference(character.maxDefense, character.currentDefense)}";
        characterMag.text = $"Magic: {StatsDifference(character.maxMagic, character.currentMagic)}";
        characterRes.text = $"Resistance: {StatsDifference(character.maxResistance, character.currentResistance)}";
        characterEff.text = $"Efficiency: {StatsDifference(character.maxEfficiency, character.currentEfficiency)}";
        characterSki.text = $"Skill: {StatsDifference(character.maxSkill, character.currentSkill)}";
        characterSpd.text = $"Speed: {StatsDifference(character.maxSpeed, character.currentSpeed)}";
        //characterAbility.text = character.characterData.CharaStatList.CharacterAbility; 
               
    }

    public string StatsDifference(float baseStatValue, float currentStatValue){
        float statDifference = Convert.ToInt32(currentStatValue - baseStatValue);
        return $"{baseStatValue} (+{statDifference})";
    }

    //setting all buttons but close to inactive. player will have to choose a character first to enable the buttons
    public void SetButtonsInactive(){
        viewCharacterButton.interactable = false;
        switchCharactersButton.interactable = false;
        removeCharacterButton.interactable = false;
        addCharacterButton.interactable = false;
        //equipCharacterButton.interactable = false;
    }

    //setting view, switch, and equip buttons to interactable
    public void SetButtonsActive(){
        viewCharacterButton.interactable = true;
        switchCharactersButton.interactable = true;
        //equipCharacterButton.interactable = true;

        //if the character is one of the characters in the current party, the remove button will be interactable
        if(partyManager.currentParty.Contains(chosenCharacter)){
            removeCharacterButton.interactable = true;
        }
        else{
            //if the character is not in the current party, the add button will be interactable
            addCharacterButton.interactable = true;
        }

        //removing previous listeners
        viewCharacterButton.onClick.RemoveAllListeners();

        //adding a listener to the view button
        viewCharacterButton.onClick.AddListener(() => shopButtonController.OpenStats(chosenCharacter));

    }


    //saving the character's data
    public void SaveCharacterData(CharacterTemplate character){
        chosenCharacter = character;
        Debug.Log("Character saved. Character name: " + chosenCharacter.characterData.CharaStatList.CharacterName);
        SetButtonsActive();
    }
}
