using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public UnityEvent OnWin;
    //have anims play, sound effects, etc with OnWin
    public UnityEvent OnLevelUp;
    
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
