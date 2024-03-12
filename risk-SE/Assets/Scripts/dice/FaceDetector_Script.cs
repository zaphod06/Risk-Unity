using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDetector_Script : MonoBehaviour
{
    Dice_d6_Plastic dice;

    // Start is called before the first frame update
    private void Awake()
    {
        dice = FindObjectOfType<Dice_d6_Plastic>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (dice != null)
        {
            // only when dice has stopped
            if (dice.GetComponent<Rigidbody>().velocity == Vector3.zero){
                dice.diceFaceNum = int.Parse(other.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
