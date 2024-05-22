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
    public Transform moveContainer;
    public Transform itemContainer;
    public float buttonSpacing;
    public Moves selectedMove;
    public ItemObject selectedItem;
    public List<Moves> movesAlreadyAdded = new List<Moves>();
    public List<Button> navButtons = new List<Button>();
    ButtonController buttonCon;

    public InvenObject playerInven;
    public CharacterTemplate savedTarget;

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
                    case Moves.MoveType.Drain:
                        panelAction = buttonCon.OnEnemyPanel;
                        break;
                    case Moves.MoveType.Healing:
                        panelAction = buttonCon.OnAllyPanel;
                        break;
                    case Moves.MoveType.Supplementary:
                    //if the move has debuffs, the panel action will be to open the enemy panel, if it has buffs, the panel action will be to open the ally panel
                        panelAction = move.Debuffs.Length > 0 ? buttonCon.OnEnemyPanel : move.Buffs.Length > 0 ? buttonCon.OnAllyPanel : null;
                        break;
                    default:
                        continue;
                }

                buttonComp.onClick.AddListener(() => panelAction());
            }
        }
        NavButtons(navButtons);
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

    public void GenerateItems(){
        //destroy all the buttons in the item container before generating new ones
        foreach(Transform button in itemContainer){
            Destroy(button.gameObject);
        }

        float currentPosY = 0f;

        

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

    
    

