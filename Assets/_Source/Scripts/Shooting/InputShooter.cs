using UnityEngine;

[RequireComponent(typeof(Gun))]
public class InputShooter : MonoBehaviour
{
    [SerializeField] private InputService _inputService;

    private Gun _gun;

    private void Awake()
    {
        _gun = GetComponent<Gun>();
    }

    private void OnEnable()
    {
        _inputService.Shooted += OnInputShooted;
    }

    private void OnDisable()
    {
        _inputService.Shooted -= OnInputShooted;
    }

    private void OnInputShooted()
    {
        _gun.Shoot();
    }
}