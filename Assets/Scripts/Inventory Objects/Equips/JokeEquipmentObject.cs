using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when equipped to a character whose name matches characterName, increases their best stat by far, more than exclusive weapons
[CreateAssetMenu(fileName = "New Joke Equipment", menuName = "Items/Equipment/Joke Equipment")]
public class JokeEquipmentObject : EquipmentObject
{
    [SerializeField] string characterName;
    [SerializeField] string trueDesc;
    
    public void Awake()
    {
        Type = ItemType.Equipment;
        EquipmentType = EquipType.Joke;
    }
  
    public string CharacterName { get { return characterName; } }
    public string TrueDesc { get { return trueDesc; } }
}