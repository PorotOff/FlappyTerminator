using UnityEngine;

public class Mover
{
    private Transform _transform;
    private float _speed;

    public Mover(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public void Move(Vector2 direction)
    {
        _transform.Translate(direction * _speed * Time.deltaTime);
    }
}