using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Territory> Territories = new List<Territory>();
    public int noTroops;
    public MissionCards mission;
    public Cards[] cards;
    public string colour;
    public int TurnNumber;

    public int Infantry = 0;
    public int Cavalry = 0;
    public int Artillery = 0;



    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to add a territory to the player's list of territories  
    public void AddTerritory(Territory territory)
    {
        Territories.Add(territory);
    }

    // Method to place an army on a territory owned by the player
    public void PlaceArmy(Territory territory)
    {
        // Check if the player owns the territory
        if (Territories.Contains(territory))
        {
            territory.PlaceInfantry();
            Infantry--;
            Debug.Log("Placing army on territory: " + territory.Name);
        }
        else
        {
            Debug.LogWarning("Player does not own territory: " + territory.Name);
        }
    }

    public void AssignTurn(int turn)
    {
        TurnNumber = turn;
    }

    public int GetTurn()
    {
        return TurnNumber;
    }

    public void AssignInfantry(int InfantryNo)
    {
        Infantry = InfantryNo;
    }

    public void GiveInfantry(int amount)
    {
        Infantry = Infantry + amount;
    }
}
