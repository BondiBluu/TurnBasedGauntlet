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

    BattleState currentState;
    PartyManager partyManager;
    UIManager uiManager;
    Generator generator;
    ButtonController buttonController;
    DamageCalculations damageCalc;
    AttackSaver attackSaver;
    public GameObject characterPrefab;
    public Transform[] playerBattleStations;
    public Transform[] enemyBattleStations;
    [SerializeField] int currentSeed;
    [SerializeField] int currentGroupSet;
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
        for(int i = 0; i < partyManager.currentParty.Count; i++)
        {
            GameObject charaObject = Instantiate(characterPrefab, playerBattleStations[i]);

            CharacterHolder charaHolder = charaObject.GetComponent<CharacterHolder>();
            charaHolder.characterTemplate = partyManager.currentParty[i];
            
            SpriteRenderer spriteRenderer = charaObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = partyManager.currentParty[i].characterData.CharaSprite;
        }

        yield return new WaitForSeconds(.01f);
        currentState = BattleState.EnemySetup;
        StartCoroutine(EnemySetup());
    }

    //instantiating the enemy characters
    public IEnumerator EnemySetup()
    {
        for(int i = 0; i < partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers.Count; i++)
        {
            GameObject charaObject = Instantiate(characterPrefab, enemyBattleStations[i]);

            CharacterHolder charaHolder = charaObject.GetComponent<CharacterHolder>();
            charaHolder.characterTemplate = partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers[i];
            
            SpriteRenderer spriteRenderer = charaObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers[i].characterData.CharaSprite;
        }

        yield return new WaitForSeconds(1f);
        currentState = BattleState.PlayerTurn;
        StartCoroutine(PlayerTurn());
    }

    public IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(1f);
    }
}