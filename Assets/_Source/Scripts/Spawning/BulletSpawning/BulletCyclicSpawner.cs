using UnityEngine;

public class BulletCyclicSpawner : CyclicCpawner<Bullet>
{
    [Header("Bullets settings")]
    [SerializeField] private BulletConfiguration _bulletConfiguration;
    [Header("Shooting settings")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GunConfiguration _gunConfiguration;
    [SerializeField] private bool _isLeftShootingDirection = false;

    protected override Bullet Spawn()
    {
        Bullet bullet = base.Spawn();
        Vector2 impulse = DirectionProvider.GetHorizontalDirection(_isLeftShootingDirection) * _gunConfiguration.ShootForce;

        bullet.Initialize(_bulletConfiguration, impulse);

        return bullet;
    }
}