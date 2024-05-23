using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] CharacterTemplate selectedTarget;
    [SerializeField] bool targetSelected = false;
    
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
        //disabling all buttons
        buttonController.DisableButtons();
        buttonController.HideUndoButton();
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
        StartCoroutine(PlayerTurn(0));
    }

    public IEnumerator PlayerTurn(int startingCycle)
    {
        buttonController.EnableButtons();
        buttonController.atkButton.Select();
        Debug.Log("Battle Start. Player Turn");


        for(int i = startingCycle; i < partyManager.currentParty.Count; i++)
        {
            buttonController.atkButton.Select();

            if(i > 0)
            {
             buttonController.ShowUndoButton();
            } else
            {
                buttonController.HideUndoButton();
            }

            //if the character is downed, skip to the next character
            if(savedCharacter.characterStatus == CharacterTemplate.CharacterStatus.Downed)
            {
                continue;
            }
            
            int currentCharacter = i;
            savedCharacter = partyManager.currentParty[currentCharacter];
            Debug.Log("Current Character: " + savedCharacter.characterData.CharaStatList.CharacterName + "'s turn.");   

            yield return new WaitUntil(() => targetSelected == true);

            //reset the selected target, move, and item
            generator.selectedMove = null;
            generator.selectedItem = null;          
            selectedTarget = null;       
            targetSelected = false;
        }
        yield return new WaitForSeconds(1f);
        Debug.Log("Player Turn End");
        currentState = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy Turn");
        buttonController.DisableButtons();
        buttonController.HideUndoButton();

        int enemyCount = partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers.Count;

        for(int i = 0; i < enemyCount; i++)
        {
            //if the character is downed, skip to the next character
            if(savedCharacter.characterStatus == CharacterTemplate.CharacterStatus.Downed)
            {
                continue;

            } 

            savedCharacter = partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers[i];
                Debug.Log("Current Character: " + savedCharacter.characterData.CharaStatList.CharacterName + "'s turn.");

                //randomly select a target from the player party
                int targetIndex = Random.Range(0, partyManager.currentParty.Count);

                //TODO: if the target is downed, skip to the next target
                
                selectedTarget = partyManager.currentParty[targetIndex];

                //chosing a random move from the enemy's move list
                int moveIndex = Random.Range(0, savedCharacter.characterData.Moveset.Moves.Count);
                Moves selectedMove = savedCharacter.characterData.Moveset.Moves[moveIndex];

                //add the move to the attack saver
                attackSaver.SaveMove(savedCharacter, selectedMove, selectedTarget);
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Enemy Turn End");
        currentState = BattleState.BattleCalc;
        StartCoroutine(BattleCalculations());
    }

    public IEnumerator BattleCalculations(){
        Debug.Log("Battle Calculations");

        //sort actions by speed
        List<AttackSaver.SaveActions> sortedActions = attackSaver.characterActionsContainer.OrderByDescending(x => x.user.currentSpeed).ToList();

        //grabbing users who used healing items
        List<AttackSaver.SaveActions> healingActions = new List<AttackSaver.SaveActions>();

        foreach(AttackSaver.SaveActions action in sortedActions)
        {
            if(action.item != null && action.item.Type == ItemObject.ItemType.Restorative)
            {
                healingActions.Add(action);
                sortedActions.Remove(action);
            }
        }

        //putting healing actions back into the top of the sorted actions list
        foreach(AttackSaver.SaveActions action in healingActions)
        {
            sortedActions.Insert(0, action);
        }

        //performing the actions
        foreach(AttackSaver.SaveActions action in sortedActions)
        {
            int damage = 0;
            int healing = 0;
            int magic = 0;
            string message = "";
            if(action.move != null)
            {
                switch(action.move.MovesType){
                    case Moves.MoveType.Damaging:
                    damage = damageCalc.CalculateDamagingMove(action.user, action.move, action.target);
                    break;
                    case Moves.MoveType.Drain:
                    (damage, healing) = damageCalc.CalculateDrainMove(action.user, action.move, action.target);
                    break;
                    case Moves.MoveType.Healing:
                    healing = damageCalc.CalcHealingMove(action.user, action.move, action.target);
                    break;
                    case Moves.MoveType.Supplementary:
                    message = damageCalc.DescribeBuffsAndDebuffs(action.user, action.move, action.target);
                    break;
                }
            }
            else if(action.item != null)
            {
                switch(action.item.Type){
                    case ItemObject.ItemType.Tool:
                    damage = damageCalc.CalcTool(action.user, action.item, action.target);
                    break;
                    case ItemObject.ItemType.Restorative:
                    (healing, magic) = damageCalc.CalcPotions(action.user, action.item, action.target);
                    break;
                }
            }
        }

        //clear the actions for the next turn
        attackSaver.characterActionsContainer.Clear();
        healingActions.Clear();
        sortedActions.Clear();
        
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

    public void OnUndoButton(){
        int lastAction = attackSaver.characterActionsContainer.Count - 1;
        attackSaver.characterActionsContainer.RemoveAt(attackSaver.characterActionsContainer.Count - 1);

        //making sure the target is unselected so wait until still works
        targetSelected = false;
        Debug.Log($"Last action undone. {attackSaver.characterActionsContainer.Count} actions have been saved.");
        StartCoroutine(PlayerTurn(lastAction));
    }

    public void OnAllyClick(int index){
        selectedTarget = partyManager.currentParty[index];
        Debug.Log("Ally selected: " + selectedTarget.characterData.CharaStatList.CharacterName);
        if(generator.selectedMove != null){
            attackSaver.SaveMove(savedCharacter, generator.selectedMove, selectedTarget);
            targetSelected = true;
        }
        else if(generator.selectedItem != null){
            attackSaver.SaveItem(savedCharacter, generator.selectedItem, selectedTarget);
            targetSelected = true;
        }
    }

    public void OnEnemyClick(int index){
        selectedTarget = partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers[index];
        Debug.Log("Enemy selected: " + selectedTarget.characterData.CharaStatList.CharacterName);
        if(generator.selectedMove != null){
            attackSaver.SaveMove(savedCharacter, generator.selectedMove, selectedTarget);
            targetSelected = true;
        }
        else if(generator.selectedItem != null){
            attackSaver.SaveItem(savedCharacter, generator.selectedItem, selectedTarget);
            targetSelected = true;
        }
    }

    //set up move generation for current character. DO NOT REMOVE- used for attack button
    public void OnChosenAttackButton(){
        generator.GenerateMoves(savedCharacter);
    }
}