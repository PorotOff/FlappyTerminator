using UnityEngine;

[RequireComponent(typeof(BulletSpawner))]
public class Gun : MonoBehaviour
{
    [Header("Bullets settings")]
    [SerializeField] private BulletConfiguration _bulletConfiguration;
    [Header("Shooting settings")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GunConfiguration _gunConfiguration;
    [SerializeField] private bool _isLeftShootingDirection = false;

    private BulletSpawner _bulletSpawner;

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
    }

    public void Shoot()
    {
        Bullet bullet = _bulletSpawner.Spawn();
        Vector2 _shootDirection = _shootPoint.right * DirectionProvider.GetHorizontalDirection(_isLeftShootingDirection);
        Vector2 _shootImpulse = _shootDirection * _gunConfiguration.ShootForce;

        bullet.transform.position = _shootPoint.position;
        bullet.Initialize(_bulletConfiguration, _shootImpulse);
    }
}