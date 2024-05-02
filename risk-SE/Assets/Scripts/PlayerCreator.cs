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
    public GameObject AIplayerPrefab;
    public int turn = 1;
    private int amount;
    public bool AI;
    public MainMenu mainMenu;

    private void Awake()
    {
        instance = this;
        //playerAmount = (PlayerAmount)FindObjectOfType(typeof(PlayerAmount));
        
        amount = mainMenu.getPlayerAmount();
        AI = mainMenu.AI;

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
        
        if (amount == 2 && AI == true)
        {
            player.AssignInfantry(40);
        }
        if (amount == 3)
        {
            player.AssignInfantry(10);
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
        if (AI && amount < 6)
        {
            player.Infantry = player.Infantry - 5;
        }
        turn++;

        
        return player;
    }

    public AiPlayer createAIPlayer()
    {
        GameObject AIplayerObject = Instantiate(AIplayerPrefab);
        AiPlayer player = AIplayerObject.GetComponent<AiPlayer>();
        player.AssignTurn(turn);

        if (amount == 2)
        {
            player.AssignInfantry(40);
        }
        if (amount == 3)
        {
            player.AssignInfantry(10);
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

        if (AI && amount < 6)
        {
            player.Infantry = player.Infantry - 5;
        }
        turn++;

        return player;
    }

    public List<Player> createPlayers()
    {
        //Create new list of players
        if (AI == false)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < amount; i++)
            {
                players.Add(createPlayer());
            }
            return players;

        }
        else
        {
            if (amount == 6)
            {
                List<Player> players = new List<Player>();


                for (int i = 0; i < amount - 1; i++)
                {
                    players.Add(createPlayer());
                }

                players.Add(createAIPlayer()); 
                return players;
            }
            else
            {
                List<Player> players = new List<Player>();


                for (int i = 0; i < amount; i++)
                {
                    players.Add(createPlayer());
                }

               
                players.Add(createAIPlayer());
                return players;
            }
            
        }
    }
    public int getAmount()
    {
        
        return amount;
    }
}
