using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterTemplate 
{
    public enum CharacterStatus
    {
        Normal,
        Freeze,
        Burn,
        Poison,
        Still,
        Downed
    }
    public enum CharacterType
    {
        Friendly,
        Enemy
    }
    public CharacterType characterType;
    public CharacterStatus characterStatus;
    public CharacterData characterData;
    public int currentLevel;
    public int currentEXP;
    public int maxEXP;
    public float maxHP;
    public float currentHP;
    public float maxMP;
    public float currentMP;
    public float maxAttack;
    public float currentAttack;
    public float maxDefense;
    public float currentDefense;
    public float maxSpeed;
    public float currentSpeed;
    public float maxMagic;
    public float currentMagic;
    public float maxResistance;
    public float currentResistance;
    public float maxSkill;
    public float currentSkill;
    public float maxEfficiency;
    public float currentEfficiency;

    public void TakeMP(float mp){
        currentMP -= mp;
        if(currentMP < 0){
            currentMP = 0;
        }
    }

    public void TakeDamage(float damage){
        currentHP -= damage;
        if(currentHP < 0){
            currentHP = 0;
            //characterStatus = CharacterStatus.Downed;
        }
    }

    public void HealDamage(float heal, float healMP){
        currentHP += heal;
        currentMP += healMP;

        if(currentHP > maxHP){
            currentHP = maxHP;
        }
        if(currentMP > maxMP){
            currentMP = maxMP;
        }
    }

    //ApplyBuffandDebuff using the Boost enum from Moves.cs
    public void ApplyBuffandDebuff(){

    }

    //remove debuffs
    public void RemoveDebuffs(){

    }

    //revert stats back at the start of battle
    public void RevertStats(){
        currentAttack = maxAttack;
        currentDefense = maxDefense;
        currentSpeed = maxSpeed;
        currentMagic = maxMagic;
        currentResistance = maxResistance;
        currentSkill = maxSkill;
        currentEfficiency = maxEfficiency;
    }

    public void GainEXP(int exp){
        //tentative method to gain exp
        currentEXP += exp;
        if(currentEXP >= maxEXP){
            //level up
            currentLevel++;
            //level roll system
            //have maxEXP increase by a certain amount and retain the remaining exp
            currentEXP -= maxEXP;
            //increase max exp
            maxEXP += 100;
        }
    }
    
    //TODO: method to take away accumulated stat rolls and levels when failing a checkpoint

    public void TurnToBaseLevel(){
        currentLevel = characterData.CharaStatList.BaseLvl;
        maxHP = characterData.CharaStatList.BaseHP;
        currentHP = maxHP;
        maxMP = characterData.CharaStatList.BaseMP;
        currentMP = maxMP;
        maxAttack = characterData.CharaStatList.BaseAttack;
        currentAttack = maxAttack;
        maxDefense = characterData.CharaStatList.BaseDefense;
        currentDefense = maxDefense;
        maxSpeed = characterData.CharaStatList.BaseSpeed;
        currentSpeed = maxSpeed;
        maxMagic = characterData.CharaStatList.BaseMagic;
        currentMagic = maxMagic;
        maxResistance = characterData.CharaStatList.BaseResistance;
        currentResistance = maxResistance;
        maxSkill = characterData.CharaStatList.BaseSkill;
        currentSkill = maxSkill;
        maxEfficiency = characterData.CharaStatList.BaseEfficiency;
        currentEfficiency = maxEfficiency;
    }

    //if current level is more than 0, level up before the start of the game
    public void LevelUpFromBase(){
        if(currentLevel > 0){
            for(int i = 0; i < currentLevel; i++){
                LevelUpRoll();
            }
        }
    }

    public void LevelUpRoll(){
        //will change based on level up type
        currentLevel++;
        maxHP += Random.Range(1, 5);
        currentHP = maxHP;
        maxMP += Random.Range(1, 5);
        currentMP = maxMP;
        maxAttack += Random.Range(1, 5);
        maxDefense += Random.Range(1, 5);
        maxSpeed += Random.Range(1, 5);
        maxMagic += Random.Range(1, 5);
        maxResistance += Random.Range(1, 5);
        maxSkill += Random.Range(1, 5);
        maxEfficiency += Random.Range(1, 5);
    }
}
