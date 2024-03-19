using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Territories[] territories;
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

    public void assignTurn(int turn)
    {
        this.turn = turn;
    }

    public int getTurn()
    {
        return turn;
    }
}
