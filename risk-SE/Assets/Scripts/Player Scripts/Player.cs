using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Territory> Territories = new List<Territory>();
    public MissionCards mission;
    public List<Card> cards = new List<Card>();
    public int TurnNumber;
    public bool AI = false;
    int CardNumber = 0;
    List<Card> TradingCards = new List<Card>();
    int set = 2;

    
    

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
    public void AddCard(Card card)
    {
        cards.Add(card);
    }
    //Trades cards that have at least 3 of the same type of troop
    public void TradeCard(string troopType)
    {
        CardNumber = 0;
        foreach (Card card in cards)
        {
            if (card.TroopType == troopType)
            {
                TradingCards.Add(card);
                CardNumber = CardNumber + 1;
                if(CardNumber >= 3)
                {
                    break; 
                }
            }

        }
        if (CardNumber >= 3)
        {
            List<Card> CardsRemoved = new List<Card>();
            foreach (Card card in TradingCards)
            {
                CardsRemoved.Add(card);
            }

            foreach (Card cardToRemove in CardsRemoved)
            {
                cards.Remove(cardToRemove);
                TradingCards.Remove(cardToRemove);
            }

            Debug.Log("Trade worked! All " + troopType + " cards have been traded.");
            if (set < 12)
            {
                
                set = set + 2;
                GiveInfantry(set);
            }
            else if (set == 12){
                set = set + 3;
                GiveInfantry(set);
            }
            else
            {
                set = set + 5;
                GiveInfantry(set);
            }
        }
        else
        {
            Debug.Log("Trade did not work!");
        }            
    }
    //Trades WildCards
    public void TradeWildCard(string troopType)
    {
        List<Card> cardsToRemove = new List<Card>();
        foreach (Card card in cards)
        {
            if (card.TroopType == troopType)
            {
                cardsToRemove.Add(card);
            }
        }

        foreach (Card cardToRemove in cardsToRemove)
        {
            cards.Remove(cardToRemove);
        }

        Debug.Log("Trade worked! The " + troopType + " has been traded alongside:");

        for (int i = 0; i < 2; i++)
        {
            int randomIndex = Random.Range(0, cards.Count);
            Debug.Log("The " + cards[randomIndex].TroopType + " Card");
            cards.RemoveAt(randomIndex);
        }
    }


}

