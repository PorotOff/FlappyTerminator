using System;
using UnityEngine;

[RequireComponent(typeof(CyclicShooter))]
public class Enemy : MonoBehaviour, IDamageable, IKillable, IPooledObject<Enemy>
{
    [Header("Movement settings")]
    [SerializeField] private float _speed;
    [SerializeField] private bool _isLeftMovementDirection = false;
    [Header("Shooting settings")]
    [SerializeField] private Gun _gun;
    [Header("View settings")]
    [SerializeField] private AnimatedBarMinToMaxValueIndicator _animatedBarMinToMaxValueIndicator;

    private CyclicShooter _cyclicShooter;

    private Health _health;
    private Mover _mover;

    public event Action<Enemy> Released;
    public event Action<Enemy> Killed;

    private void Awake()
    {
        _cyclicShooter = GetComponent<CyclicShooter>();

        _health = new Health();
        _mover = new Mover(transform, _speed);

        _animatedBarMinToMaxValueIndicator.Initialize(0, _health.Max, _health.Current);
    }

    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
        _health.BecameZero += OnHealthBecameZero;       

        _cyclicShooter.StartShooting();
    }

    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
        _health.BecameZero -= OnHealthBecameZero;

        _cyclicShooter.StopShooting();
    }

    private void Update()
    {
        float horizontalDirection = DirectionProvider.GetHorizontalDirection(_isLeftMovementDirection);
        Vector2 movementDirection = new Vector2(horizontalDirection, 0f);
        _mover.Move(movementDirection);
    }

    public void Initialize(Transform bulletsContainer)
    {
        _gun.Initialize(bulletsContainer);

        _animatedBarMinToMaxValueIndicator.Enable();
        _health.Reset();

        _cyclicShooter.Initialize(_gun);
    }

    public void ReleaseGunBullets()
    {
        _gun.ReleaseBullets();
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Die()
    {
        Release();
    }

    private void OnHealthChanged()
    {
        _animatedBarMinToMaxValueIndicator.Display(_health.Current);
    }

    private void OnHealthBecameZero()
    {
        _animatedBarMinToMaxValueIndicator.Disable();

        Killed?.Invoke(this);
        Release();
    }
}