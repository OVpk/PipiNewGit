using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToiletTrigger : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTextP1;
    [SerializeField] private TMP_Text scoreTextP2;
    private int scoreP1 = 0;
    private int scoreP2 = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "P1" :
                scoreP1++;
                scoreTextP1.text = scoreP1.ToString();
                break;
            case "P2" :
                scoreP2++; 
                scoreTextP2.text = scoreP2.ToString();
                break;
        }
    }
}
