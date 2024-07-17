using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpRolls : MonoBehaviour
{
    //leveling up 
     [Header("For Level Up")]
    public GameObject levelUpPanel;
    public TMP_Text levelUpText;
    public TMP_Text levelUp;
    public TMP_Text levelUpHP;
    public TMP_Text levelUpMP;
    public TMP_Text levelUpAtk;
    public TMP_Text levelUpDef;
    public TMP_Text levelUpMag;
    public TMP_Text levelUpRes;
    public TMP_Text levelUpEff;
    public TMP_Text levelUpSki;
    public TMP_Text levelUpSpd;
    public TMP_Text levelUpMove;
    public Button nextButton;
    public Button closeButton;
    string[] statNames = {"HP", "MP", "ATK", "DEF", "SPD", "MAG", "RES", "SKI", "EFF", "LVL"};
    TMP_Text[] texts;
    ButtonController buttonController;
    
    void Start()
    {
        buttonController = FindObjectOfType<ButtonController>();
        levelUpPanel.SetActive(false);
        //array of texts
        texts = new TMP_Text[]{levelUpHP, levelUpMP, levelUpAtk, levelUpDef, levelUpSpd, levelUpMag, levelUpRes, levelUpSki, levelUpEff, levelUp};
    }


    //showing the leveling up panel
    public void LevelUpPanel(CharacterTemplate character){
        buttonController.DisableButtons();
        levelUpPanel.SetActive(true);

        levelUpText.text = $"{character.characterData.CharaStatList.CharacterName} has leveled up!";
        levelUp.text = "Level: + 1";

        StatsText(texts, statNames, character.previousCharaMaxStats, character.characterGrowths);

        //if character has a new move, show it

        //making close button inactive and next button active
        OpenAndCloseButtons(true);

        //add on click event to show NextPanel
        nextButton.onClick.AddListener(() => NextPanel(character));
        nextButton.Select();
    }
    
    public void StatsText(TMP_Text[] text, string[] statNames, List<float> previousStats, List<float> growths){
        for(int i = 0; i < growths.Count; i++){
            text[i].text = $"{statNames[i]}: {previousStats[i]} + ({growths[i]})";
        }
    }

    public void NextPanelText(TMP_Text[] text, string[] statName, float[] maxStats){
        for(int i = 0; i < maxStats.Length; i++){
            text[i].text = $"{statName[i]}: {maxStats[i]}";
        }
    }

    public void NextPanel(CharacterTemplate character){
        float[] maxStats = {character.maxHP, character.maxMP, character.maxAttack, character.maxDefense, character.maxSpeed, character.maxMagic, character.maxResistance, character.maxSkill, character.maxEfficiency, character.currentLevel};

        NextPanelText(texts, statNames, maxStats);
       
        //if character has a new move, show it

        //making close button active and next button inactive
        OpenAndCloseButtons(false);

        //add on click event to close the panel
        closeButton.onClick.AddListener(() => CloseLevelUpPanel());
        closeButton.Select();

        //clear the lists
        character.previousCharaMaxStats.Clear();
        character.characterGrowths.Clear();
    }

    public void OpenAndCloseButtons(bool activeOn){
        nextButton.gameObject.SetActive(activeOn);
        closeButton.gameObject.SetActive(!activeOn);
        
    }

    public void CloseLevelUpPanel(){
        levelUpPanel.SetActive(false);   
        buttonController.EnableButtons();    
    }
}
