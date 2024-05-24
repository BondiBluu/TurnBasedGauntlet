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

    LevelUpRolls levelUpRolls = new LevelUpRolls();

    // Start is called before the first frame update
    void Start()
    {
        RevertStats();
        characterStatus = CharacterStatus.Normal;
    }

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
    public void ApplyBuffandDebuff(Moves.Boost[] boosts, float value){
        foreach(Moves.Boost boostType in boosts){
            switch(boostType){
                case Moves.Boost.Attack:
                    currentAttack += (int)(characterData.CharaStatList.BaseAttack * value);
                    break;
                case Moves.Boost.Defense:
                    currentDefense += (int)(characterData.CharaStatList.BaseDefense * value);
                    break;
                case Moves.Boost.Speed:
                    currentSpeed += (int)(characterData.CharaStatList.BaseSpeed * value);
                    break;
                case Moves.Boost.Magic:
                    currentMagic += (int)(characterData.CharaStatList.BaseMagic * value);
                    break;
                case Moves.Boost.Resistance:
                    currentResistance += (int)(characterData.CharaStatList.BaseResistance * value);
                    break;
                case Moves.Boost.Skill:
                    currentSkill += (int)(characterData.CharaStatList.BaseSkill * value);
                    break;
                case Moves.Boost.Efficiency:
                    currentEfficiency += (int)(characterData.CharaStatList.BaseEfficiency * value);
                    break;
            }
        }
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
            levelUpRolls.LevelRoll(this);
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
    public void LevelUpToBase(){
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
