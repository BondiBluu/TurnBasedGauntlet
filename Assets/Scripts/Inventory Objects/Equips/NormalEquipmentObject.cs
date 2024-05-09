using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Normal Equipment", menuName = "Items/Equipment/Normal")]
public class NormalEquipmentObject : EquipmentObject
{
    public void Awake()
    {
        Type = ItemType.Equipment;
        EquipmentType = EquipType.Normal;
    }
  
}
