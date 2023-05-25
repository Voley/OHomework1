using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private CubeLane _currentLane;

    public void SetCurrentLane(CubeLane lane)
    {
        _currentLane = lane;
    }

    public void Move(Vector2 direction)
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
