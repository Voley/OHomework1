using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionObserver : MonoBehaviour
{
    [SerializeField] private CollisionComponent _collisionHandler;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Shared;
        _collisionHandler.OnCollisionEntered += PlayerDidCollideWithObstacle;
    }

    private void OnDestroy()
    {
        _collisionHandler.OnCollisionEntered -= PlayerDidCollideWithObstacle;
    }

    private void PlayerDidCollideWithObstacle(Collision collision)
    {
        _gameManager.EndGame();
    }
}
