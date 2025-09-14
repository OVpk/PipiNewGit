using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinematicController introCinematic;
    [SerializeField] private float gameDuration;
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;
    
    public TMP_Text scoreTextP1;
    public TMP_Text scoreTextP2;
    public int scoreP1 = 0;
    public int scoreP2 = 0;
    
    private int winningRoundP1 = 0;
    private int winningRoundP2 = 0;
    
    public FloatingScorePool scorePoolP1;
    public FloatingScorePool scorePoolP2;

    public GameEvent[] activeEvents;

    public static GameManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public float roundDuration = 45f;
    public float minWait = 2f;
    public float maxWait = 5f;

    private void Start()
    {
        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        //yield return introCinematic.PlayCinematic();
        Debug.Log("cinematic intro");
        
        for (int i = 1; i <= 3; i++)
        {
            Debug.Log($"=== Début Round {i} ===");
            yield return StartCoroutine(RoundRoutine());
            Debug.Log($"=== Fin Round {i} ===");

            if (winningRoundP1 == 2 || winningRoundP2 == 2)
                break;
        }
        Debug.Log("Partie terminée !");
        StopGame();
    }
    
    

    public GameEvent currentEvent;

    IEnumerator RoundRoutine()
    {
        PauseGame(false);
        
        List<GameEvent> availableEvents = new List<GameEvent>(activeEvents);
        
        Coroutine thisRound = null;
        IEnumerator RoundBody()
        {
            while (true)
            {
                float waitTime = Random.Range(minWait, maxWait);
                Debug.Log("wait");
                yield return new WaitForSeconds(waitTime);

                if (availableEvents.Count > 0)
                {
                    currentEvent = ChoiceEvent(availableEvents);
                    Debug.Log("event");
                    yield return StartCoroutine(currentEvent.EventRoutine());
                    currentEvent = null;
                }
                else
                {
                    Debug.Log("no more event available");
                }
            }
        }

        thisRound = StartCoroutine(RoundBody());

        yield return StartCoroutine(TimerRoutine(roundDuration, thisRound));
        
        PauseGame(true);
        
        GivePointRound();
    }

    public void GivePointRound()
    {
        if (scoreP1 > scoreP2)
        {
            winningRoundP1++;
        }
        else
        {
            winningRoundP2++;
        }

        scoreP1 = 0;
        scoreP2 = 0;
        scoreTextP1.text = scoreP1.ToString();
        scoreTextP2.text = scoreP2.ToString();
    }

    public GameEvent ChoiceEvent(List<GameEvent> list)
    {
        int index = Random.Range(0, list.Count);
        GameEvent chosen = list[index];
        list.RemoveAt(index);
        return chosen;
    }

    IEnumerator TimerRoutine(float duration, Coroutine targetRound)
    {
        float t = duration;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        
        StopCoroutine(targetRound);
        if (currentEvent != null)
        {
            currentEvent.StopAllCoroutines();
            currentEvent.StopEvent();
            currentEvent = null;
        }
        Debug.Log(">>> Timer: round interrompu !");
    }

    public void PauseGame(bool state)
    {
        ChangePlayerControls(!state);
        switch (state)
        {
            case true :
                player1.peeController.peeGenerator.StopGenerator();
                player2.peeController.peeGenerator.StopGenerator();
                break;
            case false :
                player1.peeController.peeGenerator.StartGenerator();
                player2.peeController.peeGenerator.StartGenerator();
                break;
        }
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
