/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;

    public void CreateCard(int id, string territoryName, string troopType)
    {
        // Instantiate the card prefab
        GameObject cardGameObject = Instantiate(cardPrefab);

        // Create a new Card object
        Card newCard = new Card(id, territoryName, troopType);

        // Assign the GameObject reference to the Card object
        newCard.associatedGameObject = cardGameObject;

        // Add any further initialization or logic here if needed
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/