using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;


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



    public void SetCharacterStats(CharacterTemplate character)
    {
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
}
