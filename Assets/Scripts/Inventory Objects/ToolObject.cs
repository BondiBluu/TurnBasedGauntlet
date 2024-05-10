using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//in inventory in battle. Used to damage enemies and give status effects
[CreateAssetMenu(fileName = "New Tool", menuName = "Items/Tool")]
public class ToolObject : ItemObject
{
     public enum StatusType{
        Freeze,
        Burn,
        Poison,
        Cure,
        Still,
        None
    }

    [SerializeField] float atkPwr;
    [SerializeField] float magPwr;
    [SerializeField] StatusType[] statusEffects;

    public void Awake()
    {
        Type = ItemType.Tool;
    }

    public float AtkPwr { get { return atkPwr; } }
    public float MagPwr { get { return magPwr; } }
    public StatusType[] StatusEffects { get { return statusEffects; } }
}
