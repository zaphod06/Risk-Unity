using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Player> Players = new List<Player>();
    public PlayerCreator playerCreator;
    private int currentTurnIndex = 1;

    private void Awake()
    {
        
    }
    void Start()
    {
        CreatePlayers(playerCreator);
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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


