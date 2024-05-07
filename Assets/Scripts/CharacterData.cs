using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/New CharacterData")]
public class CharacterData : ScriptableObject
{
    public Movesets moveset;

    public StatsList charaStatList;
}
