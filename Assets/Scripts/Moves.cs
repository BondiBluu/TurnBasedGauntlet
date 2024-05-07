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

    //Could be a symbol, so may have to be a gameObject variable or Image var
    [SerializeField] string moveTypeString;
    [SerializeField] float movePower;
    [SerializeField] float mpCost;
    [SerializeField] int lvlGotten;
    [SerializeField] MoveType[] moveTypes;
    [SerializeField] AttackType attackingType;
    [SerializeField] StatusType charaStatusType;
    [SerializeField] Boost[] buffs;
    [SerializeField] Boost[] debuffs;
    [SerializeField] TargetNumber targetNumbers;

    public string MoveName { get => moveName; set => moveName = value; }
    public string MovePowerString { get => movePowerString; set => movePowerString = value; }
    public string MoveCostString { get => moveCostString; set => moveCostString = value; }
    public string MoveExplanation { get => moveExplanation; set => moveExplanation = value; }
    public string MoveTypeString { get => moveTypeString; set => moveTypeString = value; }
    public float MovePower { get => movePower; set => movePower = value; }
    public float MpCost { get => mpCost; set => mpCost = value; }
    public int LvlGotten { get => lvlGotten; set => lvlGotten = value; }
    public MoveType[] MoveTypes { get => moveTypes; set => moveTypes = value; }
    public AttackType AttackingType { get => attackingType; set => attackingType = value; }
    public StatusType CharaStatusType { get => charaStatusType; set => charaStatusType = value; }
    public Boost[] Buffs { get => buffs; set => buffs = value; }
    public Boost[] Debuffs { get => debuffs; set => debuffs = value; }
    public TargetNumber TargetNumbers { get => targetNumbers; set => targetNumbers = value; }
}
