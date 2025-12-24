using System;
using UnityEngine;

public class DamageableTriggerDetector : MonoBehaviour
{
    public event Action<IDamageable> Detected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
            Detected?.Invoke(damageable);
    }
}