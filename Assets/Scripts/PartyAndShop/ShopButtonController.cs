using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    public GameObject partyPanel;
    public GameObject shopPanel;
    public GameObject itemPanel;
    public GameObject nextBattle;
    public GameObject statsPanel;


    void Start()
    {
        CloseAll();
    }

    public void OpenParty(){
        partyPanel.SetActive(true);
        shopPanel.SetActive(false);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void OpenStats(CharacterTemplate character){
        shopPanel.SetActive(false);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
        statsPanel.SetActive(true);
        Debug.Log(character.characterData.CharaStatList.CharacterName);
    }

    public void CloseAll(){
        partyPanel.SetActive(false);
        shopPanel.SetActive(false);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
        statsPanel.SetActive(false);
    }
}
