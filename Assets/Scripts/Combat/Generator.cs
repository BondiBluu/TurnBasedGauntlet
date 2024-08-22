using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject textPrefab;
    public Transform moveContainer;
    public Transform itemContainer;
    public Transform logContainer;
    public float buttonSpacing;
    public Moves selectedMove;
    public ItemObject selectedItem;
    public List<Moves> movesAlreadyAdded = new List<Moves>();
    public List<Button> navButtons = new List<Button>();
    public List<string> logList = new List<string>();
    ButtonController buttonCon;

    public InvenObject playerInven;
    public CharacterTemplate savedTarget;

    [Header("Move Hover")]
    MoveHover moveHover;
    public TMP_Text moveName;
    public TMP_Text moveCost;
    public TMP_Text movePower;
    public TMP_Text moveType;
    public TMP_Text moveExplanation;

    [Header("Item Hover")]
    MoveHover itemHover;
    public TMP_Text itemName;
    public TMP_Text itemCost;
    public TMP_Text itemPower;
    public TMP_Text itemType;
    public TMP_Text itemExplanation;


    public void Start(){
        buttonCon = FindObjectOfType<ButtonController>();
    }

    //takes the character unit’s characterData’s movelist 
    public void GenerateMoves(CharacterTemplate character){

        navButtons.Clear();
        movesAlreadyAdded.Clear();

        //destroy all the buttons in the move container before generating new ones
        foreach(Transform button in moveContainer){
            Destroy(button.gameObject);
        }

        float currentPosY = 0f;

        foreach(Moves move in character.characterData.Moveset.Moves){
            if(character.currentLevel >= move.LvlGotten && !movesAlreadyAdded.Contains(move)){
                movesAlreadyAdded.Add(move);
                
                GameObject button = Instantiate(buttonPrefab, moveContainer);

                //we use these a lot so we store them in variables
                RectTransform buttonRect = button.GetComponent<RectTransform>();
                Button buttonComp = button.GetComponent<Button>();
                moveHover = button.GetComponent<MoveHover>();

                //setting up to gather data for the move hover
                moveHover.Move = move;
                moveHover.typeOfButton = MoveHover.TypeOfButton.Move;
                
                //adding the button to the list of buttons for navigation
                navButtons.Add(buttonComp);

                TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

                //give space between buttons
                buttonRect.anchoredPosition = new Vector2(0, -currentPosY);
                currentPosY += buttonSpacing + buttonRect.sizeDelta.y;

                buttonText.text = move.MoveName;
                buttonComp.onClick.AddListener(() => SaveMove(move));

                //representing the method that will be called when the button is clicked
                UnityAction panelAction;

                switch(move.MovesType){
                    case Moves.MoveType.Damaging:
                        panelAction = buttonCon.OnEnemyPanel;
                        break;
                    case Moves.MoveType.Healing:
                        panelAction = buttonCon.OnAllyPanel;
                        break;
                    case Moves.MoveType.Supplementary:
                    if(move is SupplementaryMoves supplementaryMoves){
                        panelAction = supplementaryMoves.Debuffs.Length > 0 ? buttonCon.OnEnemyPanel : supplementaryMoves.Buffs.Length > 0 ? buttonCon.OnAllyPanel : null;
                    }
                    else{
                        panelAction = null;
                        Debug.LogWarning("Panel action is null");
                    }
                    break;
                    default:
                        continue;
                }

                buttonComp.onClick.AddListener(() => panelAction());
            }
        }
        NavButtons(navButtons);
    }

    public void GenerateItems(){
        //destroy all the buttons in the item container before generating new ones
        foreach(Transform button in itemContainer){
            Destroy(button.gameObject);
        }

        navButtons.Clear();
        float currentPosY = 0f; 

        for(int i = 0; i < playerInven.container.Count; i++){
            InvenSlot slot = playerInven.container[i];

            ItemObject item = slot.item;

            if(item.Type == ItemObject.ItemType.Restorative || item.Type == ItemObject.ItemType.Tool)
            {
                GameObject button = Instantiate(buttonPrefab, itemContainer);

                RectTransform buttonRect = button.GetComponent<RectTransform>();
                Button buttonComp = button.GetComponent<Button>();
                TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
                moveHover = button.GetComponent<MoveHover>();

                //setting up to gather data for the move hover
                moveHover.Item = item;
                moveHover.typeOfButton = MoveHover.TypeOfButton.Item;

                navButtons.Add(buttonComp);

                buttonRect.anchoredPosition = new Vector2(0, -currentPosY);
                currentPosY += buttonSpacing + buttonRect.sizeDelta.y;

                buttonText.text = $"{item.ItemName} x {slot.amount}";

                buttonComp.onClick.AddListener(() => SaveItem(item));

                //if the item is a restorative item, the panel action will be to open the ally panel, if it is a tool, the panel action will be to open the enemy panel
                UnityAction panelAction = item.Type == ItemObject.ItemType.Restorative ? buttonCon.OnAllyPanel : buttonCon.OnEnemyPanel;

                buttonComp.onClick.AddListener(() => panelAction());
            }
        }
           NavButtons(navButtons);
    }

    public void GenerateBattleLog(){

        //destroy all the text in the log container before generating new ones
        foreach(Transform text in logContainer){
            Destroy(text.gameObject);
        }
        
        //making text for the battle log and spacing them out
        float currentPosY = 0f;

        foreach(string log in logList){
            GameObject text = Instantiate(textPrefab, logContainer);
            RectTransform textRect = text.GetComponent<RectTransform>();

            textRect.anchoredPosition = new Vector2(0, -currentPosY);
            currentPosY += buttonSpacing + textRect.sizeDelta.y;

            text.GetComponent<TMP_Text>().text = log;
        }
    }

    public void AddToBattleLog(string message){
        logList.Add(message);
    }

    public void ClearBattleLog(){
        foreach(Transform log in logContainer){
            Destroy(log.gameObject);
        }
        logList.Clear();
    }

    //navigation for the buttons
    public void NavButtons(List<Button> navButtons){
        for(int i = 0; i < navButtons.Count; i++){
            //get the navigation of the button
            Navigation nav = navButtons[i].navigation;
            nav.mode = Navigation.Mode.Explicit;
            
            //set the down and up navigation of the button
            nav.selectOnDown = navButtons[(i + 1) % navButtons.Count];
            nav.selectOnUp = navButtons[(i - 1 + navButtons.Count) % navButtons.Count];

            navButtons[i].navigation = nav;
        }
    }

        public void SaveMove(Moves move){
        selectedMove = move;
        selectedItem = null;
        
        }

        public void SaveItem(ItemObject item){
        selectedItem = item;
        selectedMove = null;
        }

        //select the first button in movesalreadyadded
        public void SelectFirstMove(){
            if(navButtons.Count > 0){
                navButtons[0].Select();
            }
        }
}

    
    

