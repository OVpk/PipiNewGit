using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum PlayerID
    {
        Player1,
        Player2
    }

    public PlayerID playerId;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    private void Awake()
    {
        peeController.playerID = playerId;
    }

    private void Start()
    {
        switch (playerId)
        {
            case PlayerID.Player1 :
                up = KeyCode.W;
                down = KeyCode.S;
                left = KeyCode.A;
                right = KeyCode.D;
                break;
            case PlayerID.Player2 :
                up = KeyCode.UpArrow;
                down = KeyCode.DownArrow;
                left = KeyCode.LeftArrow;
                right = KeyCode.RightArrow;
                break;
        }
    }

    public bool canUseControls = false;
    public PeeController peeController;


    private void Update()
    {
        if (Input.GetKey(up))
        {
            peeController.MoveVertically(1);
        }
        else if (Input.GetKey(down))
        {
            peeController.MoveVertically(-1);
        }
        
        if (Input.GetKey(right))
        {
            peeController.RotateHorizontally(1);
        }
        else if (Input.GetKey(left))
        {
            peeController.RotateHorizontally(-1);
        }
    }
}
