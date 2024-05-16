using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform moveContainer;
    public Transform itemContainer;
    public float buttonSpacing;
    public List<Moves> movesAlreadyAdded = new List<Moves>();

    //takes the character unit’s characterData’s moveLlist 
    public void GenerateMoves(CharacterTemplate character){

        float currentPosY = 0f;

        foreach(Moves move in character.characterData.Moveset.Moves){
            if(character.currentLevel >= move.LvlGotten && !movesAlreadyAdded.Contains(move)){
                movesAlreadyAdded.Add(move);
                
                GameObject button = Instantiate(buttonPrefab, moveContainer);

                //give space between buttons
                button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -currentPosY);
                currentPosY += buttonSpacing + button.GetComponent<RectTransform>().sizeDelta.y;

                button.GetComponentInChildren<TMP_Text>().text = move.MoveName;
                //button.GetComponent<Button>().onClick.AddListener(delegate {SelectMove(move);});
            }
        }
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
