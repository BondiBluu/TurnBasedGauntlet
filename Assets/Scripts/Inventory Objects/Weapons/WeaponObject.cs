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
    
    public float AtkBoost { get => atkBoost; set => atkBoost = value; }
    public float MagBoost { get => magBoost; set => magBoost = value; }
    public StatusType[] StatusEffects { get => statusEffects; set => statusEffects = value; }
    public WeaponType Weapon { get => weapon; set => weapon = value; }
}
