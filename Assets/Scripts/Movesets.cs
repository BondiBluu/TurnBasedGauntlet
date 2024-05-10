using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Moveset", menuName = "Moves/New Moveset")]
public class Movesets : ScriptableObject
{
    [SerializeField] List<Moves> moves = new List<Moves>();

    public List<Moves> Moves { get { return moves; } }
}
