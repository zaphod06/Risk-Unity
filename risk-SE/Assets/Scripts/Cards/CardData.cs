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
        
        List<string> territoryNames = new List<string> { "East Africa", "Venezuala", "Brazil", "Greenland", "Iceland", "Kamchatka", "Peru","Eastern Canada", "Middle East", "New Guinea", "South Africa", "Madagascar", "Western United States", "Scandinavia", "Alberta", "North West", "Great Britain", "Eastern United States", "Central America", "Northern Europe", "Western Europe", "Southern Europe", "North Africa", "Siberia", "Mongolia", "Japan", "Ural", "South East Asia", "Afghanistan", "Indonesia", "Irkutsk", "Eastern Australia", "Western Australia", "Russia", "Ontario", "Argentina", "Ukraine", "Siam", "Quebec", "Congo"};
        List<string> troopTypes = new List<string> { "Infantry", "Infantry", "Artillery", "Cavalry", "Infantry", "Infantry", "Infantry", "Infantry", "Cavalry", "Infantry", "Infantry", "Artillery", "Cavalry", "Artillery", "Cavalry", "Cavalry", "Artillery", "Artillery", "Artillery", "Artillery", "Artillery", "Artillery", "Artillery", "Cavalry", "Cavalry", "Infantry", "Artillery", "Cavalry", "Cavalry", "Infantry", "Cavalry", "Artillery", "Cavalry", "Artillery", "Artillery", "Cavalry", "Cavalry", "Infantry", "Infantry", "Cavalry", "Artillery", "Artillery"  };
       
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
