using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Player> Players = new List<Player>();
    public PlayerCreator playerCreator;
    private int currentTurnIndex = 1; 


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
        //Create the Players
        CreatePlayers(playerCreator);
        StartGame();
        deck.AddRange(CardData.cardList);
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

    public void ExecuteTurn(Player currentPlayer)
    {
        Debug.Log("Player " + currentPlayer.TurnNumber + "'s turn.");
        //Put in turn actions later
            
        //

        EndTurn();
    }

    public void EndTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % Players.Count;

        //Execute turn for next player
        ExecuteTurn(Players[currentTurnIndex]);

    }

     
} 


