using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//can fit into slots and raise character stats for as long as it is equipped 
public class EquipmentObject : ItemObject
{
    public enum EquipType
    {
        Normal,
        Exclusive,
        Joke
    }
    
    [SerializeField] float hpBoost;
    [SerializeField] float mpBoost;
    [SerializeField] float atkBoost;
    [SerializeField] float defBoost;
    [SerializeField] float magBoost;
    [SerializeField] float resBoost;
    [SerializeField] float skllBoost;
    [SerializeField] float effBoost;
    [SerializeField] float spdBoost;
    [SerializeField] EquipType equipmentType;

    public float HpBoost { get => hpBoost; set => hpBoost = value; }
    public float MpBoost { get => mpBoost; set => mpBoost = value; }
    public float AtkBoost { get => atkBoost; set => atkBoost = value; }
    public float DefBoost { get => defBoost; set => defBoost = value; }
    public float MagBoost { get => magBoost; set => magBoost = value; }
    public float ResBoost { get => resBoost; set => resBoost = value; }
    public float SkllBoost { get => skllBoost; set => skllBoost = value; }
    public float EffBoost { get => effBoost; set => effBoost = value; }
    public float SpdBoost { get => spdBoost; set => spdBoost = value; }
    public EquipType EquipmentType { get => equipmentType; set => equipmentType = value; }
}
