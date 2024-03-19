using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string input;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
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
}
