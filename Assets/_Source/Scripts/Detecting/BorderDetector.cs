using System;
using UnityEngine;

public class BorderDetector : MonoBehaviour
{
    public event Action Detected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Border>(out _))
            Detected?.Invoke();
    }
}