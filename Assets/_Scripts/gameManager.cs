using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public static gameManager Instance;

    public GameState State;

    public int points{ get; private set;}

    public static event Action<GameState> OnGameStateChanged;

    public static event Action OnPointsIncreased;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void updateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Start:
                points = 0;
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void increasePoints()
    {
        points++;
        OnPointsIncreased?.Invoke();
    }
   
    void Start()
    {
        updateGameState(GameState.Start);
    }

   
    void Update()
    {
        
    }

    public enum GameState
    {
        Start,
        Playing,
        Paused,
        GameOver
    }
}
