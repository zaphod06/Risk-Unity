using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour
{
    public string Name;
    public Player Player;
    public List<Territory> AdjacentTerritories;

    public int Infantry = 0;
    public int Cavalry = 0;
    public int Artillery = 0;

    void Start()
    {
        Name = gameObject.name;
        AdjacentTerritories = new List<Territory>();
    }

    public void AddAdjacentTerritory(Territory territory)
    {
        if (!AdjacentTerritories.Contains(territory))
        {
            AdjacentTerritories.Add(territory);
            territory.AdjacentTerritories.Add(this); // Assuming adjacency is symmetric
        }
    }

    public void AssignPlayer(Player NewPlayer)
    {
        Player = NewPlayer;
    }

    public void PlaceInfantry()
    {
        Infantry++;
        Player.Infantry--;
    }
}
