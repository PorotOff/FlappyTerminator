using System;
using UnityEngine;

public class FlappyTerminatorAnimationEvents : MonoBehaviour
{
    public event Action Died;

    public void InvokeDied()
    {
        Died?.Invoke();
    }
}