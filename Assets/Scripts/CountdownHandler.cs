using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownHandler : MonoBehaviour, IGameCountdownStartListener
{
    public Action OnCountdownEnded;
    public Action<int> OnCountdownValueChanged;

    private float _value;
    private bool _started;
    private int _lastValue;

    public void OnCountdownStarted()
    {
        StartCountdown(3);
    }

    private void Awake()
    {
        GameManager.Shared.AddListener(this);
    }

    private void StartCountdown(float value)
    {
        _value = value;
        _started = true;
        _lastValue = (int)value;

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

        if (_value <= 0 )
        {
            OnCountdownEnded?.Invoke();
            _started = false;
        }
    }
}
