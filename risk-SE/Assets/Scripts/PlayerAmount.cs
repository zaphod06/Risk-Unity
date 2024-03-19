using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAmount : MonoBehaviour
{
    // Start is called before the first frame update
    public int amount;
    public MainMenu menu;
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        amount = menu.getPlayerAmount();
    }

    public int getAmount()
    {
        return amount;
    }
}
