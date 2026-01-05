using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DamageableTriggerDetector))]
public class Bullet : MonoBehaviour, IKillable, IPooledObject<Bullet>
{
    private int _damage;

    private Rigidbody2D _rigidbody;
    private DamageableTriggerDetector _damageableTriggerDetector;

    public event Action<Bullet> Released;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damageableTriggerDetector = GetComponent<DamageableTriggerDetector>();
    }

    private void OnEnable()
    {
        _damageableTriggerDetector.Detected += OnDamageableDetected;
    }

    private void OnDisable()
    {
        _damageableTriggerDetector.Detected -= OnDamageableDetected;
    }

    public void Initialize(BulletConfiguration bulletConfiguration, Vector2 impulse, Quaternion rotation)
    {
        _damage = bulletConfiguration.Damage;       

        _rigidbody.velocity = Vector2.zero; 
        _rigidbody.AddForce(impulse, ForceMode2D.Impulse);

        transform.rotation = rotation;
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    public void Die()
    {
        Release();
    }

    private void OnDamageableDetected(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
        Release();
    }
}