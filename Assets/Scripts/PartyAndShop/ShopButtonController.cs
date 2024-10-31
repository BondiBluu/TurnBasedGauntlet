using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    public GameObject partyPanel;
    public GameObject shopPanel;
    public GameObject itemPanel;
    public GameObject nextBattle;
    public GameObject statsPanel;
    public Button partyButton;
    PartyStatsManager partyStatsManager;


    void Start()
    {
        partyStatsManager = FindObjectOfType<PartyStatsManager>();
        partyButton.Select();
        CloseAll();
    }

    public void OpenParty(){
        partyPanel.SetActive(true);
        shopPanel.SetActive(false);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
        statsPanel.SetActive(false);
        partyStatsManager.SetButtonsInactive();
    }

    public void OpenStats(CharacterTemplate character){
        shopPanel.SetActive(false);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
        statsPanel.SetActive(true);
        Debug.Log(character.characterData.CharaStatList.CharacterName);
        partyStatsManager.SetCharacterStats(character);
    }

    public void OpenShop(){
        partyPanel.SetActive(false);
        shopPanel.SetActive(true);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void OpenItem(){
        partyPanel.SetActive(false);
        shopPanel.SetActive(false);
        itemPanel.SetActive(true);
        nextBattle.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void CloseStatsPanel(){
        statsPanel.SetActive(false);
    }

    public void CloseAll(){
        partyPanel.SetActive(false);
        shopPanel.SetActive(false);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
        statsPanel.SetActive(false);
    }
}
