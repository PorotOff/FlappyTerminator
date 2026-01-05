using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FlappyTerminatorAnimator))]
[RequireComponent(typeof(FlappyTerminatorAnimationEvents))]
[RequireComponent(typeof(CollisionDetector))]
public class FlappyTerminator : MonoBehaviour, IDamageable, IKillable
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
    [Header("Other")]
    [SerializeField] private Collider2D _collider;

    private Rigidbody2D _rigidbody;
    private FlappyTerminatorAnimator _flappyTerminatorAnimator;
    private FlappyTerminatorAnimationEvents _flappyTerminatorAnimationEvents;
    private CollisionDetector _collisionDetector;

    private Health _health;
    private Flapper _flapper;
    private Rotater _rotater;

    public event Action Died;
    public event Action HealthBecameZero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flappyTerminatorAnimator = GetComponent<FlappyTerminatorAnimator>();
        _flappyTerminatorAnimationEvents = GetComponent<FlappyTerminatorAnimationEvents>();
        _collisionDetector = GetComponent<CollisionDetector>();

        _health = new Health();
        _flapper = new Flapper(_rigidbody, _flapForce);
        _rotater = new Rotater(transform, _rotationSpeed, _minZAngle, _maxZAngle);

        _animatedBarMinToMaxValueIndicator.Initialize(0, _health.Max, _health.Current);
    }

    private void OnEnable()
    {
        _inputService.Flapped += OnFlapped;

        _health.Changed += OnHealthChanged;
        _health.BecameZero += OnHealthBecameZero;

        _collisionDetector.Detected += OnDetectedCollision;

        _flappyTerminatorAnimationEvents.Died += OnAnimationEventsDied;
    }

    private void OnDisable()
    {
        _inputService.Flapped -= OnFlapped;
        
        _health.Changed -= OnHealthChanged;
        _health.BecameZero -= OnHealthBecameZero;

        _collisionDetector.Detected -= OnDetectedCollision;

        _flappyTerminatorAnimationEvents.Died -= OnAnimationEventsDied;
    }

    private void Update()
    {
        _rotater.Rotate();
    }

    public void StartGame()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _collider.enabled = true;

        _rotater.Enable();
        _rotater.StartRotation();

        _flappyTerminatorAnimator.StartGame(); 

        _health.Reset();
        _animatedBarMinToMaxValueIndicator.Enable();
    }

    public void RestartGame()
    {
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;

        transform.rotation = Quaternion.identity;

        _flapper.Enable();

        _flappyTerminatorAnimator.RestartGame();
        _animatedBarMinToMaxValueIndicator.Disable();
    }

    public void OverGame()
    {
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;

        transform.rotation = Quaternion.identity;

        _collider.enabled = false;
        _flapper.Disable();
        _rotater.Disable();

        _flappyTerminatorAnimator.Die();
        _animatedBarMinToMaxValueIndicator.Disable();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Die()
    {
        _health.Zeroize();
    }

    private void OnHealthChanged()
    {
        _animatedBarMinToMaxValueIndicator.Display(_health.Current);
    }

    private void OnHealthBecameZero()
    {
        OverGame();
        HealthBecameZero?.Invoke();
    }

    private void OnDetectedCollision()
    {
        Die();
    }

    private void OnAnimationEventsDied()
    {
        Died?.Invoke();
    }

    private void OnFlapped()
    {
        _flapper.Flap();
        _rotater.StartRotation();
    }
}