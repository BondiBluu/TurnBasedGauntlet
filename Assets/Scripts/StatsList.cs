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

    public enum LevelUpType{
        PhysicalTank,
        MagicalTank,
        Jammer,
        PhysicalDPS,
        MagicalDPS,
        Support,
        Balanced,
        Speedster,
        LateBloomer,
        All
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
    [SerializeField] Immunity[] immunities;
    [SerializeField] LevelUpType[] levelType;

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
    public Immunity[] Immunities { get { return immunities; } }
    public LevelUpType[] LevelType { get { return levelType; } }
    }
