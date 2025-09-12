using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinematicController introCinematic;
    [SerializeField] private float gameDuration;
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;

    private void Start()
    {
        StartCoroutine(StartGame());
    }


    public IEnumerator StartGame()
    {
        if (introCinematic != null)
        {
            yield return introCinematic.PlayCinematic();
        }
        
        player1.peeController.Init();
        player2.peeController.Init();
        
        ChangePlayerControls(true);
        yield return new WaitForSeconds(gameDuration);
        
        StopGame();
    }

    public void ChangePlayerControls(bool state)
    {
        player1.canUseControls = state;
        player2.canUseControls = state;
    }
    
    private void StopGame()
    {
        
    }
}
