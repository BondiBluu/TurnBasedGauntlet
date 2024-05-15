using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Groups/Seed")]
public class Seed : ScriptableObject
{
    [SerializeField] List<Groups> groupMembers = new List<Groups>();

}
