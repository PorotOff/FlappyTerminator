using UnityEngine;

public class EnemySpawner : CyclicCpawner<Enemy>
{
    [SerializeField] private Transform _minSpawnPoint;
    [SerializeField] private Transform _maxSpawnPoint;

    protected override Enemy Spawn()
    {
        Enemy enemy = base.Spawn();
        float ySpawnPosition = Random.Range(_minSpawnPoint.position.y, _maxSpawnPoint.position.y);
        Vector2 spawnPosition = new Vector2(_minSpawnPoint.position.x, ySpawnPosition);

        enemy.transform.position = spawnPosition;

        return enemy;
    }
}