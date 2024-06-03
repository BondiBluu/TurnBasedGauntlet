using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//in battle inventory in battle. restores hp, mp, or both
[CreateAssetMenu(fileName = "New Restoration", menuName = "Items/Restorative")]
public class RestorativeObject : ItemObject
{
    public enum HealDebuff
    {
        Attack,
        Defense,
        Speed,
        Magic,
        Resistance,
        Skill,
        Efficiency
    }

    public enum StatusCure{
        Freeze,
        Burn,
        Poison,
        Still,
        Downed
    }

    [SerializeField] float hpRestore;
    [SerializeField] float mpRestore;
    [SerializeField] HealDebuff[] healDebuffs;
    [SerializeField] StatusCure[] statusCures;

    public void Awake()
    {
        Type = ItemType.Restorative;
    }

    public float HpRestore { get { return hpRestore; } }
    public float MpRestore { get { return mpRestore; } }
    
    public HealDebuff[] HealDebuffs { get { return healDebuffs; } }
    public StatusCure[] StatusCures { get { return statusCures; } }
    }
