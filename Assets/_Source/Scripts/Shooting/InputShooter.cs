using UnityEngine;

public class InputBulletSpawner : Spawner<Bullet>
{
    [SerializeField] private InputService _inputService;

    private void OnEnable()
    {
        _inputService.Shooted += OnInputShooted;
    }

    private void OnDisable()
    {
        _inputService.Shooted -= OnInputShooted;
    }

    private void OnInputShooted()
    {
        Spawn();
    }
}