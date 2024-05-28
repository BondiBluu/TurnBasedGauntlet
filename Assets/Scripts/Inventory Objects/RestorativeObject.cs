using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//in battle inventory in battle. restores hp, mp, or both
[CreateAssetMenu(fileName = "New Restoration", menuName = "Items/Restorative")]
public class RestorativeObject : ItemObject
{
    public enum DebuffCure{
        Attack,
        Defense,
        Speed,
        Magic,
        Resistance,
        Skill,
        Efficiency,
        None
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
    [SerializeField] DebuffCure[] debuffCures;
    [SerializeField] StatusCure[] statusCures;

    public void Awake()
    {
        Type = ItemType.Restorative;
    }

    public float HpRestore { get { return hpRestore; } }
    public float MpRestore { get { return mpRestore; } }
    public DebuffCure[] DebuffCures { get { return debuffCures; } }
    public StatusCure[] StatusCures { get { return statusCures; } }
    }
