using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class CyclicEnemySpawner : MonoBehaviour
{
    [Header("Spawner settings")]
    [SerializeField, Min(0)] private float _minDelaySeconds = 1.5f;
    [SerializeField, Min(0)] private float _maxDelaySeconds = 3f;
    [Header("Spawning settings")]
    [SerializeField] private Transform _minSpawnPoint;
    [SerializeField] private Transform _maxSpawnPoint;

    private EnemySpawner _enemySpawner;

    private Coroutine _coroutine;

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
    }

    private void OnEnable()
    {
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    private void StartSpawning()
    {
        StopSpawning();
        _coroutine = StartCoroutine(SpawnCyclic());
    }

    private void StopSpawning()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator SpawnCyclic()
    {
        while (enabled)
        {
            float delay = Random.Range(_minDelaySeconds, _maxDelaySeconds);
            yield return new WaitForSecondsRealtime(delay);

            Enemy enemy = _enemySpawner.Spawn();
            float ySpawnPosition = Random.Range(_minSpawnPoint.position.y, _maxSpawnPoint.position.y);
            Vector2 spawnPosition = new Vector2(_minSpawnPoint.position.x, ySpawnPosition);

            enemy.transform.position = spawnPosition;
        }
    }
}