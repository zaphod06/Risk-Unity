using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string input;
    public bool AI = false;
    public GameObject Board;
    public GameObject Territories;

    public GameObject PlayerCreator;
    public GameObject GameManager;
    public GameObject PlayerSelect;
    public void PlayGame()
    {
        if (getPlayerAmount() > 0)
        {
            //Activate each component of the game
            Board.SetActive(true);
            Territories.SetActive(true);
            PlayerCreator.SetActive(true);
            GameManager.SetActive(true);
            //Deactivate player creator menu
            PlayerSelect.SetActive(false);
            Debug.Log("There are " + input.ToString() + " Players");
        }
        else
        {
            Debug.Log("Must have between 2 to 6 players");
        }
    }

    public void ReadStringInput(string s)
    {
        input = s;
        
    }

    public int getPlayerAmount()
    {
        if (input == "1")
        {
            return 1;
        }
        else if (input == "2")
        {
            return 2;
        }
        else if (input == "3")
        {
            return 3;
        }
        else if (input == "4")
        {
            return 4;
        }
        else if (input == "5")
        {
            return 5;
        }
        else if (input == "6")
        {
            return 6;
        }
        else
        {
            return 0;
        }


    }

    public void setAI()
    {
        if (AI == false)
        {
            AI = true;
        }
            
        else
        {
            AI = false;
        }
    }
}
