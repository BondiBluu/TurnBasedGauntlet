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

    //showing the leveling up panel
    public void LevelUpPanel(CharacterTemplate character){
        levelUpPanel.SetActive(true);

        levelUpText.text = $"{character.characterData.CharaStatList.CharacterName} has leveled up!";
        levelUp.text = "Level: + 1";
        levelUpHP.text = $"HP: {character.previousCharaMaxStats[0]} + ({character.characterGrowths[0]})";
        levelUpMP.text = $"MP: {character.previousCharaMaxStats[1]} + ({character.characterGrowths[1]})";
        levelUpAtk.text = $"ATK: {character.previousCharaMaxStats[2]} + ({character.characterGrowths[2]})";
        levelUpDef.text = $"DEF: {character.previousCharaMaxStats[3]} + ({character.characterGrowths[3]})";
        levelUpSpd.text = $"SPD: {character.previousCharaMaxStats[4]} + ({character.characterGrowths[4]})";
        levelUpMag.text = $"MAG: {character.previousCharaMaxStats[5]} + ({character.characterGrowths[5]})";
        levelUpRes.text = $"RES: {character.previousCharaMaxStats[6]} + ({character.characterGrowths[6]})";
        levelUpSki.text = $"SKI: {character.previousCharaMaxStats[7]} + ({character.characterGrowths[7]})";
        levelUpEff.text = $"EFF: {character.previousCharaMaxStats[8]} + ({character.characterGrowths[8]})";

        //if character has a new move, show it

        //making close button inactive and next button active
        closeButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);

        //add on click event to show NextPanel
        nextButton.onClick.AddListener(() => NextPanel(character));
    }

    public void NextPanel(CharacterTemplate character){
        levelUp.text = $"Level: {character.currentLevel}";
        levelUpHP.text = $"HP: {character.maxHP}";
        levelUpMP.text = $"MP: {character.maxMP}";
        levelUpAtk.text = $"ATK: {character.maxAttack}";
        levelUpDef.text = $"DEF: {character.maxDefense}";
        levelUpSpd.text = $"SPD: {character.maxSpeed}";
        levelUpMag.text = $"MAG: {character.maxMagic}";
        levelUpRes.text = $"RES: {character.maxResistance}";
        levelUpSki.text = $"SKI: {character.maxSkill}";
        levelUpEff.text = $"EFF: {character.maxEfficiency}";

        //if character has a new move, show it

        //making close button active and next button inactive
        closeButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);

        //add on click event to close the panel
        closeButton.onClick.AddListener(() => CloseLevelUpPanel());
    }

    public void CloseLevelUpPanel(){
        levelUpPanel.SetActive(false);
    }
}
