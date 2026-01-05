using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action Detected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Detected?.Invoke();
    }
}