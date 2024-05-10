using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//equipable weapons that boost attack or magic
public class WeaponObject : ItemObject
{
    public enum StatusType{
        Freeze,
        Burn,
        Poison,
        Cure,
        Still,
        None
    }

    public enum WeaponType{
        Normal,
        Exclusive,
        Joke
    }

    [SerializeField] float atkBoost;
    [SerializeField] float magBoost;
    [SerializeField] StatusType[] statusEffects;
    [SerializeField] WeaponType weapon;

    //automatically classified as a weapon whenever object is loaded
    void Awake(){
        Type = ItemType.Weapon;
    }
    
    public float AtkBoost { get { return atkBoost; } }
    public float MagBoost { get { return magBoost; } }
    public StatusType[] StatusEffects { get { return statusEffects; } }
    public WeaponType Weapon { get { return weapon; } set { weapon = value; }}
}
