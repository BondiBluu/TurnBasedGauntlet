using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    public GameObject partyPanel;
    public GameObject shopPanel;
    public GameObject itemPanel;
    public GameObject nextBattle;

    void Start()
    {
        CloseAll();
    }

    public void OpenParty(){
        partyPanel.SetActive(true);
        shopPanel.SetActive(false);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
    }

    public void CloseAll(){
        partyPanel.SetActive(false);
        shopPanel.SetActive(false);
        itemPanel.SetActive(false);
        nextBattle.SetActive(false);
    }
}
