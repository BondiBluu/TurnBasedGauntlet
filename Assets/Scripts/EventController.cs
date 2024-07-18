using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    LevelUpRolls levelUpRolls;

    Queue<CharacterTemplate> levelUpQueue = new Queue<CharacterTemplate>();
    public UnityEvent OnWin;
    //have anims play, sound effects, etc with OnWin

    //this is a custom event that passes a CharacterTemplate object
    [System.Serializable]
    public class OnLevelUpEvent : UnityEvent<CharacterTemplate>{};

    void Start(){
        levelUpRolls = FindObjectOfType<LevelUpRolls>();
    }

    public void AddToLevelUpQueue(CharacterTemplate character){
        levelUpQueue.Enqueue(character);
        Debug.Log("Added to level up queue");
        Debug.Log($"Queue count: {levelUpQueue.Count}. Character: {character.characterData.CharaStatList.CharacterName}");
        if(levelUpQueue.Count == 1){
            
            QueueNextCharaLevelUp();
            
        }
    }

    //dequeue the first character in the queue and invokes the OnLevelUp event
    public void QueueNextCharaLevelUp(){
        

        if(levelUpQueue.Count > 0){
            Debug.Log("QueueLevelUp called");
            CharacterTemplate chara = levelUpQueue.Dequeue();
            OnLevelUp.Invoke(chara);
        } else{
            levelUpRolls.CloseLevelUpPanel();
        }
    }
    
    public OnLevelUpEvent OnLevelUp;
    
    public UnityEvent OnLose;

    public static EventController instance;

    void Awake(){
        //ensuring only one instance of the event controller exists
        if( instance != null && instance != this){
            Destroy(this.gameObject);
        }else{
            instance = this;
        }
    }

    

}
