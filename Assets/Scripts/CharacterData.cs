using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/New CharacterData")]
public class CharacterData : ScriptableObject
{
    public enum CharacterStatus
    {
        Normal,
        Freeze,
        Burn,
        Poison,
        Still,
        Downed
    }
    public Movesets moveset;

    public StatsList charaStatList;

    public AnimationClip[] charaAnimations;
    public SpriteRenderer charaSprite;

    //character images willl be added here
}
