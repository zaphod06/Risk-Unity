using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card 
{
    public int id;
    public string TerritoryName;
    public string TroopType;
    public GameObject associatedGameObject;

    public Card()
    { }
    public Card(int Id, string territoryName, string troopType, GameObject associatedGameObject)
    {   
        id = Id;
        TerritoryName = territoryName;
        TroopType = troopType;
        this.associatedGameObject = associatedGameObject;
    }
}