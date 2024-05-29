using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Move", menuName = "Moves/Healing Move")]
public class HealingMoves : Moves
{
    public enum HealType
    {
        Heal,
        Revive
    }

    public enum HealStatus
    {
        None,
        Poison,
        Burn,
        Freeze,
        Still
    }

    public enum HealDebuff
    {
        None,
        Attack,
        Defense,
        Speed,
        Magic,
        Resistance,
        Skill,
        Efficiency
    }

    [SerializeField] float healPower;
    //if the type is revive, takes off the Down status effect and heals the target
    [SerializeField] HealType healType;
    [SerializeField] HealStatus[] statusEffects;
    [SerializeField] HealDebuff[] debuffs;

    void Awake()
    {
        MovesType = MoveType.Healing;
    }

    public float HealPower { get { return healPower; } }
    public HealType HealTypes { get { return healType; } }
    public HealStatus[] StatusEffects { get { return statusEffects; } }
    public HealDebuff[] Debuffs { get { return debuffs; } }
}
