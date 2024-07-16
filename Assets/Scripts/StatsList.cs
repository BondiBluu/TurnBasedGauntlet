using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/New Statlist")]
public class StatsList : ScriptableObject
{
    public enum Immunity
    {
        Freeze,
        Burn,
        Poison,
        Cure,
        Still,
        None,
        All
    }

    public enum LevelGrowth{
        Excellent,
        Great,
        Average,
        Bad,
        Worst,
        Abnormal
    }

    [SerializeField] string characterName;
    
    [TextArea(15, 20)]
    [SerializeField] string characterDesc;
    [SerializeField] string characterAbility;

    [TextArea(15, 20)]
    [SerializeField] string abilityDesc;
    [SerializeField] int baseLvl;
    [SerializeField] float baseHP;
    [SerializeField] float baseMP;
    [SerializeField] float baseAttack;
    [SerializeField] float baseDefense;
    [SerializeField] float baseSpeed;
    [SerializeField] float baseMagic;
    [SerializeField] float baseResistance;
    [SerializeField] float baseSkill;
    [SerializeField] float baseEfficiency;
    [SerializeField] int baseExp;
    [SerializeField] LevelGrowth[] growths;
    [SerializeField] Immunity[] immunities;

    void OnEnable(){
        if(growths == null || growths.Length != 9){
            growths = new LevelGrowth[9];
        }
        //if the base level is less than 1, set it to 1
        if(baseLvl == 0){
            baseLvl = 1;
        }
    }

    public string CharacterName { get { return characterName; } }
    public string CharacterDesc { get { return characterDesc; } }
    public string CharacterAbility { get { return characterAbility; } }
    public string AbilityDesc { get { return abilityDesc; } }
    public int BaseLvl { get { return baseLvl; } }
    public float BaseHP { get { return baseHP; } }
    public float BaseMP { get { return baseMP; } }
    public float BaseAttack { get { return baseAttack; } }
    public float BaseDefense { get { return baseDefense; } }
    public float BaseSpeed { get { return baseSpeed; } }
    public float BaseMagic { get { return baseMagic; } }
    public float BaseResistance { get { return baseResistance; } }
    public float BaseSkill { get { return baseSkill; } }
    public float BaseEfficiency { get { return baseEfficiency; } }
    public int BaseExp { get { return baseExp; } }
    public LevelGrowth[] Growths { get { return growths; } }
    public Immunity[] Immunities { get { return immunities; } }
    }



