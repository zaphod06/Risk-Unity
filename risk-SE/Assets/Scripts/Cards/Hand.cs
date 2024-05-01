using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    
   [SerializeField] GameObject HandMenu0;
   [SerializeField] GameObject HandMenu1;
   [SerializeField] GameObject HandMenu2;
   [SerializeField] GameObject HandMenu3;
   [SerializeField] GameObject HandMenu4;
   [SerializeField] GameObject HandMenu5;
    public RectTransform[] cardSpaces;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenHand()
    {
        
        HandMenu0.SetActive(true);
    }
}
