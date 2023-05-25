using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeLane
{
    Left = 0,
    Center = 1,
    Right = 2
}

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private MoveInput _inputController;
    [SerializeField] private MoveComponent _moveComponent;

    private void Awake()
    {
        _inputController.OnPlayerMovement += _moveComponent.Move;
        _moveComponent.SetCurrentLane(CubeLane.Center);
    }

    private void OnDestroy()
    {
        _inputController.OnPlayerMovement -= _moveComponent.Move;
    }
}
