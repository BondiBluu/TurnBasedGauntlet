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
    LevelUpRolls levelUpRolls;
    public GameObject characterPrefab;
    public Transform[] playerBattleStations;
    public Transform[] enemyBattleStations;
    [SerializeField] int currentSeed;
    [SerializeField] int currentGroupSet;
    [SerializeField] CharacterTemplate savedCharacter;
    [SerializeField] int savedCharacterIndex; //for stats
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
        levelUpRolls = FindObjectOfType<LevelUpRolls>();
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
        uiManager.InstantiateEnemyHealthBars(enemyMembers);

        yield return new WaitForSeconds(1f);
        currentState = BattleState.PlayerTurn;
        StartCoroutine(PlayerTurn(0));
    }

    public void Setup(int groupCount, Transform[] battleStations, List <CharacterTemplate> charaTemplate){

        uiManager.DisplayCombatText("Battle Start!");
        
        for(int i = 0; i < groupCount; i++)
        {
            GameObject charaObject = Instantiate(characterPrefab, battleStations[i]);

            CharacterHolder charaHolder = charaObject.GetComponent<CharacterHolder>();
            charaHolder.characterTemplate = charaTemplate[i];
            
            SpriteRenderer spriteRenderer = charaObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = charaTemplate[i].characterData.CharaSprite;

            //reverting the character's stats back to the base stats
            charaTemplate[i].RevertStats();
            //setting the character's base stats
            charaTemplate[i].SetBaseStats();
            //adding the character's equipment stats
            charaTemplate[i].EquipmentStats();
            //reverting the character's status back to normal
            charaTemplate[i].characterStatus = CharacterTemplate.CharacterStatus.Normal;
        }
    }


    public IEnumerator PlayerTurn(int startingCycle)
    {
        buttonController.EnableButtons();
        buttonController.atkButton.Select();
        Debug.Log("Battle Start!");


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

            //grabbing the current character for the stats list
            savedCharacterIndex = i;
            savedCharacter = partyManager.currentParty[i];

            //if the character is downed, skip to the next character
            if(savedCharacter.characterStatus == CharacterTemplate.CharacterStatus.Downed)
            {
                continue;
            }

            string currentCharacter = savedCharacter.characterData.CharaStatList.CharacterName;
            
            
            Debug.Log("Current Character: " + currentCharacter + "'s turn."); 
            uiManager.DisplayCombatText($"{currentCharacter}'s turn.");  

            yield return new WaitUntil(() => targetSelected == true);

            //reset the selected target, move, and item
            generator.selectedMove = null;
            generator.selectedItem = null;          
            selectedTarget = null;       
            targetSelected = false;
        }
        buttonController.DisableButtons();
        buttonController.HideUndoButton();
        yield return new WaitForSeconds(1f);
        Debug.Log("Player Turn End");
        currentState = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy Turn");
        uiManager.DisplayCombatText("Enemy Turn!");

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

        //grabbing users who used healing items and grabbing users who do everything else
        List<AttackSaver.SaveActions> healingActions = new List<AttackSaver.SaveActions>();
        List<AttackSaver.SaveActions> everyOtherAction = new List<AttackSaver.SaveActions>();

        foreach(AttackSaver.SaveActions action in sortedActions)
        {
            if(action.item != null && action.item.Type == ItemObject.ItemType.Restorative)
            {
                healingActions.Add(action);
            } else{
                everyOtherAction.Add(action);
            }
        }

        sortedActions.Clear();
        sortedActions.AddRange(healingActions);
        sortedActions.AddRange(everyOtherAction);

        //performing the actions
        foreach(AttackSaver.SaveActions action in sortedActions)
        {
            //checks if the user is downed in the middle of the battle
            if(action.user.characterStatus == CharacterTemplate.CharacterStatus.Downed)
            {
                Debug.Log($"{action.user.characterData.CharaStatList.CharacterName} is downed! They can't perform an action!");
                continue;
            }

            if(action.move != null)
            {
                switch(action.move.MovesType){
                    case Moves.MoveType.Damaging:
                    yield return StartCoroutine(damageCalc.OnAttack(action.user, action.move, action.target));
                    break;
                    case Moves.MoveType.Healing:
                    yield return StartCoroutine(damageCalc.OnHeal(action.user, action.move, action.target));
                    break;
                    case Moves.MoveType.Supplementary:
                    yield return StartCoroutine(damageCalc.OnStatus(action.user, action.move, action.target));
                    break;
                }
            }
            else if(action.item != null)
            {
                switch(action.item.Type){
                    case ItemObject.ItemType.Restorative:
                    yield return StartCoroutine(damageCalc.OnPotion(action.user, action.item, action.target));
                    break;
                    case ItemObject.ItemType.Tool:
                    yield return StartCoroutine(damageCalc.OnAttack(action.user, action.item, action.target));
                    break;
                }
            }
        }

        //clear the actions for the next turn
        attackSaver.characterActionsContainer.Clear();
        healingActions.Clear();
        sortedActions.Clear();

        List<CharacterTemplate> enemyMembers = partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers;

        CheckDefeat(partyManager.currentParty, BattleState.Lost);
        CheckDefeat(enemyMembers, BattleState.Won);
        
        
        yield return new WaitForSeconds(1f);

        currentState = BattleState.PlayerTurn;
        StartCoroutine(PlayerTurn(0));
    }

    public void Win(){
        Debug.Log("Battle Won!");
         int exp = partyManager.seed[currentSeed].GroupSet[currentGroupSet].Exp;
         Debug.Log($"Party gained {exp} exp each!");
         //have the player gain experience
         foreach(CharacterTemplate character in partyManager.currentParty)
         {
             character.GainEXP(exp);
         }
         
        EventController.instance.QueueNextCharaLevelUp();

        //possibly put a wait until clause to stop the game from continuing until the level up panel is closed
        //need to make a method that attaches the level up event since coroutines can't be invoked
        //yield return new WaitForSeconds(1f);

         
        //have the player gain currency
        //if battle is won, go to the next battle. Battle increases by 1
        if(currentGroupSet >= partyManager.seed[currentSeed].GroupSet.Count - 1)
        {
            currentState = BattleState.Checkpoint;
            Debug.Log("Checkpoint reached, Battle State: " + currentState);
            //go to checkpoint, most likely will be invoked
            currentGroupSet = 0; //to be moved

        } else
        {
            currentGroupSet++;
            Debug.Log($"Battle {currentGroupSet} of seed {currentSeed} has been loaded. Team of {partyManager.seed[currentSeed].GroupSet[currentGroupSet].GroupMembers[0].characterData.CharaStatList.CharacterName} Here.");
            DeleteCurrentEnemies();
            StartCoroutine(EnemySetup());
        }

    }

    void DeleteCurrentEnemies(){
        foreach(Transform enemy in enemyBattleStations){
            if(enemy.childCount > 0){
                foreach(Transform child in enemy){
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void Lose(){
        Debug.Log("Battle Lost!");
        currentGroupSet = 0;
        Debug.Log($"Current battle: {currentGroupSet} of seed {currentSeed} has been loaded.");
        //game over
        //character stats and levels have to be tranfered back to what they were at the previous checkpoint
        // foreach(CharacterTemplate character in partyManager.currentParty)
        // {
        //     character.RevertStats();
        // }
        // EnemySetup();
        //whatever battle you were in goes to 0- start at the beginning of the checkpoint- so go the current seed
        //if the player loses, go back to the previous checkpoint to prepare
        //DeleteCurrentEnemies();
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

    //showing stats
    public void OnStatsButton(){
        uiManager.ShowStats(savedCharacter);
        //setting the saved character index to the current character
        savedCharacterIndex = partyManager.currentParty.IndexOf(savedCharacter);
    }

    //show next character's stats
    public void OnNextStatsButton(){
        savedCharacterIndex++;
        if(savedCharacterIndex >= partyManager.currentParty.Count){
            savedCharacterIndex = 0;
        }
        uiManager.ShowStats(partyManager.currentParty[savedCharacterIndex]);
    }

    //check if the list of characters have been defeated
    public void CheckDefeat(List<CharacterTemplate> characters, BattleState state){
        int defeatedCount = 0;



        foreach(CharacterTemplate character in characters)
        {
            if(character.characterStatus == CharacterTemplate.CharacterStatus.Downed)
            {
                defeatedCount++;
                Debug.Log(character.characterData.CharaStatList.CharacterName + " has been defeated!");
            }
        }


        if(defeatedCount == characters.Count)
        {
            currentState = state;
        }

        if(currentState == BattleState.Won)
        {
            //invoke win unity event
            Debug.Log(currentState);
            EventController.instance.OnWin.Invoke();
            
        }
        else if(currentState == BattleState.Lost)
        {
            Debug.Log(currentState);
            EventController.instance.OnLose.Invoke();
            //invoke lose unity event
        } 
    }

}