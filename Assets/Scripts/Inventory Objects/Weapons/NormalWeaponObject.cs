using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Normal Weapon", menuName = "Items/Weapons/Normal Weapon")]
public class NormalWeaponObject : WeaponObject
{
    public void Awake()
    {
        Type = ItemType.Weapon;
        Weapon = WeaponType.Normal;
    }
}
