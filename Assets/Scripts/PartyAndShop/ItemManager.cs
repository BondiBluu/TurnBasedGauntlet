using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    ShopButtonController shopButtonController;

    // Start is called before the first frame update
    void Start()
    {
        shopButtonController = FindObjectOfType<ShopButtonController>();
    }

    
}
