using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour, IGameStartListener, IGamePausedListener, IGameResumeListener, IGameFinishListener
{
    [SerializeField] private ObstacleSpawner _obstacleSpawner;
    [SerializeField] private float _spawnCooldown;

    private GameManager _gameManager;
    private float _spawnCooldownInternal;
    private bool _isSpawning;

    public void OnGameStarted()
    {
        _isSpawning = true;
    }

    public void OnGameFinished()
    {
        _isSpawning = false;
    }

    public void OnGamePaused()
    {
        _isSpawning = false;
    }

    public void OnGameResumed()
    {
        _isSpawning = true;
    }

    private void Awake()
    {
        _gameManager = GameManager.Shared;
        _gameManager.AddListener(this);
    }

    private void Start()
    {
        _spawnCooldownInternal = _spawnCooldown;
    }

    private void Update()
    {
        if (!_isSpawning) return;

        _spawnCooldownInternal -= Time.deltaTime;

        if (_spawnCooldownInternal <= 0f)
        {
            _spawnCooldownInternal = _spawnCooldown;
            _obstacleSpawner.SpawnObstacle();
        }
    }



}
