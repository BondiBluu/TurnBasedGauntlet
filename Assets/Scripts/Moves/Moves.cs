using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : ScriptableObject
{
    public enum MoveType
    {
        Damaging,
        Healing,
        Supplementary
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
    [SerializeField] float mpCost;
    [SerializeField] int lvlGotten;

    //can be damaging and healing for drain type moves
    [SerializeField] MoveType movesType;
    [SerializeField] TargetNumber targetNumbers;

    public string MoveName { get { return moveName; } }
    public string MovePowerString { get { return movePowerString; } }
    public string MoveCostString { get { return moveCostString; } }
    public string MoveExplanation { get { return moveExplanation; } }
    public string MoveTypeString { get { return moveTypeString; } }
    public float MPCost { get { return mpCost; } }
    public int LvlGotten { get { return lvlGotten; } }
    public MoveType MovesType { get { return movesType; } set { movesType = value; }}
    public TargetNumber TargetNumbers { get { return targetNumbers; } }
}
