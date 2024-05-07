using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Moves", menuName = "Moves/New Move")]
public class Moves : ScriptableObject
{
    public enum MoveType
    {
        Damaging,
        Healing,
        Supplementary
    }

    public enum AttackType
    {
        Physical,
        Magical
    }

    public enum StatusType{
        Freeze,
        Burn,
        Poison,
        Cure,
        Still,
        None
    }

    public enum Boost{
        Attack,
        Defense,
        Speed,
        Magic,
        Resistance,
        Skill,
        Efficiency,
        None
        }

        public enum TargetNumber{
            Single,
            Multi,
            All,
        }

    [SerializeField] string moveName;
    [SerializeField] string movePowerString;
    [SerializeField] string moveCostString;
    [SerializeField] string moveExplanation;
    [SerializeField] string moveTypeString;
    [SerializeField] float movePower;
    [SerializeField] float healPower;
    [SerializeField] float mpCost;
    [SerializeField] int lvlGotten;

    //can be damaging and healing for drain type moves
    [SerializeField] MoveType[] moveTypes;

    //will never be physical and magical
    [SerializeField] AttackType attackingType;

    //can have more than one status effect
    [SerializeField] StatusType[] statusTypes;
    [SerializeField] Boost[] buffs;
    [SerializeField] Boost[] debuffs;
    [SerializeField] TargetNumber targetNumbers;

    public string MoveName { get => moveName; set => moveName = value; }
    public string MovePowerString { get => movePowerString; set => movePowerString = value; }
    public string MoveCostString { get => moveCostString; set => moveCostString = value; }
    public string MoveExplanation { get => moveExplanation; set => moveExplanation = value; }
    public string MoveTypeString { get => moveTypeString; set => moveTypeString = value; }
    public float MovePower { get => movePower; set => movePower = value; }
    public float HealPower { get => healPower; set => healPower = value; }
    public float MpCost { get => mpCost; set => mpCost = value; }
    public int LvlGotten { get => lvlGotten; set => lvlGotten = value; }
    public MoveType[] MoveTypes { get => moveTypes; set => moveTypes = value; }
    public AttackType AttackingType { get => attackingType; set => attackingType = value; }
    public StatusType[] StatusTypes { get => statusTypes; set => statusTypes = value; }
    public Boost[] Buffs { get => buffs; set => buffs = value; }
    public Boost[] Debuffs { get => debuffs; set => debuffs = value; }
    public TargetNumber TargetNumbers { get => targetNumbers; set => targetNumbers = value; }
}
