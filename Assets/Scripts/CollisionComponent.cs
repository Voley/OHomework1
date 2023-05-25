using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    public Action<Collision> OnCollisionEntered;

    public void OnCollisionEnter(Collision collision)
    {
        OnCollisionEntered?.Invoke(collision);
    }
}
