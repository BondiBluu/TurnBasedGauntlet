using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/New CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] Movesets moveset;
    [SerializeField] StatsList charaStatList;
    [SerializeField] AnimationClip[] charaAnimations;
    [SerializeField] Sprite charaSprite;
    public Movesets Moveset { get { return moveset; } }
    public StatsList CharaStatList { get { return charaStatList; } }
    public AnimationClip[] CharaAnimations { get { return charaAnimations; } }
    public Sprite CharaSprite { get { return charaSprite; } }

    //character images willl be added here
}
