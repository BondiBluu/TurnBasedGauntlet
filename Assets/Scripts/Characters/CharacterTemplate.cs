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

    public List<float> characterGrowths = new List<float>();
    public List<float> previousCharaMaxStats = new List<float>();

     //3 equip slots (1 weapon, 2 pieces of armor)
    public WeaponObject weapon;
    public EquipmentObject equip1;
    public EquipmentObject equip2;



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

    //adds the stats of the equipment to the character
    public void EquipmentStats(){
        if(weapon != null){
            AddWeaponStats(weapon);
        }
        if(equip1 != null){
           AddArmorStats(equip1);
        }
        if(equip2 != null){
            AddArmorStats(equip2);
        }
    }

    //subtracts the stats of the equipment from the character
    public void RemoveEquipmentStats(){
        if(weapon != null){
            RemoveWeaponStats(weapon);
        }
        if(equip1 != null){
            RemoveArmorStats(equip1);
        }
        if(equip2 != null){
            RemoveArmorStats(equip2);
        }
    }

    void RemoveArmorStats(EquipmentObject armor){
        currentDefense -= armor.HPBoost;
        currentDefense -= armor.DefBoost;
        currentMagic -= armor.MagBoost;
        currentResistance -= armor.ResBoost;
        currentSkill -= armor.SkllBoost;
        currentEfficiency -= armor.EffBoost;
        currentSpeed -= armor.SpdBoost;
    }

    void RemoveWeaponStats(WeaponObject weapon){
        currentAttack -= weapon.AtkBoost;
        currentMagic -= weapon.MagBoost;
    }

    void AddWeaponStats(WeaponObject weapon){
        currentAttack += weapon.AtkBoost;
        currentMagic += weapon.MagBoost;
    }

    void AddArmorStats(EquipmentObject armor){
        currentDefense += armor.HPBoost;
        currentDefense += armor.DefBoost;
        currentMagic += armor.MagBoost;
        currentResistance += armor.ResBoost;
        currentSkill += armor.SkllBoost;
        currentEfficiency += armor.EffBoost;
        currentSpeed += armor.SpdBoost;
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
        //revert the stats of the equipment
        RemoveEquipmentStats();

        currentAttack = maxAttack;
        currentDefense = maxDefense;
        currentSpeed = maxSpeed;
        currentMagic = maxMagic;
        currentResistance = maxResistance;
        currentSkill = maxSkill;
        currentEfficiency = maxEfficiency;
        
        //reapply the stats of the equipment
        EquipmentStats();
    } 

    public void GainEXP(int exp){
        //tentative method to gain exp
        currentEXP += exp;
        if(currentEXP >= maxEXP){
            //add the previous stats to a list
            AddPreviousMaxStats();

            //level up
            //Debug.Log($"{characterData.CharaStatList.CharacterName} leveled up!");
            
            //level roll system
            LevelUp(characterData.CharaStatList.Growths);

            //invoke level up event
            
            //have maxEXP increase by a certain amount and retain the remaining exp
            currentEXP -= maxEXP;
            //increase max exp
            maxEXP += 100;
        }
    }

    public void AddPreviousMaxStats(){
        previousCharaMaxStats.Add(maxHP);
        previousCharaMaxStats.Add(maxMP);
        previousCharaMaxStats.Add(maxAttack);
        previousCharaMaxStats.Add(maxDefense);
        previousCharaMaxStats.Add(maxSpeed);
        previousCharaMaxStats.Add(maxMagic);
        previousCharaMaxStats.Add(maxResistance);
        previousCharaMaxStats.Add(maxSkill);
        previousCharaMaxStats.Add(maxEfficiency);
    }

    //used to set the base stats of the character or turn them back to the base stats
    public void SetBaseStats(){
        StatsList statsList = characterData.CharaStatList;
        currentLevel = statsList.BaseLvl;
        //making the max hp and current hp equal the base hp and so on
        maxHP = currentHP = statsList.BaseHP;
        maxMP = currentMP = statsList.BaseMP;
        maxAttack = currentAttack = statsList.BaseAttack;
        maxDefense = currentDefense = statsList.BaseDefense;
        maxSpeed = currentSpeed = statsList.BaseSpeed;
        maxMagic = currentMagic = statsList.BaseMagic;
        maxResistance = currentResistance = statsList.BaseResistance;
        maxSkill = currentSkill = statsList.BaseSkill;
        maxEfficiency = currentEfficiency = statsList.BaseEfficiency;

    }

    //if current level is more than 0, level up before the start of the game
    public void LevelUpFromBase(){
        if(currentLevel > 0){
            for(int i = 0; i < currentLevel; i++){
                LevelUp(characterData.CharaStatList.Growths);
                //make max exp increase by a certain amount 
            }
        }
    }


    public void LevelUp(StatsList.LevelGrowth[] growth){
        //will change based on level up type
        currentLevel++;

        LevelRoll(growth[0], ref maxHP, "HP");
        LevelRoll(growth[1], ref maxMP , "MP");
        LevelRoll(growth[2], ref maxAttack , "Attack");
        LevelRoll(growth[3], ref maxDefense , "Defense");
        LevelRoll(growth[4], ref maxSpeed , "Speed");
        LevelRoll(growth[5], ref maxMagic   , "Magic");
        LevelRoll(growth[6], ref maxResistance , "Resistance");
        LevelRoll(growth[7], ref maxSkill , "Skill");
        LevelRoll(growth[8], ref maxEfficiency , "Efficiency");

        RevertStats();
        currentHP = maxHP;
        currentMP = maxMP;

        //invoke level up event here to update UI
        EventController.instance.AddToLevelUpQueue(this);
    }

    public void LevelRoll(StatsList.LevelGrowth growth, ref float statToBeGrown, string statName){
        int growthValue = 0;
        //TODO: find the right values for the growths
        switch(growth){
            case StatsList.LevelGrowth.Excellent:
            growthValue = Random.Range(8, 10);
                break;
            case StatsList.LevelGrowth.Great:
            growthValue = Random.Range(6, 7);
                break;
            case StatsList.LevelGrowth.Average:
            growthValue = Random.Range(3, 5);
                break;
            case StatsList.LevelGrowth.Bad:
            growthValue = Random.Range(1, 3);
                break;
            case StatsList.LevelGrowth.Worst:
            growthValue = Random.Range(0, 2);
                break;
            case StatsList.LevelGrowth.Abnormal:
            if(currentLevel < 20){
                growthValue = Random.Range(1, 3);
            } else {
                growthValue = Random.Range(9,10);
            }
                break;
                }
        //Debug.Log($" {statName} growth value: {growth}, {growthValue}");
        statToBeGrown += growthValue;

        //add the growth value to the character growths list to use in the level up panel
        characterGrowths.Add(growthValue);

        //probably do a wait until a button is pressed to show the player the growths
    }
}
