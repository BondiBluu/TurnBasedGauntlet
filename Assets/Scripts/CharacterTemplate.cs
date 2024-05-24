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
    bool isDowned;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeMP(float mp){
        currentMP -= mp;
        if(currentMP < 0){
            currentMP = 0;
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

    public void GainEXP(int exp){
        //tentative method to gain exp
        currentEXP += exp;
        if(currentEXP >= maxEXP){
            LevelUpRoll();
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
