using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Exclusive Weapon", menuName = "Items/Weapons/Exclusive Weapon")]
public class ExclusiveWeaponObject : WeaponObject
{
     [SerializeField] string characterName;

    public void Awake()
    {
        Type = ItemType.Equipment;
        Weapon = WeaponType.Exclusive;
    }

    public string CharacterName { get => characterName; set => characterName = value; }

}
