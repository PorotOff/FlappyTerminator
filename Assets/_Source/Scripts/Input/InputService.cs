using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    private InputSystem _inputSystem;

    public event Action Flapped;
    public event Action Shooted;

    private void Awake()
    {
        _inputSystem = new InputSystem();
        _inputSystem.Game.Enable();
    }

    private void OnEnable()
    {
        _inputSystem.Game.Flapping.performed += OnFlapped;
        _inputSystem.Game.Shooting.performed += OnShooted;
    }

    private void OnDisable()
    {
        _inputSystem.Game.Flapping.performed -= OnFlapped;
        _inputSystem.Game.Shooting.performed -= OnShooted;
    }

    private void OnFlapped(InputAction.CallbackContext context)
        => Flapped?.Invoke();

    private void OnShooted(InputAction.CallbackContext context)
        => Shooted?.Invoke();
}