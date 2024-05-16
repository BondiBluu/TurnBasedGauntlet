using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Groups/Seed")]
public class Seed : ScriptableObject
{
    [SerializeField] List<Groups> groupSet = new List<Groups>();

    public List<Groups> GroupSet { get { return groupSet; } }
}
