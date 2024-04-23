using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour
{
    public string Name;
    public Player player;
    public List<Territory> AdjacentTerritories;

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
        player = NewPlayer;
    }
}
