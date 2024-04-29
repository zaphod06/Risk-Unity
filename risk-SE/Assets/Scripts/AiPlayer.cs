using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class AiPlayer : Player 
{

    // Example method for AI decision-making
    public AiPlayer()
    {
        AI = true;
    }
    public void MakeDecision()
    {
        // Implement AI decision-making logic here
        Debug.Log("AI player is making a decision.");
    }
}
