using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObject : ItemObject
{
    public enum DefaultType{
        Default,
        KeyItem
        }

        public void Awake(){
            Type = ItemType.Default;
        }
}
