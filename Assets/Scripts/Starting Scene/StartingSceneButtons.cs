using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingSceneButtons : MonoBehaviour
{
    public Button startButton;
    public Button contButton;
    public Button optionsButton;
    public Button exitButton;
    public Button achieveButton;

    public void StartGame(){
        Debug.Log("Game Started");
    }

    public void ContinueGame(){
        Debug.Log("Game Continued");
    }

    public void OpenOptions(){
        Debug.Log("Options Opened");
    }

    public void ExitGame(){
        Debug.Log("Game Exited");
    }

    public void OpenAchievements(){
        Debug.Log("Achievements Opened");
    }
}
