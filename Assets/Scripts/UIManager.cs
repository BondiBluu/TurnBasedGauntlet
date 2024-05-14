using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("For HUD")]
    public GameObject attackPanel;
    public GameObject statsPanel;
    public GameObject inventoryPanel;
    public GameObject fleePanel;
    public GameObject enemyPanel;
    public GameObject allyPanel;

    [Header("For UI")]
    public TMP_Text characterName;
    public TMP_Text characterLevel;
    public TMP_Text characterHP;
    public TMP_Text characterMP;
    public Slider characterHPSlider;
    public Slider characterMPSlider;

    [Header("For Stats")]
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
