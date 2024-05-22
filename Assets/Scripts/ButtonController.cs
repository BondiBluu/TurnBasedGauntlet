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
    public Button undoButton;
    
    UIManager uiManager;
    private void Start() {
        uiManager = FindObjectOfType<UIManager>();
    }

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
        uiManager.SelectFirstMove(uiManager.allyButtons);

        enemyPanel.SetActive(false);
    }

    public void OnEnemyPanel(){
        enemyPanel.SetActive(true);
        blockerPanel.SetActive(true);
        uiManager.SelectFirstMove(uiManager.enemyButtons);

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

    //disable all buttons
    public void DisableButtons(){
        atkButton.interactable = false;
        itemButton.interactable = false;
        statsButton.interactable = false;
        logButton.interactable = false;
        quitButton.interactable = false;
    }

    //enable all buttons
    public void EnableButtons(){
        atkButton.interactable = true;
        itemButton.interactable = true;
        statsButton.interactable = true;
        logButton.interactable = true;
        quitButton.interactable = true;
    }

    public void ShowUndoButton(){
        undoButton.gameObject.SetActive(true);
    }

    public void HideUndoButton(){
        undoButton.gameObject.SetActive(false);
    }
}
