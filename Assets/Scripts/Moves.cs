using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Moves/New Move")]
public class Moves : ScriptableObject
{
    public enum MoveType
    {
        //needs to be changed to Damaging, Healing, and Supplementary
        Damaging,
        Healing,
        Supplementary,
        Cure,
        Drain
    }

    public enum AttackType
    {
        Physical,
        Magical,
        None
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
    [SerializeField] MoveType movesType;

    //will never be physical and magical
    [SerializeField] AttackType attackingType;

    //can have more than one status effect
    [SerializeField] StatusType[] statusTypes;
    [SerializeField] Boost[] buffs;
    [SerializeField] Boost[] debuffs;
    [SerializeField] float buffValue;
    [SerializeField] float debuffValue;
    [SerializeField] TargetNumber targetNumbers;

    public string MoveName { get { return moveName; } }
    public string MovePowerString { get { return movePowerString; } }
    public string MoveCostString { get { return moveCostString; } }
    public string MoveExplanation { get { return moveExplanation; } }
    public string MoveTypeString { get { return moveTypeString; } }
    public float MovePower { get { return movePower; } }
    public float HealPower { get { return healPower; } }
    public float MPCost { get { return mpCost; } }
    public int LvlGotten { get { return lvlGotten; } }
    public MoveType MovesType { get { return movesType; } }
    public AttackType AttackingType { get { return attackingType; } }
    public StatusType[] StatusTypes { get { return statusTypes; } }
    public Boost[] Buffs { get { return buffs; } }
    public Boost[] Debuffs { get { return debuffs; } }
    public float BuffValue { get { return buffValue; } }
    public float DebuffValue { get { return debuffValue; } }
    public TargetNumber TargetNumbers { get { return targetNumbers; } }
}
