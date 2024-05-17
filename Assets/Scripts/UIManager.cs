using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("For HUD")]
    public Transform allyContainer;
    public Transform enemyContainer;
    public Transform characterHUDContainer;
    public GameObject characterHUDPanelPrefab;
    public GameObject allyHUDPanelPrefab;
    public GameObject enemyHUDPanelPrefab;

    [Header("For UI")]
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

    public float buttonSpacing = 50f;

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
    public void InstantiateCharacterPanels(List <CharacterTemplate> charas){

        float currentPosY = 0f;

        for(int i = 0; i < charas.Count; i++){

            //set the character's name, level, HP, and MP
            TMP_Text characterName = characterHUDPanelPrefab.transform.GetChild(0).GetComponent<TMP_Text>();
            characterName.text = charas[i].characterData.CharaStatList.CharacterName;

            TMP_Text characterLevel = characterHUDPanelPrefab.transform.GetChild(1).GetComponent<TMP_Text>();
            characterLevel.text = "Level: " + charas[i].currentLevel;

            TMP_Text characterHP = characterHUDPanelPrefab.transform.GetChild(2).GetComponent<TMP_Text>();
            characterHP.text = charas[i].currentHP + "/" + charas[i].maxHP;

            TMP_Text characterMP = characterHUDPanelPrefab.transform.GetChild(3).GetComponent<TMP_Text>();
            characterMP.text =  charas[i].currentMP + "/" + charas[i].maxMP;
            
            //set the max value of the slider to the character's max HP
            Image whiteHPBar = characterHUDPanelPrefab.transform.GetChild(4).GetComponent<Image>();
            Slider subHPSlider = whiteHPBar.transform.GetChild(3).GetComponent<Slider>();
            subHPSlider.maxValue = charas[i].maxHP;
            subHPSlider.value = charas[i].currentHP;
            Slider mainHPSlider = whiteHPBar.transform.GetChild(4).GetComponent<Slider>();
            mainHPSlider.maxValue = charas[i].maxHP;
            mainHPSlider.value = charas[i].currentHP;

            //set the max value of the slider to the character's max MP
            Image whiteMPBar = characterHUDPanelPrefab.transform.GetChild(5).GetComponent<Image>();
            Slider subMPSlider = whiteMPBar.transform.GetChild(3).GetComponent<Slider>();
            subMPSlider.maxValue = charas[i].maxMP;
            subMPSlider.value = charas[i].currentMP;
            Slider mainMPSlider = whiteMPBar.transform.GetChild(4).GetComponent<Slider>();
            mainMPSlider.maxValue = charas[i].maxMP;
            mainMPSlider.value = charas[i].currentMP;

            //instantiate the panel
            GameObject panel = Instantiate(characterHUDPanelPrefab, characterHUDContainer);

            //have the next panel be directly below the previous panel
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -currentPosY);
            currentPosY += buttonSpacing + panel.GetComponent<RectTransform>().sizeDelta.y;
        }
    }

    public void InstantiateAllyPanels(List <CharacterTemplate> charas){

        float currentPosY = 0f;

        for(int i = 0; i < charas.Count; i++){

            //set the character's name
            Button characterButton = allyHUDPanelPrefab.transform.GetChild(0).GetComponent<Button>();
            characterButton.GetComponentInChildren<TMP_Text>().text = charas[i].characterData.CharaStatList.CharacterName;
            
            //set the max value of the slider to the character's max HP
            Image whiteHPBar = allyHUDPanelPrefab.transform.GetChild(1).GetComponent<Image>();
            Slider mainHPSlider = whiteHPBar.transform.GetChild(2).GetComponent<Slider>();
            mainHPSlider.maxValue = charas[i].maxHP;
            mainHPSlider.value = charas[i].currentHP;

            //set the max value of the slider to the character's max MP
            Image whiteMPBar = allyHUDPanelPrefab.transform.GetChild(2).GetComponent<Image>();
            Slider mainMPSlider = whiteMPBar.transform.GetChild(2).GetComponent<Slider>();
            mainMPSlider.maxValue = charas[i].maxMP;
            mainMPSlider.value = charas[i].currentMP;

            //instantiate the panel
            GameObject panel = Instantiate(allyHUDPanelPrefab, allyContainer);

            //have the next panel be directly below the previous panel
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -currentPosY);
            currentPosY += buttonSpacing + panel.GetComponent<RectTransform>().sizeDelta.y;
        }
    }

    public void InstantiateEnemyPanels(List <CharacterTemplate> charas){
        float currentPosY = 0f;

        for(int i = 0; i < charas.Count; i++){

            //set the character's name
            Button characterButton = allyHUDPanelPrefab.transform.GetChild(0).GetComponent<Button>();
            characterButton.GetComponentInChildren<TMP_Text>().text = charas[i].characterData.CharaStatList.CharacterName;
            
            //set the max value of the slider to the character's max HP
            Image whiteHPBar = allyHUDPanelPrefab.transform.GetChild(1).GetComponent<Image>();
            Slider mainHPSlider = whiteHPBar.transform.GetChild(2).GetComponent<Slider>();
            mainHPSlider.maxValue = charas[i].maxHP;
            mainHPSlider.value = charas[i].currentHP;

            //instantiate the panel
            GameObject panel = Instantiate(enemyHUDPanelPrefab, enemyContainer);

            //have the next panel be directly below the previous panel
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -currentPosY);
            currentPosY += buttonSpacing + panel.GetComponent<RectTransform>().sizeDelta.y;
        }
    }
}
