using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action OnPlayerCollidedWithObstacle;

    public void OnCollisionEnter(Collision collision)
    {
        OnPlayerCollidedWithObstacle?.Invoke();
    }
}
