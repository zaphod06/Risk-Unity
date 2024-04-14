using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<string> Territories = new List<string>();
    public int noTroops;
    public MissionCards mission;
    public Cards[] cards;
    public string colour;
    public int TurnNumber;
    public Dice_d6_Plastic Dice;
    public Deck deck;
    public Troops[] troops;
    
    

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to add a territory to the player's list of territories
    public void AddTerritory(string territoryName)
    {
        Territories.Add(territoryName);
    }

    // Method to place an army on a territory owned by the player
    public void PlaceArmy(string territoryName)
    {
        // Check if the player owns the territory
        if (Territories.Contains(territoryName))
        {
            //NEED TO FINSIH
            Debug.Log("Placing army on territory: " + territoryName);
        }
        else
        {
            Debug.LogWarning("Player does not own territory: " + territoryName);
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
}
