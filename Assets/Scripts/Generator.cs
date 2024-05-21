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
    ButtonController buttonCon;

    public InvenObject playerInven;
    public CharacterTemplate savedTarget;
    
    public void Start(){
        buttonCon = FindObjectOfType<ButtonController>();
    }

    //takes the character unit’s characterData’s movelist 
    public void GenerateMoves(CharacterTemplate character){

        //destroy all the buttons in the move container before generating new ones
        foreach(Transform button in moveContainer){
            Destroy(button.gameObject);
        }

        float currentPosY = 0f;


        //getting each move in movelist
        for(int i = 0; i < character.characterData.Moveset.Moves.Count; i++){

            Moves move = character.characterData.Moveset.Moves[i];
            
            if(character.currentLevel >= move.LvlGotten && !movesAlreadyAdded.Contains(move)){
                movesAlreadyAdded.Add(move);
                
                GameObject button = Instantiate(buttonPrefab, moveContainer);

                //we use these a lot so we store them in variables
                RectTransform buttonRect = button.GetComponent<RectTransform>();
                Button buttonComp = button.GetComponent<Button>();
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

        // foreach(Moves move in character.characterData.Moveset.Moves){
        //     if(character.currentLevel >= move.LvlGotten && !movesAlreadyAdded.Contains(move)){
        //         movesAlreadyAdded.Add(move);
                
        //         GameObject button = Instantiate(buttonPrefab, moveContainer);

        //         //we use these a lot so we store them in variables
        //         RectTransform buttonRect = button.GetComponent<RectTransform>();
        //         Button buttonComp = button.GetComponent<Button>();
        //         TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        //         //give space between buttons
        //         buttonRect.anchoredPosition = new Vector2(0, -currentPosY);
        //         currentPosY += buttonSpacing + buttonRect.sizeDelta.y;

        //         buttonText.text = move.MoveName;
        //         buttonComp.onClick.AddListener(() => SaveMove(move));

        //         //representing the method that will be called when the button is clicked
        //         UnityAction panelAction;

        //         switch(move.MovesType){
        //             case Moves.MoveType.Damaging:
        //             case Moves.MoveType.Drain:
        //                 panelAction = buttonCon.OnEnemyPanel;
        //                 break;
        //             case Moves.MoveType.Healing:
        //                 panelAction = buttonCon.OnAllyPanel;
        //                 break;
        //             case Moves.MoveType.Supplementary:
        //             //if the move has debuffs, the panel action will be to open the enemy panel, if it has buffs, the panel action will be to open the ally panel
        //                 panelAction = move.Debuffs.Length > 0 ? buttonCon.OnEnemyPanel : move.Buffs.Length > 0 ? buttonCon.OnAllyPanel : null;
        //                 break;
        //             default:
        //                 continue;
        //         }

        //         buttonComp.onClick.AddListener(() => panelAction());
        //     }
        // }
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
}

    
    

