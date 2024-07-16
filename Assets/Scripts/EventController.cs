using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public UnityEvent OnWin;
    //have anims play, sound effects, etc with OnWin

    //this is a custom event that passes a CharacterTemplate object
    [System.Serializable]
    public class OnLevelUpEvent : UnityEvent<CharacterTemplate>{};
    
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
