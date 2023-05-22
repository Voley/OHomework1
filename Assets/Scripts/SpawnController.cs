using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour, IGameStartListener, IGamePausedListener, IGameResumeListener, IGameFinishListener
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private float _spawnCooldown;
    private float _spawnCooldownInternal;

    private bool _isSpawning;

    public void OnGameResumed()
    {
        _isSpawning = true;
    }

    public void OnGamePaused()
    {
        _isSpawning = false;
    }

    public void OnGameStarted()
    {
        _isSpawning = true;
    }

    private void Awake()
    {
        GameManager.Shared.AddListener(this);
    }

    private void Start()
    {
        _spawnCooldownInternal = _spawnCooldown;
    }

    private void Update()
    {
        if (!_isSpawning) return;

        _spawnCooldownInternal -= Time.deltaTime;

       if (_spawnCooldownInternal <= 0f )
        {
            _spawnCooldownInternal = _spawnCooldown;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        Transform randomPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
        GameObject obstacle = Instantiate(_obstaclePrefab, randomPoint.position, Quaternion.identity);
        FindParent(obstacle);
    }

    private void FindParent(GameObject obstacle)
    {
        RaycastHit hit;
        Ray ray = new Ray(obstacle.transform.position, Vector3.down * 2);
        if (Physics.Raycast(ray, out hit))
        {
            obstacle.transform.SetParent(hit.transform, true);
            Destroy(obstacle, 5);
        }
    }

    public void OnGameFinished()
    {
        _isSpawning = false;
    }
}
