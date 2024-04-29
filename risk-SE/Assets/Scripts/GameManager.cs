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
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    // Player variables
    public List<Player> Players = new List<Player>();
    public PlayerCreator playerCreator;
    
    private int currentTurnIndex = 0;

    public dice Dice;

    //Territory variables
    public TerritoryCreator territoryCreator;
    public List<Territory> territories = new List<Territory>();

    //Card variables
    public List<Card> deck = new List<Card>();
    public bool[] freeCardSpaces;
    
    public Canvas canvas;
    public RectTransform[] cardSpaces;

    private bool Setup = false;

    //Variable ditactiating what phase the game is in
    public bool troopPlacingPhase = false;
    public bool battlePhase = false;

    //Variables used for battle mechanics
    public int troopsAttacking;
    public int troopsDefending;
    public GameObject attackerPrompt;
    public GameObject defenderPrompt;
    //Stores last two selected territories to know what territory is attacking and defending
    private Territory[] lastSelectedTerritories = new Territory[2];
    



    private void Awake()
    {

    }
    void Start()
    {
        //Initilise territories 
        territories.AddRange(FindObjectsOfType<Territory>());
        
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
        /*if (playerCreator != null)
        {
            for (int i = 0; i < playerCreator.getAmount(); i++)
            {
                Players.Add(playerCreator.createPlayer());
            }
        }
        else 
        {
            Debug.LogError("PlayerCreator instance is null. Players cannot be created.");
        }*/
        Players = playerCreator.createPlayers();
    }

    

    public void SetUpGame(PlayerCreator playerCreator)
    {
        // Create players
        CreatePlayers(playerCreator);



        // Start the game
        StartGame();
    }
    public void StartGame()
    {
        Debug.Log("Player " + Players[currentTurnIndex].TurnNumber + "'s turn.");
        ExecuteTurn(Players[currentTurnIndex]);
    }




    public void AssignPlayerToTerritory(Territory territory)
    {
        if (Setup == false)
        {
            if (territory.Player == null)
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

                SetUpInfantry(territory);
            }
            else
            {

                foreach (Territory terr in territories)
                {
                    if (terr.Player == null)
                    {
                        Debug.Log(terr.name + " still must be claimed");
                    }
                }

            }
        }
        
        
        
    }


    public void SetUpInfantry(Territory Terr)
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
    



    public void PlaceInfantry(Territory Terr)
    {
        Player player = Players[currentTurnIndex];
        if(Setup == true && troopPlacingPhase == true)
        {
            if (Terr.Player == player && player.Infantry > 0)
            {
                Terr.PlaceInfantry();
                Debug.Log("Player " + player.TurnNumber + " Placing army on territory: " + Terr.Name);
                //check if player is ready to move onto attacking phase of their turn
                if (player.Infantry == 0)
                {
                    troopPlacingPhase = false;
                    battlePhase=true;
                    Debug.Log("Player may now attack");
                }
                
            }
            else if(player.Infantry == 0)
            {
                Debug.Log("Player has no more troops");
                
                
            }
            else if(player.Infantry > 0 && Terr.Player != player)
            {
                Debug.Log("Player " + player.TurnNumber + " Does not own " + Terr.Name);
            }
        }
   
    }

    public void ExecuteTurn(Player currentPlayer)
    {
        
        
        //Player given Infantry at start of their turn
        if (currentPlayer.Infantry == 0)
        {
            //Calcukate how much infantry a player will receive
            battlePhase = false;
            troopPlacingPhase = true;
            int amount = currentPlayer.Territories.Count / 3;
            Setup = true;

            //Minimum is 3
            if (amount < 3)
            {
                amount = 3;
            }
            currentPlayer.GiveInfantry(amount);
            Debug.Log("Player " + currentPlayer.TurnNumber + " has been given " + amount + " infantry.");
        }

        //Code for the AI player
        if (currentPlayer.AI == true)
        {
            if (Setup == false)
            {   
                //What AI player does during set up
                if (AllTerritoriesOwned() == true)
                {
                    foreach(Territory terr in currentPlayer.Territories)
                    {
                        SetUpInfantry(terr);
                        if (Players[currentTurnIndex] != currentPlayer)
                        {
                            break;

                        }
                    }
                    
                }
                else
                {
                    foreach(Territory territory in territories)
                    {
                        AssignPlayerToTerritory(territory);
                        if (Players[currentTurnIndex] != currentPlayer)
                        {
                            break;

                        }
                    }
                   
                }
            }
            //AI player after setuo
            else if(troopPlacingPhase == true)
            {
                int turns = currentPlayer.Infantry;
                for(int i = 0; i <= turns; i++)
                {
                    int randomTerritoryIndex = Random.Range(0, currentPlayer.Territories.Count);
                    Territory terr = currentPlayer.Territories[randomTerritoryIndex];
                    PlaceInfantry(terr);
                }
                battlePhase = true;
                //AI player battle 
                troopsAttacking = 2;
                lastSelectedTerritories[1] = currentPlayer.Territories[0];
                lastSelectedTerritories[0] = lastSelectedTerritories[1].AdjacentTerritories[0];
                if (!currentPlayer.Territories.Contains(lastSelectedTerritories[0]))
                {
                    Debug.Log("Player " + currentTurnIndex + " attacking " + lastSelectedTerritories[0].name + " with " + lastSelectedTerritories[1].name);
                    defenderPrompt.SetActive(true);
                }
                
                

            }
            
        }
    }
    public void EndTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % Players.Count;

        //Execute turn for next player
        
        Debug.Log("Player " + Players[currentTurnIndex].TurnNumber + "'s turn.");
        ExecuteTurn(Players[currentTurnIndex]);
    }

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

    //Method to check all territories have been claimed, indicates to move to next part of setup
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

    //Method to check all troops are placed and game has been setup
    public bool AllTroopsPlaced()
    {
        foreach (Player player in Players)
        {
            if (player.Infantry > 0)
            {
                return false;
            }
        }
        Setup = true;
        return true;
    }

    //Method to check player is allowed to end there turn
    //Will be linked to end turn button of game
    public void EndTurnIfAllowed()
    {
        Player player = Players[currentTurnIndex];
        if (player.Infantry == 0)
        {
            EndTurn();
        }
        else
        {
            Debug.Log("Player must finsish placing troops before ending turn");
        }
    }

    //Methods for battles

    //Battle method
    public void Battle(Territory terr)
    {
        Debug.Log("Attacking");

        int attackerRoll1 = 0, attackerRoll2 = 0, attackerRoll3 = 0;
        int defenderRoll1 = 0, defenderRoll2 = 0;
        List<int> attackersRolls = new List<int>();
        
        if (troopsAttacking == 2)
        {
            
            attackerRoll1 = Dice.Roll();
            Debug.Log("Attacker rolled " + attackerRoll1);
        }
        else if(troopsAttacking == 3)
        {

            int roll1 = Dice.Roll();
            int roll2 = Dice.Roll();
            
            attackerRoll1 = Mathf.Max(roll1, roll2);
            attackerRoll2 = Mathf.Min(roll2, roll1);
            Debug.Log("Attacker rolled " + attackerRoll1 + ", " + attackerRoll2);
        }
        else if (troopsAttacking == 4)
        {
            int roll1 = Dice.Roll();
            int roll2 = Dice.Roll();
            int roll3 = Dice.Roll();

            attackersRolls.Add(roll1);
            attackersRolls.Add(roll2);
            attackersRolls.Add(roll3);

            attackersRolls.Sort();

            //Order die values highest first
            attackerRoll1 = attackersRolls[2];
            attackerRoll2 = attackersRolls[1];
            attackerRoll3 = attackersRolls[0];

            Debug.Log("Attacker rolled " + attackerRoll1 + ", " + attackerRoll2 + ", " + attackerRoll3);


        }

        if(troopsDefending == 1)
        {
            defenderRoll1 = Dice.Roll();
            Debug.Log("Defender rolled " + defenderRoll1);
            if (attackerRoll1 > defenderRoll1)
            {
                terr.Infantry--;
                Debug.Log("Attack win! " + terr.name + " looses 1 infantry and now has " + terr.Infantry);
                
            }
            else
            {
                lastSelectedTerritories[1].Infantry--;
                Debug.Log("Defence win! " + lastSelectedTerritories[1].name + " looses 1 infantry and now has " + lastSelectedTerritories[1].Infantry);
            }
        }

        else if (troopsDefending == 2 && troopsAttacking >= 3)
        {
            int roll1 = Dice.Roll();
            int roll2 = Dice.Roll();
            defenderRoll1 = Mathf.Max(roll1, roll2);
            defenderRoll2 = Mathf.Min(roll2, roll1);

            
            Debug.Log("Defender rolled " + defenderRoll1 + ", " + defenderRoll2);
            if (attackerRoll1 > defenderRoll1)
            {
                terr.Infantry--;
                Debug.Log("Attack win! " + terr.name + " looses 1 infantry and now has " + terr.Infantry);

            }
            else
            {
                lastSelectedTerritories[1].Infantry--;
                Debug.Log("Defence win! " + lastSelectedTerritories[1].name + " looses 1 infantry and now has " + lastSelectedTerritories[1].Infantry);
            }
            if (attackerRoll2 > defenderRoll2) { 
                terr.Infantry--;
                Debug.Log("Attack win! " + terr.name + " looses 1 infantry and now has " + terr.Infantry);
            }
            else
            {
                lastSelectedTerritories[1].Infantry--;
                Debug.Log("Defence win! " + lastSelectedTerritories[1].name + " looses 1 infantry and now has " + lastSelectedTerritories[1].Infantry);
            }

        }
        else if (troopsAttacking == 2 && troopsDefending == 2)
        {
            int roll1 = Dice.Roll();
            int roll2 = Dice.Roll();
            defenderRoll1 = Mathf.Max(roll1, roll2);
            defenderRoll2 = Mathf.Min(roll2, roll1);


            Debug.Log("Defender rolled " + defenderRoll1 + ", " + defenderRoll2);
            if (attackerRoll1 > defenderRoll1)
            {
                terr.Infantry--;
                Debug.Log("Attack win! " + terr.name + " looses 1 infantry and now has " + terr.Infantry);
            }
            else
            {
                lastSelectedTerritories[1].Infantry--;
                Debug.Log("Defence win! " + lastSelectedTerritories[1].name + " looses 1 infantry and now has " + lastSelectedTerritories[1].Infantry);
            }
        }
        TakeTerritory(terr);
        if (Players[currentTurnIndex].AI == true)
        {
            EndTurn();
        }
    }

    public void TakeTerritory(Territory terr)
    {
        if (terr.Infantry == 0)
        {
            Player loser = terr.Player;
            terr.Player.Territories.Remove(terr);
            terr.Player = Players[currentTurnIndex];
            Players[currentTurnIndex].Territories.Add(terr);
            lastSelectedTerritories[1].Infantry -= troopsAttacking - 1;
            terr.Infantry += troopsAttacking - 1;

            Debug.Log("Player "+ Players[currentTurnIndex].TurnNumber + " has taken " + terr.name);

            if (loser.Territories.Count == 0)
            {
                Debug.Log("Player " + loser.TurnNumber + " has no more territories and is out of the game. ");
                Players.Remove(loser);
            }
        }
    }

    //Method to Read how many troops are being used to attack from the user input
    public void ReadAttackers(string str)
    {
        if (lastSelectedTerritories[0].Player.AI == true)
        {
            if (str == "2" && lastSelectedTerritories[1].Infantry > 1)
            {
                troopsAttacking = 2;
                attackerPrompt.SetActive(false);
                ReadDefenders("1");

            }
            else if (str == "3" && lastSelectedTerritories[1].Infantry > 2)
            {
                troopsAttacking = 3;
                attackerPrompt.SetActive(false);
                ReadDefenders("1");
            }
            else if (str == "4" && lastSelectedTerritories[1].Infantry > 3)
            {
                troopsAttacking = 4;
                attackerPrompt.SetActive(false);
                ReadDefenders("1");
            }
            else
            {
                Debug.Log("Must input between 2-4 attackers, or territory must have enough infantry");
                attackerPrompt.SetActive(false);
            }
        }
        else
        {
            if (str == "2" && lastSelectedTerritories[1].Infantry > 1)
            {
                troopsAttacking = 2;
                attackerPrompt.SetActive(false);
                defenderPrompt.SetActive(true);

            }
            else if (str == "3" && lastSelectedTerritories[1].Infantry > 2)
            {
                troopsAttacking = 3;
                attackerPrompt.SetActive(false);
                defenderPrompt.SetActive(true);
            }
            else if (str == "4" && lastSelectedTerritories[1].Infantry > 3)
            {
                troopsAttacking = 4;
                attackerPrompt.SetActive(false);
                defenderPrompt.SetActive(true);
            }
            else
            {
                Debug.Log("Must input between 2-4 attackers, or territory must have enough infantry");
                attackerPrompt.SetActive(false);
            }
        }
        
    }

    public void ReadDefenders(string str)
    {
        Debug.Log("Player " + lastSelectedTerritories[0].Player.TurnNumber + " enter amount of defenders");
    
        if (str == "1" && lastSelectedTerritories[0].Infantry > 0)
        {
            troopsDefending = 1;
            defenderPrompt.SetActive(false);
            Battle(lastSelectedTerritories[0]);

        }
        else if (str == "2" && lastSelectedTerritories[0].Infantry > 1)
        {
            troopsDefending = 2;
            defenderPrompt.SetActive(false);
            Battle(lastSelectedTerritories[0]);

        }
        
        else
        {
            Debug.Log("Must input between 1-2 attackers and territory must have enough infantry to defend");
        }
    }

    //Method to keep track of the last selected territory
    public void LastSelectedTerritory(Territory territory)
    {
        lastSelectedTerritories[1] = lastSelectedTerritories[0];
        lastSelectedTerritories[0] = territory;
    }

    //method to start battle
    //activates attacker prompt screen, which once the user submits will commence the battle
    public void startBattle(Territory territory)
    {
        Player player = Players[currentTurnIndex];
        LastSelectedTerritory(territory);
        if (battlePhase == true)
        {
            if (territory.Player != player && lastSelectedTerritories[1].AdjacentTerritories.Contains(territory) && lastSelectedTerritories[1].Player == Players[currentTurnIndex] && lastSelectedTerritories[1].Infantry > 1)
            {
                attackerPrompt.SetActive(true);
            }
            
            else if (lastSelectedTerritories[1].Player != Players[currentTurnIndex])
            {
                Debug.Log("Must select a territory you own to attack with, then select the adjacent territory you want to attack.");
            }
            else if (lastSelectedTerritories[1].Infantry == 1 && lastSelectedTerritories[1].Player == Players[currentTurnIndex])
            {
                Debug.Log("Must attack with a territroy with more than 1 infantry");
            }
            else
            {
                Debug.Log("Player can only attack unonwed adjacent territories.");
            }
        }
        
        
    }



}


//METHOD GRAVE YARD 

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

/*void EnableTerritoryButtonsForPlayer(Player player)
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
}*/


