using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Group", menuName = "Groups/New Groups")]
public class Groups : ScriptableObject
{

    [SerializeField] List<CharacterTemplate> groupMembers = new List<CharacterTemplate>();
    [SerializeField] int currency;
    [SerializeField] int exp;

    public List<CharacterTemplate> GroupMembers { get { return groupMembers; } }
    public int Currency { get { return currency; } }
    public int Exp { get { return exp; } }
}
