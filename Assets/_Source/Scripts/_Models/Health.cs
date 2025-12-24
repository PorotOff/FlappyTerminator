using System;
using UnityEngine;

public class Health
{
    private int _current;

    public Health()
        => Reset();

    public event Action Changed;
    public event Action BecameZero;

    public int Max => 100;
    public int Current
    {
        get => _current;

        private set
        {
            _current = Mathf.Clamp(value, 0, Max);
            Changed?.Invoke();

            if (Current == 0)
                BecameZero?.Invoke();
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
            Current -= damage;
    }

    public void Zeroize()
        => Current = 0;

    public void TakeHealth(int health)
    {
        if (health > 0)
            Current += health;
    }

    public void Reset()
        => Current = Max;
}