using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardData : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();
    public static List<GameObject> cardGameObjects = new List<GameObject>();
    public GameObject cardPrefab;

 

    void Awake()
    {

        List<string> territoryNames = new List<string> { "EastAfrica", "Venezuela", "Brazil", "Greenland", "Iceland", "Kamchatka", "Peru", "EasternCanada", "MiddleEast", "NewGuinea", "SouthAfrica", "Madagascar", "WesternUnitedStates", "Scandinavia", "Alberta", "NorthWest", "GreatBritain", "EasternUnitedStates", "CentralAmerica", "NorthernEurope", "WesternEurope", "SouthernEurope", "NorthAfrica", "Siberia", "Mongolia", "Japan", "Ural", "SouthEastAsia", "Afghanistan", "Indonesia", "Irkutsk", "EasternAustralia", "WesternAustralia", "Russia", "Ontario", "Argentina", "Ukraine", "Siam", "Quebec", "Congo", "SouthAmerica", "Australia" };
;
        List<string> troopTypes = new List<string> { "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery", "Infantry", "Cavalry", "Artillery" };
       
        for (int i = 0; i < territoryNames.Count; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab);
            cardGO.name = territoryNames[i];
            TextMeshProUGUI[] textComponents = cardGO.GetComponentsInChildren<TextMeshProUGUI>();

            if (textComponents.Length >= 2)
            {
               
                textComponents[0].text = territoryNames[i]; 
                textComponents[1].text = troopTypes[i]; 
            }
            else
            {
                Debug.LogError("TextMeshProUGUI components not found in card prefab hierarchy");
            }


            cardGameObjects.Add(cardGO);
            Card card = new Card(i, territoryNames[i], troopTypes[i], cardGO);
            cardList.Add(card);

        }
    }

}
