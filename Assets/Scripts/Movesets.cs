using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Moves", menuName = "Moves/New Moveset")]
public class Movesets : ScriptableObject
{
    [SerializeField] List<Moves> moves = new List<Moves>();
}
