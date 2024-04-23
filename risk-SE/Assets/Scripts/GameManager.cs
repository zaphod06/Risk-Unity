using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using System.Numerics;
using UnityEditor.Experimental.GraphView;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Player> Players = new List<Player>();
    public PlayerCreator playerCreator;
    private int currentTurnIndex = 0;

    public dice Dice;

    public TerritoryCreator territoryCreator;
    public List<Territory> territories = new List<Territory>();

    public List<Card> deck = new List<Card>();
    public bool[] freeCardSpaces;
    
    public Canvas canvas;
    public RectTransform[] cardSpaces;


    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            int randomIndex = Random.Range(0, deck.Count);
            Card randomCard = deck[randomIndex];
            for (int i = 0; i < freeCardSpaces.Length; i++)
            {
                if (freeCardSpaces[i] == true)
                {
                    GameObject cardGO = Instantiate(randomCard.associatedGameObject, canvas.transform);
                    cardGO.name = randomCard.TerritoryName;
                  
                    RectTransform cardRectTransform = cardGO.GetComponent<RectTransform>();
                    cardRectTransform.anchoredPosition = cardSpaces[i].anchoredPosition;

                    freeCardSpaces[i] = false;
                    deck.Remove(randomCard);

                    return;
                }
            }
        }
    }



    private void Awake()
    {

    }
    void Start()
    {
        territories.AddRange(FindObjectsOfType<Territory>());
        //Initialise territories 
        //territories = territoryCreator.createTerriotries();
        //Initialise the deck
        deck.AddRange(CardData.cardList);
        //Set up game
        SetUpGame(playerCreator);
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    //Method to create Players
    private void CreatePlayers(PlayerCreator playerCreator)
    {
        if (playerCreator != null)
        {
            for (int i = 0; i < playerCreator.getAmount(); i++)
            {
                Players.Add(playerCreator.createPlayer());
            }
        }
        else 
        {
            Debug.LogError("PlayerCreator instance is null. Players cannot be created.");
        }
    }

    public void StartGame()
    {
        
        ExecuteTurn(Players[currentTurnIndex]);
    } 

    public void SetUpGame(PlayerCreator playerCreator)
    {
        // Create players
        CreatePlayers(playerCreator);



        // Start the game
        StartGame();
    }



    void EnableTerritoryButtonsForPlayer(Player player)
    {
        foreach (Territory territory in territories)
        {
            // Enable buttons for territories that do not belong to any player
            // and where the player has adjacent territories
            if (territory.Player == null && HasAdjacentTerritoryOwnedByPlayer(territory, player))
            {
                // Enable buttons for this territory
                Button territoryButton = territory.GetComponent<Button>();
                territoryButton.interactable = true;

                // Add an onClick event listener to the button
                territoryButton.onClick.RemoveAllListeners(); // Remove existing listeners to prevent duplicate calls
                territoryButton.onClick.AddListener(() => AssignPlayerToTerritory(territory));
            }
        }
    }

    public void AssignPlayerToTerritory(Territory territory)
    {
        if(territory.Player == null)
        {
            Player player = Players[currentTurnIndex];
            territory.AssignPlayer(player);
            // Implement logic to place troops on the territory
            Debug.Log(player.TurnNumber + " has been assigned to " + territory.Name);
            player.AddTerritory(territory);
            territory.PlaceInfantry();
            EndTurn();
        }
        else if (AllTerritoriesOwned() == true && AllTroopsPlaced() == false)
        {
            
            PlaceInfantry(territory);
        }
        else if (AllTroopsPlaced() == true)
        {
            Debug.Log("Player "+ currentTurnIndex + " attacking " + territory.name);
        }
        else
        {
            
            foreach (Territory terr in territories)
            {
                if(terr.Player == null){
                    Debug.Log(terr.name + " still must be claimed");
                }
            }
            
        }
        
        
    }

    public void Attack(Territory Terr)
    {
        
    }

    public void PlaceInfantry(Territory Terr)
    {
        Player player = Players[currentTurnIndex];
        if (Terr.Player == player) {
            Terr.PlaceInfantry();
            Debug.Log("Player " + player.TurnNumber + " Placing army on territory: " + Terr.Name);
            EndTurn();
        }
        else
        {
            Debug.Log("Player " + player.TurnNumber + "Does not own " + Terr.Name);
        }
        
    }

    bool HasAdjacentTerritoryOwnedByPlayer(Territory territory, Player player)
    {
        foreach (Territory adjacentTerritory in territory.AdjacentTerritories)
        {
            if (adjacentTerritory.Player == player)
            {
                return true;
            }
        }
        return false;
    }
    

    public void ExecuteTurn(Player currentPlayer)
    {
        Debug.Log("Player " + currentPlayer.TurnNumber + "'s turn.");
        //Put in turn actions later

        // Allow the current player to assign armies to territories
        // For example, you can enable buttons for the territories the player can assign armies to
        //EnableTerritoryButtonsForPlayer(currentPlayer);

        //EndTurn();
    }

    public void EndTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % Players.Count;

        //Execute turn for next player
        ExecuteTurn(Players[currentTurnIndex]);

    }

    public bool AllTerritoriesOwned()
    {
        foreach (Territory territory in territories)
        {
            if (territory.Player == null)
            {
                return false;
            }
        }
        return true;
    }

    public bool AllTroopsPlaced()
    {
        foreach (Player player in Players)
        {
            if (player.Infantry > 0)
            {
                return false;
            }
        }
        return true;
    }

}

//Method to distribute territories to each player on set up
/*private void DistributeTerritories()
{

    int deckLength = deck.Count ;

    for (int i = 0; i < deckLength; i++)
    {
        foreach (Player player in Players)
        {
            if (deck.Count > 0)
            {
                int randomIndex = Random.Range(0, deck.Count);
                Card randomCard = deck[randomIndex];

                player.AddTerritory(randomCard.TerritoryName);
                deck.RemoveAt(randomIndex);
            }
            else
            {
                Debug.LogError("Not enough territories to distribute.");

                return;
            }
        }
    }*/

/*int territoriesLength = territories.Count ;

for (int i = 0; i < territoriesLength; i++)
{
    foreach (Player player in Players)
    {
        if (territories.Count > 0)
        {
            int randomIndex = Random.Range(0, territories.Count);
            Territory randomTerr = territories[randomIndex];

            player.AddTerritory(randomTerr);
            territories.RemoveAt(randomIndex);
        }
        else
        {
            Debug.LogError("Not enough territories to distribute.");

            return;
        }
    }
}

}*/


