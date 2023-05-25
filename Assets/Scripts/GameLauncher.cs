using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLauncher : MonoBehaviour
{
    public Action OnCountdownStarted;
    public Action<int> OnCountdownValueChanged;

    private GameManager _gameManager;

    private float _value;
    private bool _started;
    private int _lastValue;

    private void Awake()
    {
        _gameManager = GameManager.Shared;
    }

    public void LaunchGame()
    {
        _value = 3.0f;
        _started = true;
        _lastValue = (int)_value;

        OnCountdownValueChanged(_lastValue);
    }

    private void Update()
    {
        if (!_started) return;

        _value -= Time.deltaTime;

        if ((int)_value != _lastValue)
        {
            _lastValue = (int)_value;
            OnCountdownValueChanged(_lastValue);
        }

        if (_value <= 0)
        {
            _gameManager.StartGame();
            _started = false;
        }
    }
}
