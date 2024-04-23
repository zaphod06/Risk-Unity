using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerCreator instance;
    //private PlayerAmount playerAmount;
    
    public GameObject playerPrefab;
    public int turn = 0;
    public int amount;
    public MainMenu mainMenu;

    private void Awake()
    {
        instance = this;
        //playerAmount = (PlayerAmount)FindObjectOfType(typeof(PlayerAmount));
        
        amount = mainMenu.getPlayerAmount();
    } 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public Player createPlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab);
        Player player = playerObject.GetComponent<Player>();
        player.AssignTurn(turn);
        
        if (amount == 3)
        {
            player.AssignInfantry(35);
        }
        else if (amount == 4)
        {
            player.AssignInfantry(30);
        }
        else if (amount == 5)
        {
            player.AssignInfantry(25);
        }
        else if (amount == 6)
        {
            player.AssignInfantry(20);
        }
        turn++;
        
        return player;
    }

    public List<Player> createPlayers()
    {
        //Create new list of players

        List<Player> players = new List<Player>();

        //Find amount of players
        //amount = 5;
        for (int i = 0; i < amount; i++)
        {
            players.Add(createPlayer());
        }

        return players;
    }

    public int getAmount()
    {
        
        return amount;
    }
}
