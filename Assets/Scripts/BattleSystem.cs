using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState {
        PlayerSetup,
        EnemySetup,
        PlayerTurn,
        EnemyTurn,
        BattleCalc,
        Won,
        Lost,
        Checkpoint    
    }

    public BattleState currentState;
    PartyManager partyManager;
    UIManager uiManager;
    Generator generator;
    ButtonController buttonController;
    DamageCalculations damageCalc;
    AttackSaver attackSaver;
    CharacterHolder holder;

    // Start is called before the first frame update
    void Start()
    {
        partyManager = FindObjectOfType<PartyManager>();
        uiManager = FindObjectOfType<UIManager>();
        generator = FindObjectOfType<Generator>();
        buttonController = FindObjectOfType<ButtonController>();
        damageCalc = FindObjectOfType<DamageCalculations>();
        attackSaver = FindObjectOfType<AttackSaver>();
        currentState = BattleState.PlayerSetup;
        StartCoroutine(PlayerSetup());
    }

    //instantiating the player characters
    public IEnumerator PlayerSetup()
    {
        yield return new WaitForSeconds(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
