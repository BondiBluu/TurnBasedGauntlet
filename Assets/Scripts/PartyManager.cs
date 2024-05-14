using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] List<CharacterTemplate> currentParty = new List<CharacterTemplate>();
    [SerializeField] List<CharacterTemplate> partyInventory = new List<CharacterTemplate>();
    [SerializeField] List<CharacterData> characterRoster = new List<CharacterData>();

    //if a character from the roster gets added to the party, give it a charactera template and add it to the party inventory

    //if a character from the party inventory gets added to the current party, remove it from the party inventory and add it to the current party

    //if a character from the current party gets removed, destroy the game object and remove it from the current party- add it to the party inventory 
    
}
