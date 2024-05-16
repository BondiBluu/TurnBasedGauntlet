using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject atkPanel;
    public GameObject itemPanel;
    public GameObject statsPanel;
    public GameObject logPanel;
    public GameObject quitPanel;
    public GameObject allyPanel;
    public GameObject enemyPanel;
    public GameObject blockerPanel;
    [Header("Buttons")]
    public Button atkButton;
    public Button itemButton;
    public Button statsButton;
    public Button logButton;
    public Button quitButton;

    public void OnAttackPanel(){
        atkPanel.SetActive(true);
        blockerPanel.SetActive(true);

        itemPanel.SetActive(false);
        statsPanel.SetActive(false);
        logPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void OnItemPanel(){
        itemPanel.SetActive(true);
        blockerPanel.SetActive(true);

        atkPanel.SetActive(false);
        statsPanel.SetActive(false);
        logPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void OnStatsPanel(){
        statsPanel.SetActive(true);
        blockerPanel.SetActive(true);

        atkPanel.SetActive(false);
        itemPanel.SetActive(false);
        logPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void OnLogPanel(){
        logPanel.SetActive(true);
        blockerPanel.SetActive(true);

        atkPanel.SetActive(false);
        itemPanel.SetActive(false);
        statsPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void OnQuitPanel(){
        quitPanel.SetActive(true);
        blockerPanel.SetActive(true);

        atkPanel.SetActive(false);
        itemPanel.SetActive(false);
        statsPanel.SetActive(false);
        logPanel.SetActive(false);
    }

    public void OnAllyPanel(){
        allyPanel.SetActive(true);
        blockerPanel.SetActive(true);

        enemyPanel.SetActive(false);
    }

    public void OnEnemyPanel(){
        enemyPanel.SetActive(true);
        blockerPanel.SetActive(true);

        allyPanel.SetActive(false);
    }

    public void OnTurnOffPanels(){
        atkPanel.SetActive(false);
        itemPanel.SetActive(false);
        statsPanel.SetActive(false);
        logPanel.SetActive(false);
        quitPanel.SetActive(false);
        allyPanel.SetActive(false);
        enemyPanel.SetActive(false);
        blockerPanel.SetActive(false);
    }
}
