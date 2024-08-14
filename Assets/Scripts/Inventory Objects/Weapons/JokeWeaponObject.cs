using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Joke Weapon", menuName = "Items/Weapons/Joke Weapon")]
public class JokeWeaponObject : WeaponObject
{   
    [SerializeField] string characterName;
    [SerializeField] string trueDesc;
    
    public void Awake()
    {
        Type = ItemType.Weapon;
        Weapon = WeaponType.Joke;
    }

    public string CharacterName { get { return characterName; } }
    public string TrueDesc { get { return trueDesc; } }
}
