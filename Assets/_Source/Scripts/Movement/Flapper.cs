using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flapper
{
    private Rigidbody2D _rigidbody;
    private float _force;

    private bool _isEnabled;

    public Flapper(Rigidbody2D rigidbody, float force)
    {
        _rigidbody = rigidbody;
        _force = force;

        _isEnabled = true;
    }

    public void Enable()
    {
        _isEnabled = true;
    }

    public void Disable()
    {
        _isEnabled = false;
    }

    public void Flap()
    {
        if (_isEnabled)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _force);
        }
    }
}