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

    public float HPBoost { get { return hpBoost; } }
    public float MPBoost { get { return mpBoost; } }
    public float AtkBoost { get { return atkBoost; } }
    public float DefBoost { get { return defBoost; } }
    public float MagBoost { get { return magBoost; } }
    public float ResBoost { get { return resBoost; } }
    public float SkllBoost { get { return skllBoost; } }
    public float EffBoost { get { return effBoost; } }
    public float SpdBoost { get { return spdBoost; } }
    public EquipType EquipmentType { get { return equipmentType; } set { equipmentType = value; }}
}
