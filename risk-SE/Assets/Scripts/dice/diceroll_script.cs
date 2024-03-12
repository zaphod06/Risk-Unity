using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Dice_d6_Plastic : MonoBehaviour
{
    Rigidbody body;
 

    [SerializeField] private float maxRandomForceValue, startRollingForceUp, startRollingForceForward;


    private float forceX, forceY, forceZ, forceForward, forceBackwards;

    public int diceFaceNum;

    public int diceface;
    // Start is called before the first frame update
    private void Awake()
    {
        Initialise();

    }

    // Update is called once per frame
    private void Update()
    {
        if (body != null && DiceStopped() == true)
        {
            if (Input.GetMouseButtonDown(0)){
                RollDice();
            }
        }

        
    }

    private void RollDice()
    {
        body.isKinematic = false;

        forceX = Random.Range(0, maxRandomForceValue);
        forceY = Random.Range(0, maxRandomForceValue);
        forceZ = Random.Range(0, maxRandomForceValue);
        forceForward = Random.Range(0, startRollingForceForward);
        forceBackwards = Random.Range(0, startRollingForceForward); 

        body.AddForce(Vector3.up * startRollingForceUp);
        body.AddForce(Vector3.forward * forceForward);
        body.AddForce(Vector3.back * forceBackwards);
        //make object rotate
        body.AddTorque(forceX, forceY, forceZ);

    }

    public void Initialise()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }

    public Boolean DiceStopped()
    {
        if (body.velocity == Vector3.zero)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
