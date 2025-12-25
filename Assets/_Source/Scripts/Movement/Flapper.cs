using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flapper
{
    private Rigidbody2D _rigidbody;
    private float _force;

    public Flapper(Rigidbody2D rigidbody, float force)
    {
        _rigidbody = rigidbody;
        _force = force;
    }

    public void Flap()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _force);
    }
}