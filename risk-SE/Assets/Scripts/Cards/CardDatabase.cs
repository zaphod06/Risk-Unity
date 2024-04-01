using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardData : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();
    
    void Awake()
    {
        cardList.Add(new Card(0, "None", "None"));
        cardList.Add(new Card(1, "East Africa", "Infantry"));
        cardList.Add(new Card(2, "Venezuela", "Infantry"));
        cardList.Add(new Card(3, "Brazil", "Artillery")); 
        cardList.Add(new Card(4, "Greenland", "Cavalry"));
        cardList.Add(new Card(5, "Iceland", "Infantry"));
        cardList.Add(new Card(6, "Kamchatka", "Infantry"));
        cardList.Add(new Card(7, "Peru", "Infantry"));
        cardList.Add(new Card(8, "Alaska", "Infantry"));
        cardList.Add(new Card(9, "Eastern Canada", "Cavalry"));
        cardList.Add(new Card(10, "Middle East", "Infantry"));
        cardList.Add(new Card(11, "New Guinea", "Infantry"));
        cardList.Add(new Card(12, "South Africa", "Artillery"));
        cardList.Add(new Card(13, "Madagascar", "Cavalry"));
        cardList.Add(new Card(14, "Western United States", "Artillery"));
        cardList.Add(new Card(15, "Scandinavia", "Cavalry"));
        cardList.Add(new Card(16, "Alberta", "Cavalry"));
        cardList.Add(new Card(17, "North West", "Artillery"));
        cardList.Add(new Card(18, "Great Britain", "Artillery"));
        cardList.Add(new Card(19, "Eastern United States", "Artillery"));
        cardList.Add(new Card(20, "Central America", "Artillery"));
        cardList.Add(new Card(21, "Northen Europe", "Artillery"));
        cardList.Add(new Card(22, "Western Europe", "Artillery"));
        cardList.Add(new Card(23, "Southern Europe", "Artillery"));
        cardList.Add(new Card(24, "North Africa", "Cavalry"));
        cardList.Add(new Card(25, "Siberia", "Cavalry"));
        cardList.Add(new Card(26, "Mongolia", "Infantry"));
        cardList.Add(new Card(27, "Japan", "Artillery"));
        cardList.Add(new Card(28, "Ural", "Cavalry"));
        cardList.Add(new Card(29, "Yakutsk", "Cavlary"));
        cardList.Add(new Card(30, "South East Asia", "Infantry"));
        cardList.Add(new Card(31, "Afghanistan", "Cavalry"));
        cardList.Add(new Card(32, "Indonesia", "Artillery"));
        cardList.Add(new Card(33, "Irkutsk", "Cavalry"));
        cardList.Add(new Card(34, "Eastern Australia", "Artillery"));
        cardList.Add(new Card(35, "Western Australia", "Artillery"));
        cardList.Add(new Card(36, "Russia", "Cavalry"));
        cardList.Add(new Card(37, "Ontario", "Cavalry"));
        cardList.Add(new Card(38, "Argentina", "Infantry"));
        cardList.Add(new Card(39, "Ukraine", "Infantry"));
        cardList.Add(new Card(40, "Siam", "Cavalry"));
        cardList.Add(new Card(41, "Quebec", "Artillery"));
        cardList.Add(new Card(42, "Congo", "Artillery"));


    }
    
}
