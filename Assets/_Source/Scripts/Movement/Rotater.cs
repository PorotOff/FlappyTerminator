using UnityEngine;

public class Rotater
{
    private Transform _transform;
    private float _rotationSpeed;
    private float _minZAngle;
    private float _maxZAngle;

    private Quaternion _minRotation;
    private Quaternion _maxRotation;

    public Rotater(Transform transform, float rotationSpeed, float minZAngle, float maxZAngle)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
        _minZAngle = minZAngle;
        _maxZAngle = maxZAngle;

        _minRotation = Quaternion.Euler(0, 0, _minZAngle);
        _maxRotation = Quaternion.Euler(0, 0, _maxZAngle);
    }

    public void OnRotationStarted()
        => _transform.rotation = _maxRotation;

    public void Rotate()
        => _transform.rotation = Quaternion.Lerp(_transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
}