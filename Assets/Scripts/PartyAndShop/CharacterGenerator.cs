using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public GameObject partyCharacterPrefab;
    public GameObject invenCharacterPrefab;
    public Transform partyContainer;
    public Transform invenContainer;

    PartyManager partyManager;

    public void Start()
    {
        partyManager = FindObjectOfType<PartyManager>();
    }
    public void GenerateParty()
    {
        Debug.Log("Generating Party");
        //generate all 4 party members from the party manager
    }
}
