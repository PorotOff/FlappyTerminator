using System;
using UnityEngine;

[RequireComponent(typeof(BorderDetector))]
public class Enemy : MonoBehaviour, IDamageable, IPooledObject<Enemy>
{
    [Header("Movement settings")]
    [SerializeField] private float _speed;
    [SerializeField] private bool _isLeftMovementDirection = false;
    [Header("View settings")]
    [SerializeField] private AnimatedBarMinToMaxValueIndicator _animatedBarMinToMaxValueIndicator;

    private BorderDetector _borderDetector;

    private Health _health;
    private Mover _mover;

    public event Action<Enemy> Destroyed;
    public event Action Died;

    private void Awake()
    {
        _borderDetector = GetComponent<BorderDetector>();

        _health = new Health();
        _mover = new Mover(transform, _speed);

        _animatedBarMinToMaxValueIndicator.Initialize(0, _health.Max, _health.Current);
    }

    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
        _health.BecameZero += OnHealthBecameZero;
        _borderDetector.Detected += OnBorderDetected;        
    }

    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
        _health.BecameZero -= OnHealthBecameZero;
        _borderDetector.Detected -= OnBorderDetected;
    }

    private void Update()
    {
        Vector2 movementDirection = DirectionProvider.GetHorizontalDirection(_isLeftMovementDirection);
        _mover.Move(movementDirection);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnHealthChanged()
    {
        _animatedBarMinToMaxValueIndicator.Display(_health.Current);
    }

    private void OnHealthBecameZero()
    {
        Died?.Invoke();
        Destroyed?.Invoke(this);
    }
    
    private void OnBorderDetected()
    {
        Destroyed?.Invoke(this);
    }
}