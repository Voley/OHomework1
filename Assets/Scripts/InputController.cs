using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Action<Vector2> OnPlayerMovement;
    private const string HORIZONTAL_AXIS = "Horizontal";
    private float _lastInput;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Shared;
    }

    private void Update()
    {
        if (!(_gameManager.State == GameState.Playing)) return;

        float input = Input.GetAxisRaw(HORIZONTAL_AXIS);

        if (_lastInput == input) return;

        _lastInput = input;

        if (input < 0)
        {
            OnPlayerMovement?.Invoke(Vector2.left);
        } 
        else if (input > 0)
        {
            OnPlayerMovement.Invoke(Vector2.right);
        }
    }
}
