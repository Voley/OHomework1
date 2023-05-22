using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum CubeLane
{
    Left = 0,
    Center = 1,
    Right = 2
}


[RequireComponent(typeof(InputController))]
public class MovementController : MonoBehaviour
{
    private InputController _inputController;

    private CubeLane _currentLane;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        _inputController.OnPlayerMovement += Move;
        _currentLane = CubeLane.Center;
    }

    private void OnDestroy()
    {
        _inputController.OnPlayerMovement -= Move;
    }

    private void Move(Vector2 direction)
    {
        int adjustment = (int)Mathf.Sign(direction.x);
        
        if (adjustment < 0 && _currentLane != CubeLane.Left)
        {
            transform.Translate(new Vector3(-2.05f, 0f, 0f));
            _currentLane--;
        } 
        else if (adjustment > 0 && _currentLane != CubeLane.Right)
        {
            transform.Translate(new Vector3(2.05f, 0f, 0f));
            _currentLane++;
        }
    }
}
