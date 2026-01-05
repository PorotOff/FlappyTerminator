using UnityEngine;

[RequireComponent(typeof(BulletSpawner))]
public class Gun : MonoBehaviour
{
    [Header("Bullets settings")]
    [SerializeField] private BulletConfiguration _bulletConfiguration;
    [Header("Shooting settings")]
    [SerializeField] private GunConfiguration _gunConfiguration;
    [SerializeField] private Transform _shootPoint;

    private BulletSpawner _bulletSpawner;

    private bool _isEnabled = true;

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
    }

    public void Initialize(Transform bulletsContainer)
    {
        _bulletSpawner.Initialize(bulletsContainer);
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    public void ReleaseBullets()
    {
        _bulletSpawner.ReleaseAll();
    }

    public void Shoot()
    {
        if (_isEnabled)
        {
            Bullet bullet = _bulletSpawner.Spawn();
            Vector2 _shootDirection = _shootPoint.right;
            Vector2 _shootImpulse = _shootDirection * _gunConfiguration.ShootForce;

            bullet.transform.position = _shootPoint.position;
            bullet.Initialize(_bulletConfiguration, _shootImpulse, _shootPoint.rotation);
        }
    }
}