using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(DamageableTriggerDetector))]
[RequireComponent(typeof(BorderDetector))]
public class Bullet : MonoBehaviour, IPooledObject<Bullet>
{
    private int _damage;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private DamageableTriggerDetector _damageableTriggerDetector;
    private BorderDetector _borderDetector;

    private Flipper _flipper;

    public event Action<Bullet> Destroyed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _damageableTriggerDetector = GetComponent<DamageableTriggerDetector>();
        _borderDetector = GetComponent<BorderDetector>();

        _flipper = new Flipper();
    }

    private void OnEnable()
    {
        _damageableTriggerDetector.Detected += OnDamageableDetected;
        _borderDetector.Detected += OnBorderDetected;
    }

    private void OnDisable()
    {
        _damageableTriggerDetector.Detected -= OnDamageableDetected;
        _borderDetector.Detected -= OnBorderDetected;
    }

    public void Initialize(BulletConfiguration bulletConfiguration, Vector2 impulse)
    {
        _damage = bulletConfiguration.Damage;        
        _rigidbody.AddForce(impulse);
        _flipper.FlipX(_spriteRenderer, impulse.x);
    }

    private void OnDamageableDetected(IDamageable damageable)
        => damageable.TakeDamage(_damage);

    private void OnBorderDetected()
        => Destroyed?.Invoke(this);
}