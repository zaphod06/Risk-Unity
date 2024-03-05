using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Dice_d6_Plastic dice;

    [SerializeField]
    Text scoreText;

    // Start is called before the first frame update
    private void Awake()
    {
        dice = FindAnyObjectByType<Dice_d6_Plastic>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (dice != null)
        {
            if (dice.diceFaceNum != 0)
            {
                scoreText.text = dice.diceFaceNum.ToString();
            }
        }
    }
}
