using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpRolls : MonoBehaviour
{
    //leveling up 
    public void LevelUpRoll(CharacterTemplate character){
        
    }
    

    //To Note: will gather all stat rolls and roll them all at once

    //leveling up based on type: may be int instead of void
    public void LevelTank(CharacterTemplate character){
        //roll for HP
        int hpRoll = Random.Range(2, 7);
        //roll for MP
        int mpRoll = Random.Range(0, 2);
        //roll for Attack
        int atkRoll = Random.Range(1, 6);
        //roll for Defense
        int defRoll = Random.Range(1, 6);
        //roll for Magic
        int magRoll = Random.Range(1, 6);
        //roll for Resistance
        int resRoll = Random.Range(1, 6);
        //roll for Skill
        int skillRoll = Random.Range(1, 6);
        //roll for Efficiency
        int effRoll = Random.Range(1, 6);


        //add the rolls to the character's stats
        character.maxHP += hpRoll;
        character.maxMP += mpRoll;
        character.maxAttack += atkRoll;
        character.maxDefense += defRoll;
        character.maxMagic += magRoll;
        character.maxResistance += resRoll;
        character.maxSkill += skillRoll;
        character.maxEfficiency += effRoll;
    }

    public void LevelDPS(CharacterTemplate character){

    }
}
