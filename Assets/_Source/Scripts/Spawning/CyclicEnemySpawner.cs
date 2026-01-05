using System;
using System.Collections;
using UnityEngine;

public class CyclicEnemySpawner : Spawner<Enemy>
{
    [SerializeField, Min(0)] private float _minDelaySeconds = 1.5f;
    [SerializeField, Min(0)] private float _maxDelaySeconds = 3f;
    [SerializeField] private Transform _minSpawnPoint;
    [SerializeField] private Transform _maxSpawnPoint;
    [SerializeField] private Transform _bulletsContainer;  

    private Coroutine _coroutine;

    public event Action KilledEnemy;

    public override void ReleaseAll()
    {
        foreach(var enemy in ActiveObjects)
        {
            enemy.Killed -= OnEnemyKilled;
            enemy.ReleaseGunBullets();
        }

        base.ReleaseAll();
    }

    public void StartSpawning()
    {
        StopSpawning();
        _coroutine = StartCoroutine(SpawnCyclic());
    }

    public void StopSpawning()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator SpawnCyclic()
    {
        while (enabled)
        {
            float delay = UnityEngine.Random.Range(_minDelaySeconds, _maxDelaySeconds);
            yield return new WaitForSecondsRealtime(delay);

            Enemy enemy = Spawn();
            float ySpawnPosition = UnityEngine.Random.Range(_minSpawnPoint.position.y, _maxSpawnPoint.position.y);
            Vector2 spawnPosition = new Vector2(_minSpawnPoint.position.x, ySpawnPosition);

            enemy.Killed += OnEnemyKilled;

            enemy.Initialize(_bulletsContainer);
            enemy.transform.position = spawnPosition;
        }
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        enemy.Killed -= OnEnemyKilled;
        KilledEnemy?.Invoke();
    }
}