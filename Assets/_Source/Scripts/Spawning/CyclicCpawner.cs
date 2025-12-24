using System.Collections;
using UnityEngine;

public class CyclicCpawner<T> : Spawner<T> where T : MonoBehaviour, IPooledObject<T>
{
    [SerializeField, Min(0)] private float _minSpawnDelaySeconds = 1.5f;
    [SerializeField, Min(0)] private float _maxSpawnDelaySeconds = 3f;

    private Coroutine _coroutine;

    private void OnEnable()
        => StartShooting();

    private void OnDisable()
        => StopShooting();

    private void StartShooting()
    {
        StopShooting();
        _coroutine = StartCoroutine(SpawnCyclic());
    }

    private void StopShooting()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator SpawnCyclic()
    {
        while (enabled)
        {
            float spawnDelay = Random.Range(_minSpawnDelaySeconds, _maxSpawnDelaySeconds);
            yield return new WaitForSecondsRealtime(spawnDelay);
            Spawn();
        }
    }
}