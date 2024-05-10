using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Group", menuName = "Groups/New Groups")]
public class Groups : ScriptableObject
{
    //drops rewards if enemy group
    public enum GroupType
    {
        Party,
        Enemy
    }

    [SerializeField] List<CharacterData> groupMembers = new List<CharacterData>();
    [SerializeField] GroupType groupTyping;
    [SerializeField] int currency;
    [SerializeField] int exp;

    public List<CharacterData> GroupMembers { get { return groupMembers; } }
    public GroupType GroupTyping { get { return groupTyping; } }
    public int Currency { get { return currency; } }
    public int Exp { get { return exp; } }
}
