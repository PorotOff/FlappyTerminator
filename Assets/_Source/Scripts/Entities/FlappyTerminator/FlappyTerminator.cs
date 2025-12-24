using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlappyTerminator : MonoBehaviour, IDamageable
{
    [Header("Input settings")]
    [SerializeField] private InputService _inputService;
    [Header("Movement settings")]
    [SerializeField] private float _flapForce = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _minZAngle = 45f;
    [SerializeField] private float _maxZAngle = 60f;
    [Header("View settings")]
    [SerializeField] private AnimatedBarMinToMaxValueIndicator _animatedBarMinToMaxValueIndicator;

    private Rigidbody2D _rigidbody;

    private Health _health;
    private Flapper _flapper;
    private Rotater _rotater;

    public event Action Died;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _health = new Health();
        _flapper = new Flapper(_rigidbody, _flapForce);
        _rotater = new Rotater(transform, _rotationSpeed, _minZAngle, _maxZAngle);

        _animatedBarMinToMaxValueIndicator.Initialize(0, _health.Max, _health.Current);
    }

    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
        _health.BecameZero += OnHealthBecameZero;
        _inputService.Flapped += OnFlapped;
    }

    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
        _health.BecameZero -= OnHealthBecameZero;
        _inputService.Flapped -= OnFlapped;
    }

    private void Update()
    {
        _rotater.Rotate();
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
    }

    private void OnFlapped()
    {
        _flapper.Flap();
        _rotater.OnRotationStarted();
    }
}