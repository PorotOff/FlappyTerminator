using UnityEngine;

[RequireComponent(typeof(BulletSpawner))]
public class InputShooter : BulletSpawner
{
    [SerializeField] private InputService _inputService;

    private BulletSpawner _bulletSpawner;

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
    }

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
        _bulletSpawner.Shoot();
    }
}