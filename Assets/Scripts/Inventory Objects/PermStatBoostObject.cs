using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//in inventory. permanently raises one or more character stats
[CreateAssetMenu(fileName = "New PermStatBoost", menuName = "Items/Permanent Stat Boost")]
public class PermStatBoostObject : ItemObject
{
    [SerializeField] float hpBoost;
    [SerializeField] float mpBoost;
    [SerializeField] float atkBoost;
    [SerializeField] float defBoost;
    [SerializeField] float magBoost;
    [SerializeField] float resBoost;
    [SerializeField] float skllBoost;
    [SerializeField] float effBoost;
    [SerializeField] float spdBoost;

    public void Awake()
    {
        Type = ItemType.PermStatBoost;
    } 

    public float HpBoost { get => hpBoost; set => hpBoost = value; }
    public float MpBoost { get => mpBoost; set => mpBoost = value; }
    public float AtkBoost { get => atkBoost; set => atkBoost = value; }
    public float DefBoost { get => defBoost; set => defBoost = value; }
    public float MagBoost { get => magBoost; set => magBoost = value; }
    public float ResBoost { get => resBoost; set => resBoost = value; }
    public float SkllBoost { get => skllBoost; set => skllBoost = value; }
    public float EffBoost { get => effBoost; set => effBoost = value; }
    public float SpdBoost { get => spdBoost; set => spdBoost = value; }
    
}
