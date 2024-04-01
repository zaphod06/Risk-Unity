using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId;

    public int id;
    public string TerritoryName;
    public string TroopType;

    public TextMeshProUGUI territoryText;
    public TextMeshProUGUI troopText;


    void Start()
    {
        thisCard[0] = CardData.cardList[thisId];
    }

    // Update is called once per frame
    void Update()
    {
        if (thisCard.Count > 0)
        {            
         territoryText.text = TerritoryName;
         troopText.text = TroopType;
                             
        id = thisCard[0].id;
        TerritoryName = thisCard[0].TerritoryName;
        TroopType = thisCard[0].TroopType;
        territoryText.text = "" + TerritoryName;
        troopText.text = "" + TroopType;
        }
        else
        {
            Debug.LogWarning("thisCard list is empty!");
        }
    }
}
