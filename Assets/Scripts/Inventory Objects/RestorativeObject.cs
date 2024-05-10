using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//in battle inventory in battle. restores hp, mp, or both
[CreateAssetMenu(fileName = "New Restoration", menuName = "Items/Restorative")]
public class RestorativeObject : ItemObject
{
    [SerializeField] float hpRestore;
    [SerializeField] float mpRestore;

    public void Awake()
    {
        Type = ItemType.Restorative;
    }

    public float HpRestore { get { return hpRestore; } }
    public float MpRestore { get { return mpRestore; } }
}
