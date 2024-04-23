using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Method which allows the player to roll the dice to attack
public class dice : MonoBehaviour
{
    private int numSides = 6; // A standard six-sided dice

    // Roll the dice and return the result as an integer between 1 and numSides
    public int Roll()
    {
        return Random.Range(1, numSides + 1);
    }

    // Simulate a battle between two players with given troop counts
    public (int, int) Battle(int attackerTroops, int defenderTroops)
    {
        int attackerRoll = Roll();
        int defenderRoll = Roll();

        // Determine the outcome of the battle based on the rolls
        if (attackerRoll > defenderRoll)
        {
            attackerTroops -= 1;
            defenderTroops -= 1;
            if (attackerRoll >= numSides || defenderRoll == 1)
            {
                attackerTroops -= 1;
            }
        }
        else if (attackerRoll < defenderRoll)
        {
            defenderTroops -= 1;
        }

        // Return the updated troop counts
        return (attackerTroops, defenderTroops);
    }
}
