using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public GameObject partyCharacterPrefab;
    public GameObject invenCharacterPrefab;
    public Transform partyContainer;
    public Transform invenContainer;

    public void GenerateParty(List<CharacterTemplate> party)
    {
        foreach (Transform character in partyContainer)
        {
            Destroy(character.gameObject);
        }

        foreach (CharacterTemplate character in party)
        {
            GameObject newCharacter = Instantiate(partyCharacterPrefab, partyContainer);
        }
    }
}
