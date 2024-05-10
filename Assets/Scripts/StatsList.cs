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
        None
    }

    [SerializeField] string characterName;
    
    [TextArea(15, 20)]
    [SerializeField] string characterDesc;
    [SerializeField] string characterAbility;

    [TextArea(15, 20)]
    [SerializeField] string abilityDesc;
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

    public string CharacterName { get => characterName; set => characterName = value; }
    public string CharacterDesc { get => characterDesc; set => characterDesc = value; }
    public string CharacterAbility { get => characterAbility; set => characterAbility = value; }
    public string AbilityDesc { get => abilityDesc; set => abilityDesc = value; }
    public float BaseHP { get => baseHP; set => baseHP = value; }
    public float BaseMP { get => baseMP; set => baseMP = value; }
    public float BaseAttack { get => baseAttack; set => baseAttack = value; }
    public float BaseDefense { get => baseDefense; set => baseDefense = value; }
    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float BaseMagic { get => baseMagic; set => baseMagic = value; }
    public float BaseResistance { get => baseResistance; set => baseResistance = value; }
    public float BaseSkill { get => baseSkill; set => baseSkill = value; }
    public float BaseEfficiency { get => baseEfficiency; set => baseEfficiency = value; }
    public Immunity[] Immunities { get => immunities; set => immunities = value; }



}
