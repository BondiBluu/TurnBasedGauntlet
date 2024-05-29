using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damaging Move", menuName = "Moves/Damaging Move")]
public class DamagingMoves : Moves
{
    public enum Debuff{
        Attack,
        Defense,
        Speed,
        Magic,
        Resistance,
        Skill,
        Efficiency,
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

    public enum DamagingType{
        Damage,
        Drain
    }

    public enum AttackType
    {
        Physical,
        Magical
    }

    void Awake(){
        MovesType = MoveType.Damaging;
    }

    [SerializeField] float movePower;
    [SerializeField] Debuff[] debuffs;
    [SerializeField] StatusType[] statusEffects;
    [SerializeField] AttackType atkType;
    [SerializeField] DamagingType dmgType; 

    public float MovePower { get { return movePower; } }
    public Debuff[] Debuffs { get { return debuffs; } }
    public StatusType[] StatusEffects { get { return statusEffects; } }
    public AttackType AtkType { get { return atkType; } }
    public DamagingType DmgType { get { return dmgType; } }

}
