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

    //stats saved for checkpoint
    int checkpointLevel;
    int checkpointEXP;
    int checkpointMaxEXP;
    float checkpointHP;
    float checkpointMP;
    float checkpointAttack;
    float checkpointDefense;
    float checkpointSpeed;
    float checkpointMagic;
    float checkpointResistance;
    float checkpointSkill;
    float checkpointEfficiency;
    

    void Awake(){
        if(characterStatus != CharacterStatus.Downed){
            characterStatus = CharacterStatus.Normal;
        }
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
            characterStatus = CharacterStatus.Downed;
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

    //ApplyDebuffs 
    public void ApplyDebuff(DamagingMoves.Debuff[] debuffs, float debuffValues){
        foreach(DamagingMoves.Debuff debuff in debuffs){
            switch(debuff){
                case DamagingMoves.Debuff.Attack:
                    currentAttack -= debuffValues;
                    break;
                case DamagingMoves.Debuff.Defense:
                    currentDefense -= debuffValues;
                    break;
                case DamagingMoves.Debuff.Speed:
                    currentSpeed -= debuffValues;
                    break;
                case DamagingMoves.Debuff.Magic:
                    currentMagic -= debuffValues;
                    break;
                case DamagingMoves.Debuff.Resistance:
                    currentResistance -= debuffValues;
                    break;
                case DamagingMoves.Debuff.Skill:
                    currentSkill -= debuffValues;
                    break;
                case DamagingMoves.Debuff.Efficiency:
                    currentEfficiency -= debuffValues;
                    break;
                case DamagingMoves.Debuff.None:
                    break;
            }
        }
    }

    //apply buffs
    public void ApplyBuff(SupplementaryMoves.Boost[] buffs, float buffValues){
        foreach(SupplementaryMoves.Boost buff in buffs){
            switch(buff){
                case SupplementaryMoves.Boost.Attack:
                    currentAttack += buffValues;
                    break;
                case SupplementaryMoves.Boost.Defense:
                    currentDefense += buffValues;
                    break;
                case SupplementaryMoves.Boost.Speed:
                    currentSpeed += buffValues;
                    break;
                case SupplementaryMoves.Boost.Magic:
                    currentMagic += buffValues;
                    break;
                case SupplementaryMoves.Boost.Resistance:
                    currentResistance += buffValues;
                    break;
                case SupplementaryMoves.Boost.Skill:
                    currentSkill += buffValues;
                    break;
                case SupplementaryMoves.Boost.Efficiency:
                    currentEfficiency += buffValues;
                    break;
                case SupplementaryMoves.Boost.None:
                    break;
            }
        }
    }

    //remove debuffs
    public void RemoveDebuffs(HealingMoves.HealDebuff[] healDebuffs){
        foreach(HealingMoves.HealDebuff healDebuff in healDebuffs){
            switch(healDebuff){
                case HealingMoves.HealDebuff.Attack:
                    currentAttack = maxAttack;
                    break;
                case HealingMoves.HealDebuff.Defense:
                    currentDefense = maxDefense;
                    break;
                case HealingMoves.HealDebuff.Speed:
                    currentSpeed = maxSpeed;
                    break;
                case HealingMoves.HealDebuff.Magic:
                    currentMagic = maxMagic;
                    break;
                case HealingMoves.HealDebuff.Resistance:
                    currentResistance = maxResistance;
                    break;
                case HealingMoves.HealDebuff.Skill:
                    currentSkill = maxSkill;
                    break;
                case HealingMoves.HealDebuff.Efficiency:
                    currentEfficiency = maxEfficiency;
                    break;
            }
        }
    }

    //grabbing stats from this checkpoint, in case the player loses
    public void SaveCheckpointStats(){
        checkpointLevel = currentLevel;
        checkpointEXP = currentEXP;
        checkpointMaxEXP = maxEXP;
        checkpointHP = currentHP;
        checkpointMP = currentMP;
        checkpointAttack = currentAttack;
        checkpointDefense = currentDefense;
        checkpointSpeed = currentSpeed;
        checkpointMagic = currentMagic;
        checkpointResistance = currentResistance;
        checkpointSkill = currentSkill;
        checkpointEfficiency = currentEfficiency;
    }

    //if the player loses, revert to the checkpoint stats
    public void RevertToCheckpoint(){
        currentLevel = checkpointLevel;
        currentEXP = checkpointEXP;
        maxEXP = checkpointMaxEXP;
        currentHP = checkpointHP;
        currentMP = checkpointMP;
        currentAttack = checkpointAttack;
        currentDefense = checkpointDefense;
        currentSpeed = checkpointSpeed;
        currentMagic = checkpointMagic;
        currentResistance = checkpointResistance;
        currentSkill = checkpointSkill;
        currentEfficiency = checkpointEfficiency;
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

    //set base stats
    public void SetBaseStats(){
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

    public void GainEXP(int exp){
        //tentative method to gain exp
        currentEXP += exp;
        if(currentEXP >= maxEXP){
            //level up
            currentLevel++;
            //level roll system
            LevelUp(characterData.CharaStatList.Growths);
            //have maxEXP increase by a certain amount and retain the remaining exp
            currentEXP -= maxEXP;
            //increase max exp
            maxEXP += 100;
        }
    }
    

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
                LevelUp(characterData.CharaStatList.Growths);
            }
        }
    }

    public void LevelUp(StatsList.LevelGrowth[] growth){
        //will change based on level up type
        currentLevel++;

        LevelRoll(growth[0], ref maxHP);
        LevelRoll(growth[1], ref maxMP);
        LevelRoll(growth[2], ref maxAttack);
        LevelRoll(growth[3], ref maxDefense);
        LevelRoll(growth[4], ref maxSpeed);
        LevelRoll(growth[5], ref maxMagic);
        LevelRoll(growth[6], ref maxResistance);
        LevelRoll(growth[7], ref maxSkill);
        LevelRoll(growth[8], ref maxEfficiency);
    }

    public void LevelRoll(StatsList.LevelGrowth growth, ref float statToBeGrown){
        //TODO: find the right values for the growths
        switch(growth){
            case StatsList.LevelGrowth.Excellent:
                break;
            case StatsList.LevelGrowth.Great:
                break;
            case StatsList.LevelGrowth.Good:
                break;
            case StatsList.LevelGrowth.Bad:
                break;
            case StatsList.LevelGrowth.Worst:
                break;
            case StatsList.LevelGrowth.Abnormal:
                break;
        }
    }
}
