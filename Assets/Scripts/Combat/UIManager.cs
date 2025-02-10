using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("For HUD")]
    public Transform allyContainer;
    public Transform enemyContainer;
    public Transform characterHUDContainer;
    public Transform enemyHUDContainer;
    public Transform[] enemyUIContainers;
    public GameObject characterHUDPanelPrefab;
    public GameObject enemyMainHUDPanelPrefab;
    public GameObject allyHUDPanelPrefab;
    public GameObject enemyHUDPanelPrefab;

    public Vector2[] enemyUIPositions = new Vector2[4]{
        new Vector2(0, 0),
        new Vector2(-50, 150),
        new Vector2(0, -100),
        new Vector2(0, -150)
    };

    [Header("For UI")]
    public Image characterActionImage;

    [Header("For Stats")]
    public Image characterImage;
    public TMP_Text statsCharaName;
    public TMP_Text statsCharaLevel;
    public TMP_Text statsCharaHP;
    public TMP_Text statsCharaMP;
    public TMP_Text charaAbility;
    public TMP_Text charaAtk;
    public TMP_Text charaDef;
    public TMP_Text charaMag;
    public TMP_Text charaRes;
    public TMP_Text charaEff;
    public TMP_Text charaSki;
    public TMP_Text charaSpd;
    public TMP_Text charaPointToNextLvl;

    public List <Button> allyButtons = new List<Button>();
    public List <Button> enemyButtons = new List<Button>();

    public float buttonSpacing = 50f;
    BattleSystem battleSystem;
    ButtonController buttonController;
    Generator generator;
    PartyManager partyManager;

    private void Start() {
        battleSystem = FindObjectOfType<BattleSystem>();
        buttonController = FindObjectOfType<ButtonController>();
        generator = FindObjectOfType<Generator>();
        partyManager = FindObjectOfType<PartyManager>();
    }

    public void ShowStats(CharacterTemplate character)
    {
        //TODO: have the image of the character
        statsCharaName.text = character.characterData.CharaStatList.CharacterName;
        statsCharaLevel.text = "Level: " + character.currentLevel;
        statsCharaHP.text = $"HP: {character.currentHP}/{character.maxHP}";
        statsCharaMP.text = $"MP: {character.currentMP}/{character.maxMP}";
        charaAbility.text = $"{character.characterData.CharaStatList.CharacterAbility} {character.characterData.CharaStatList.AbilityDesc}";
        charaPointToNextLvl.text = $"Exp to next level: {character.currentEXP}/{character.maxEXP}";
        SetStats(charaAtk, character.currentAttack, character.maxAttack, "Attack");
        SetStats(charaDef, character.currentDefense, character.maxDefense, "Defense");
        SetStats(charaMag, character.currentMagic, character.maxMagic, "Magic");
        SetStats(charaRes, character.currentResistance, character.maxResistance, "Resistance");
        SetStats(charaEff, character.currentEfficiency, character.maxEfficiency, "Efficiency");
        SetStats(charaSki, character.currentSkill, character.maxSkill, "Skill");
        SetStats(charaSpd, character.currentSpeed, character.maxSpeed, "Speed");
    }

    public void SetStats(TMP_Text text, float currentStatValue, float baseStatValue, string statName){
        float statDifference = Convert.ToInt32(currentStatValue - baseStatValue);
        string sign = (statDifference >= 0) ? $"<color=#6EFFFF>+{statDifference}</color>" : $"<color=#FF1100>{statDifference}</color>";

        text.text = $"{statName}: {baseStatValue} ({sign})";
    }

    //make an event that, when PlayerSetup begins, all player HUDS will be set up
    public void InstantiateCharacterPanels(List <CharacterTemplate> charas){

        float currentPosY = 0f;

        for(int i = 0; i < charas.Count; i++){

            //instantiate the panel
            GameObject panel = Instantiate(characterHUDPanelPrefab, characterHUDContainer);

            //set the character's name, level, HP, and MP
            TMP_Text characterName = panel.transform.GetChild(0).GetComponent<TMP_Text>();
            characterName.text = charas[i].characterData.CharaStatList.CharacterName;

            TMP_Text characterLevel = panel.transform.GetChild(1).GetComponent<TMP_Text>();
            characterLevel.text = "Level: " + charas[i].currentLevel;

            TMP_Text characterHP = panel.transform.GetChild(2).GetComponent<TMP_Text>();
            characterHP.text = charas[i].currentHP + "/" + charas[i].maxHP;

            TMP_Text characterMP = panel.transform.GetChild(3).GetComponent<TMP_Text>();
            characterMP.text =  charas[i].currentMP + "/" + charas[i].maxMP;
            
            //set the max value of the slider to the character's max HP
            Image whiteHPBar = panel.transform.GetChild(4).GetComponent<Image>();
            Slider subHPSlider = whiteHPBar.transform.GetChild(3).GetComponent<Slider>();
            subHPSlider.maxValue = charas[i].maxHP;
            subHPSlider.value = charas[i].currentHP;
            Slider mainHPSlider = whiteHPBar.transform.GetChild(4).GetComponent<Slider>();
            mainHPSlider.maxValue = charas[i].maxHP;
            mainHPSlider.value = charas[i].currentHP;

            //set the max value of the slider to the character's max MP
            Image whiteMPBar = panel.transform.GetChild(5).GetComponent<Image>();
            Slider mainMPSlider = whiteMPBar.transform.GetChild(2).GetComponent<Slider>();
            mainMPSlider.maxValue = charas[i].maxMP;
            mainMPSlider.value = charas[i].currentMP;

            //have the next panel be directly below the previous panel
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -currentPosY);
            currentPosY += buttonSpacing + panel.GetComponent<RectTransform>().sizeDelta.y;
        }
    }

    public void InstantiateEnemyHealthBars(List <CharacterTemplate> charas){

        for(int i = 0; i < charas.Count; i++){
            GameObject panel = Instantiate(enemyMainHUDPanelPrefab, enemyHUDContainer);

            TMP_Text characterName = panel.transform.GetChild(0).GetComponent<TMP_Text>();
            characterName.text = charas[i].characterData.CharaStatList.CharacterName;

            Image whiteHPBar = panel.transform.GetChild(1).GetComponent<Image>();

            
            Slider mainHPSlider = whiteHPBar.transform.GetChild(4).GetComponent<Slider>();
            mainHPSlider.maxValue = charas[i].maxHP;
            mainHPSlider.value = charas[i].currentHP;

            Slider subHPSlider = whiteHPBar.transform.GetChild(3).GetComponent<Slider>();
            subHPSlider.maxValue = charas[i].maxHP;
            subHPSlider.value = charas[i].currentHP;    

            //getting the enemy's UI container positions
            switch(i){
                case 0:
                    panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                    break;
                case 1:
                    panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-320, 160);
                    break;
                case 2:
                    panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-320, -150);
                    break;
                case 3:
                    panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, 315);
                    break;
            }        
        }
    }

    public void InstantiateAllyPanels(List <CharacterTemplate> charas){

        float currentPosY = 0f;
        generator.navButtons.Clear();

        for(int i = 0; i < charas.Count; i++){

            //instantiate the panel
            GameObject panel = Instantiate(allyHUDPanelPrefab, allyContainer);

            //set the character's name
            Button characterButton = panel.transform.GetChild(0).GetComponent<Button>();
            allyButtons.Add(characterButton);
            characterButton.GetComponentInChildren<TMP_Text>().text = charas[i].characterData.CharaStatList.CharacterName;
            
            //set the max value of the slider to the character's max HP
            Image whiteHPBar = panel.transform.GetChild(1).GetComponent<Image>();
            Slider mainHPSlider = whiteHPBar.transform.GetChild(2).GetComponent<Slider>();
            mainHPSlider.maxValue = charas[i].maxHP;
            mainHPSlider.value = charas[i].currentHP;

            //set the max value of the slider to the character's max MP
            Image whiteMPBar = panel.transform.GetChild(2).GetComponent<Image>();
            Slider mainMPSlider = whiteMPBar.transform.GetChild(2).GetComponent<Slider>();
            mainMPSlider.maxValue = charas[i].maxMP;
            mainMPSlider.value = charas[i].currentMP;

            

            //have the next panel be directly below the previous panel
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -currentPosY);
            currentPosY += buttonSpacing + panel.GetComponent<RectTransform>().sizeDelta.y;
            
            int index = i;
            characterButton.onClick.AddListener(() => battleSystem.OnAllyClick(index));
            characterButton.onClick.AddListener(() => buttonController.OnTurnOffPanels());
        }
        generator.NavButtons(allyButtons);
    }

    public void InstantiateEnemyPanels(List <CharacterTemplate> charas){
        float currentPosY = 0f;
        generator.navButtons.Clear();

        for(int i = 0; i < charas.Count; i++){

            //instantiate the panel
            GameObject panel = Instantiate(enemyHUDPanelPrefab, enemyContainer);

            //set the character's name
            Button characterButton = panel.transform.GetChild(0).GetComponent<Button>();
            enemyButtons.Add(characterButton);
            characterButton.GetComponentInChildren<TMP_Text>().text = charas[i].characterData.CharaStatList.CharacterName;
            
            //set the max value of the slider to the character's max HP
            Image whiteHPBar = panel.transform.GetChild(1).GetComponent<Image>();
            Slider mainHPSlider = whiteHPBar.transform.GetChild(2).GetComponent<Slider>();
            mainHPSlider.maxValue = charas[i].maxHP;
            mainHPSlider.value = charas[i].currentHP;

            //have the next panel be directly below the previous panel
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -currentPosY);
            currentPosY += buttonSpacing + panel.GetComponent<RectTransform>().sizeDelta.y;

            int index = i;
            characterButton.GetComponent<Button>().onClick.AddListener(() => battleSystem.OnEnemyClick(index));
            characterButton.GetComponent<Button>().onClick.AddListener(() => buttonController.OnTurnOffPanels());
        }
        generator.NavButtons(enemyButtons);
    }

    public void SelectFirstMove(List<Button> navButtons){
            if(navButtons.Count > 0){
                navButtons[0].Select();
            }
        }

    //updating character hp
    public IEnumerator UpdateHP(CharacterTemplate character){
        foreach(Transform child in characterHUDContainer){
            if(child.GetComponentInChildren<TMP_Text>().text == character.characterData.CharaStatList.CharacterName){
                TMP_Text characterHP = child.GetChild(2).GetComponent<TMP_Text>();

                Image whiteHPBar = child.GetChild(4).GetComponent<Image>();
                Slider subHPSlider = whiteHPBar.transform.GetChild(3).GetComponent<Slider>();
                Slider mainHPSlider = whiteHPBar.transform.GetChild(4).GetComponent<Slider>();
                yield return new WaitForSeconds(1f);

                if(mainHPSlider.value > character.currentHP){
                    //decrease the main hp slider
                    mainHPSlider.value = character.currentHP;
                    characterHP.text = character.currentHP + "/" + character.maxHP;
                    yield return new WaitForSeconds(1f);
                    while(subHPSlider.value > character.currentHP){
                        subHPSlider.value --;
                        yield return new WaitForSeconds(0.01f);
                    }
                    subHPSlider.value = character.currentHP;
                } else if (mainHPSlider.value < character.currentHP){
                    while(mainHPSlider.value < character.currentHP){
                        mainHPSlider.value ++;
                        characterHP.text = mainHPSlider.value + "/" + character.maxHP;
                        yield return new WaitForSeconds(0.01f);
                    }
                    mainHPSlider.value = character.currentHP;
                    subHPSlider.value = character.currentHP;
                }
                
            }
        }
    }

    //updating character mp
    public IEnumerator UpdateMP(CharacterTemplate character){
        foreach(Transform child in characterHUDContainer){
            if(child.GetComponentInChildren<TMP_Text>().text == character.characterData.CharaStatList.CharacterName){
                TMP_Text characterMP = child.GetChild(3).GetComponent<TMP_Text>();

                Image whiteMPBar = child.GetChild(5).GetComponent<Image>();
                Slider mainMPSlider = whiteMPBar.transform.GetChild(2).GetComponent<Slider>();

                if(mainMPSlider.value > character.currentMP){
                    while(mainMPSlider.value > character.currentMP){
                        mainMPSlider.value --;
                        characterMP.text = mainMPSlider.value + "/" + character.maxMP;
                        yield return new WaitForSeconds(0.01f);
                    }
                    mainMPSlider.value = character.currentMP;
                }
                else if (mainMPSlider.value < character.currentMP){
                        while(mainMPSlider.value < character.currentMP){
                            mainMPSlider.value ++;
                            characterMP.text = mainMPSlider.value + "/" + character.maxMP;
                            yield return new WaitForSeconds(0.01f);
                        }
                        mainMPSlider.value = character.currentMP;
                }
            }
        }
    }

    public IEnumerator UpdateEnemyHPBar(CharacterTemplate character){
        foreach(Transform child in enemyHUDContainer){
            if(child.GetComponentInChildren<TMP_Text>().text == character.characterData.CharaStatList.CharacterName){
             
                Image whiteHPBar = child.transform.GetChild(1).GetComponent<Image>();
                Slider subHPSlider = whiteHPBar.transform.GetChild(3).GetComponent<Slider>();
                Slider mainHPSlider = whiteHPBar.transform.GetChild(4).GetComponent<Slider>();

                if(mainHPSlider.value > character.currentHP){
                    //decrease the main hp slider
                    mainHPSlider.value = character.currentHP;
                    yield return new WaitForSeconds(1f);
                    while(subHPSlider.value > character.currentHP){
                        subHPSlider.value --;
                        yield return new WaitForSeconds(0.01f);
                    }
                    subHPSlider.value = character.currentHP;
                } else if (mainHPSlider.value < character.currentHP){
                    while(mainHPSlider.value < character.currentHP){
                        mainHPSlider.value ++;
                        yield return new WaitForSeconds(0.01f);
                    }
                    mainHPSlider.value = character.currentHP;
                    subHPSlider.value = character.currentHP;
                }
            }
        }
    }

       //updating character hp panels (the sub panels when you click on a move)
    public void UpdateHPPanel(CharacterTemplate character){
        foreach(Transform child in allyContainer){
            if(child.GetComponentInChildren<TMP_Text>().text == character.characterData.CharaStatList.CharacterName){
                //getting the sub and main hp sliders
                
                Image whiteHPBar = child.GetChild(1).GetComponent<Image>();
                Slider mainHPSlider = whiteHPBar.transform.GetChild(2).GetComponent<Slider>();
                mainHPSlider.value = character.currentHP;
            }
        }
    }

    //updating enemy hp panels
    public void UpdateEnemyHPPanel(CharacterTemplate character){
        foreach(Transform child in enemyContainer){
            if(child.GetComponentInChildren<TMP_Text>().text == character.characterData.CharaStatList.CharacterName){
                Image whiteHPBar = child.GetChild(1).GetComponent<Image>();
                Slider mainHPSlider = whiteHPBar.transform.GetChild(2).GetComponent<Slider>();
                mainHPSlider.value = character.currentHP;
            }
        }
    }

    //updating character mp panels
    public void UpdateMPPanel(CharacterTemplate character){
        foreach(Transform child in allyContainer){
            if(child.GetComponentInChildren<TMP_Text>().text == character.characterData.CharaStatList.CharacterName){
                Image whiteMPBar = child.GetChild(2).GetComponent<Image>();
                Slider mainMPSlider = whiteMPBar.transform.GetChild(2).GetComponent<Slider>();
                mainMPSlider.value = character.currentMP;
            }
        }
    }

    //having the UI reflect character changes when leveling up
    public void UpdateLevelStats(CharacterTemplate character){
        Debug.Log($"Leveling up {character.characterData.CharaStatList.CharacterName}");
        foreach(Transform panel in characterHUDContainer){
            if(panel.GetComponentInChildren<TMP_Text>().text == character.characterData.CharaStatList.CharacterName)
            {
                TMP_Text characterLevel = panel.transform.GetChild(1).GetComponent<TMP_Text>();
                characterLevel.text = "Level: " + character.currentLevel;

                TMP_Text characterHP = panel.transform.GetChild(2).GetComponent<TMP_Text>();
                characterHP.text = character.currentHP + "/" + character.maxHP;

                TMP_Text characterMP = panel.transform.GetChild(3).GetComponent<TMP_Text>();
                characterMP.text =  character.currentMP + "/" + character.maxMP;
            
                Image whiteHPBar = panel.transform.GetChild(4).GetComponent<Image>();
                Slider subHPSlider = whiteHPBar.transform.GetChild(3).GetComponent<Slider>();
                subHPSlider.maxValue = character.maxHP;
                subHPSlider.value = character.currentHP;
                Slider mainHPSlider = whiteHPBar.transform.GetChild(4).GetComponent<Slider>();
                mainHPSlider.maxValue = character.maxHP;
                mainHPSlider.value = character.currentHP;

                Image whiteMPBar = panel.transform.GetChild(5).GetComponent<Image>();
                Slider mainMPSlider = whiteMPBar.transform.GetChild(2).GetComponent<Slider>();
                mainMPSlider.maxValue = character.maxMP;
                mainMPSlider.value = character.currentMP;
            }
        }
    }
    
}
