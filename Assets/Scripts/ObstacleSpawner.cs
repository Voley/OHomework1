using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private Transform[] _spawnPoints;

    public void SpawnObstacle()
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

}
