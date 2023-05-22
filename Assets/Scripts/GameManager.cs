using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Loading,
    Starting,
    Playing,
    Paused,
    Ended
}

[RequireComponent(typeof(CountdownHandler))]
public class GameManager : MonoBehaviour
{
    public static GameManager Shared;
    public GameState State { get; private set; }

    private List<IGameListener> _listeners;
    private CountdownHandler _countdownHandler;
    private CollisionHandler _collisionHandler;

    public void AddListener(IGameListener listener)
    {
        _listeners.Add(listener);
    }

    public void StartCountdown()
    {
        foreach (var listener in _listeners)
        {
            if (listener is IGameCountdownStartListener startListener)
            {
                startListener.OnCountdownStarted();
            }
        }

        State = GameState.Starting;
    }

    public void EndCountdown()
    {
        foreach (var listener in _listeners)
        {
            if (listener is IGameCountdownFinishedListener endListener)
            {
                endListener.OnCountdownFinished();
            }
        }
    }

    public void StartGame()
    {
        foreach (var listener in _listeners)
        {
            if (listener is IGameStartListener startListener) {
                startListener.OnGameStarted();
            }
        }

        State = GameState.Playing;
    }

    public void TogglePause()
    {
        if (State == GameState.Playing)
        {
            PauseGame();
        } 
        else if (State == GameState.Paused)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        foreach (var listener in _listeners)
        {
            if (listener is IGamePausedListener pausedListener)
            {
                pausedListener.OnGamePaused();
            }
        }

        State = GameState.Paused;
    }

    private void ResumeGame()
    {
        foreach (var listener in _listeners)
        {
            if (listener is IGameResumeListener resumeListener)
            {
                resumeListener.OnGameResumed();
            }
        }

        State = GameState.Playing;
    }

    public void EndGame()
    {
        foreach (var listener in _listeners)
        {
            if (listener is IGameFinishListener endGameListener)
            {
                endGameListener.OnGameFinished();
            }
        }

        State = GameState.Ended;
    }

    private void Awake()
    {
        if (Shared == null)
        {
            Shared = this;
        } else
        {
            enabled = false;
        }

        _listeners = new List<IGameListener>();

        _countdownHandler = GetComponent<CountdownHandler>();
        _countdownHandler.OnCountdownEnded += StartGame;
        _collisionHandler = FindObjectOfType<CollisionHandler>();

        _collisionHandler.OnPlayerCollidedWithObstacle += EndGame;

        State = GameState.Loading;
    }

    private void OnDestroy()
    {
        _countdownHandler.OnCountdownEnded -= StartGame;
        _collisionHandler.OnPlayerCollidedWithObstacle -= EndGame;
    }
}
