using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Supplementary Move", menuName = "Moves/Supplementary Move")]
public class SupplementaryMoves : Moves
{
    public enum Boost
    {
        Attack,
        Defense,
        Speed,
        Magic,
        Resistance,
        Skill,
        Efficiency,
        None
    }

    public enum StatusType
    {
        Freeze,
        Burn,
        Poison,
        Cure,
        Still,
        None
    }

    [SerializeField] Boost[] buffs;
    [SerializeField] Boost[] debuffs;
    [SerializeField] float buffValue;
    [SerializeField] float debuffValue;

    //can have multiple status effects
    [SerializeField] StatusType[] statusEffects;

    void Awake()
    {
        MovesType = MoveType.Supplementary;
    }

    public Boost[] Buffs { get { return buffs; } }
    public Boost[] Debuffs { get { return debuffs; } }
    public float BuffValue { get { return buffValue; } }
    public float DebuffValue { get { return debuffValue; } }
    public StatusType[] StatusEffects { get { return statusEffects; } }

}
