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

    [SerializeField] CharacterTemplate savedCharacter;
    
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
        Setup(partyManager.currentParty.Count, playerBattleStations, partyManager.currentParty);
        uiManager.InstantiateCharacterPanels(partyManager.currentParty);
        uiManager.InstantiateAllyPanels(partyManager.currentParty);
        
        yield return new WaitForSeconds(.01f);
        currentState = BattleState.EnemySetup;
        StartCoroutine(EnemySetup());
    }

    //instantiating the enemy characters
    public IEnumerator EnemySetup()
    {
        List <CharacterTemplate> enemyMembers = partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers;
        Setup(enemyMembers.Count, enemyBattleStations, enemyMembers);
        uiManager.InstantiateEnemyPanels(enemyMembers);

        yield return new WaitForSeconds(1f);
        currentState = BattleState.PlayerTurn;
        StartCoroutine(PlayerTurn());
    }

    public IEnumerator PlayerTurn()
    {
        buttonController.atkButton.Select();
        Debug.Log("Battle Start. Player Turn");

        int currentCharacter = 0;

        for(int i = currentCharacter; i < partyManager.currentParty.Count; i++)
        {
            //if the character is downed, skip to the next character
            // if(partyManager.currentParty[i].currentHP > 0)
            // {
            //     currentCharacter = i;
            //     savedCharacter = partyManager.currentParty[currentCharacter];
            // } 
            // else {
            //     continue;
            // }
            
            currentCharacter = i;
            savedCharacter = partyManager.currentParty[currentCharacter];

           //wait until the player has selected a move, and a target
           yield return new WaitUntil(() => currentCharacter == 2);
           
        }
        yield return new WaitForSeconds(1f);
    }

    public void Setup(int groupCount, Transform[] battleStations, List <CharacterTemplate> charaTemplate){
        
        for(int i = 0; i < groupCount; i++)
        {
            GameObject charaObject = Instantiate(characterPrefab, battleStations[i]);

            CharacterHolder charaHolder = charaObject.GetComponent<CharacterHolder>();
            charaHolder.characterTemplate = charaTemplate[i];
            
            SpriteRenderer spriteRenderer = charaObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = charaTemplate[i].characterData.CharaSprite;
        }
    }

    //set up move generation for current character    
    public void OnChosenAttackButton(){
        generator.GenerateMoves(savedCharacter);
    }
}