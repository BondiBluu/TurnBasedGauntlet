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

    public float HPBoost { get { return hpBoost; } }
    public float MPBoost { get { return mpBoost; } }
    public float AtkBoost { get { return atkBoost; } }
    public float DefBoost { get { return defBoost; } }
    public float MagBoost { get { return magBoost; } }
    public float ResBoost { get { return resBoost; } }
    public float SkllBoost { get { return skllBoost; } }
    public float EffBoost { get { return effBoost; } }
    public float SpdBoost { get { return spdBoost; } } 
}
