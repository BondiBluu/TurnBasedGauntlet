using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("For HUD")]
    public GameObject allyContainer;
    public GameObject enemyContainer;
    public GameObject characterHUDContainer;

    [Header("For UI")]
    public TMP_Text characterName;
    public TMP_Text characterLevel;
    public TMP_Text characterHP;
    public TMP_Text characterMP;
    public Slider characterHPSlider;
    public Slider characterMPSlider;
    public Image characterActionImage;

    [Header("For Stats")]
    public Image characterImage;
    public TMP_Text statsCharaName;
    public TMP_Text statsCharaLevel;
    public TMP_Text statsCharaHP;
    public TMP_Text statsCharaMP;
    public TMP_Text charaAbility;
    public TMP_Text charaAtk;
    public TMP_Text charaDef;
    public TMP_Text charaMag;
    public TMP_Text charaRes;
    public TMP_Text charaEff;
    public TMP_Text charaSki;
    public TMP_Text charaSpd;
    public TMP_Text charaPointToNextLvl;

    public void ShowStats(CharacterTemplate character)
    {
        //TODO: have the image of the character
        statsCharaName.text = character.characterData.CharaStatList.CharacterName;
        statsCharaLevel.text = "Level: " + character.currentLevel;
        statsCharaHP.text = $"HP: {character.currentHP}/{character.maxHP}";
        statsCharaMP.text = $"MP: {character.currentMP}/{character.maxMP}";
        charaAbility.text = $"{character.characterData.CharaStatList.CharacterAbility} {character.characterData.CharaStatList.AbilityDesc}";
        SetStats(charaAtk, character.currentAttack, character.maxAttack, "Attack");
        SetStats(charaDef, character.currentDefense, character.maxDefense, "Defense");
        SetStats(charaMag, character.currentMagic, character.maxMagic, "Magic");
        SetStats(charaRes, character.currentResistance, character.maxResistance, "Resistance");
        SetStats(charaEff, character.currentEfficiency, character.maxEfficiency, "Efficiency");
        SetStats(charaSki, character.currentSkill, character.maxSkill, "Skill");
        SetStats(charaSpd, character.currentSpeed, character.maxSpeed, "Speed");
    }

    public void SetStats(TMP_Text text, float currentStatValue, float baseStatValue, string statName){
        float statDifference = Convert.ToInt32(currentStatValue - baseStatValue);
        string sign = (statDifference >= 0) ? $"<color=#6EFFFF>+{statDifference}</color>" : $"<color=#FF1100>{statDifference}</color>";

        text.text = $"{statName}: {baseStatValue} ({sign})";
    }

    //make an event that, when PlayerSetup begins, all player HUDS will be set up
    public void SetUpHUD(){

    }
}
