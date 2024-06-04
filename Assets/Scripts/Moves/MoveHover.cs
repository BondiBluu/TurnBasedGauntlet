using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveHover : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public enum TypeOfButton
    {
        Move,
        Item
    }

    [Header("Moves")]
    public TypeOfButton typeOfButton;
    public Moves Move {get; set;}
    public ItemObject Item {get; set;}

    Generator generator;

    void Awake(){
        generator = FindObjectOfType<Generator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(typeOfButton == TypeOfButton.Move){
            MoveData(Move);
        }
        else{
            
        }
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(typeOfButton == TypeOfButton.Move){
            MoveData(Move);
        }
        else{
            
        }
    }

    void MoveData(Moves move){        
         generator.moveName.text = move.MoveName;
         generator.moveCost.text = "Cost: " + move.MPCost.ToString();
         generator.movePower.text = "Power: " + move.MovePowerString;
         generator.moveType.text = move.MovesType.ToString();
         generator.moveExplanation.text = move.MoveExplanation;}
}

