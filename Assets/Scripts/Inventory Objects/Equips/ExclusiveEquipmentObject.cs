using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when equipped to a character whose name matches characterName, increases their best stat
[CreateAssetMenu(fileName = "New Exclusive Equipment", menuName = "Items/Equipment/Exclusive Equipment")]
public class ExclusiveEquipmentObject : EquipmentObject
{
    [SerializeField] string characterName;

    public void Awake()
    {
        Type = ItemType.Equipment;
        EquipmentType = EquipType.Exclusive;
    }

    public string CharacterName { get => characterName; set => characterName = value; }
}
