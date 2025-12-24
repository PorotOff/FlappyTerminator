using UnityEngine;

[RequireComponent(typeof(BulletSpawner))]
public class BulletCyclicSpawner : CyclicCpawner<Bullet>
{
    private BulletSpawner _bulletSpawner;

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
    }
    
    protected override Bullet Spawn()
    {
        _bulletSpawner.Shoot();

        return null;
    }
}