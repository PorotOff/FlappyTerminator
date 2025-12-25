using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public class CyclicShooter : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minDelaySeconds = 1.5f;
    [SerializeField, Min(0)] private float _maxDelaySeconds = 3f;

    private Gun _gun;

    private Coroutine _coroutine;

    private void Awake()
    {
        _gun = GetComponent<Gun>();
    }

    private void OnEnable()
    {
        StartShooting();
    }

    private void OnDisable()
    {
        StopShooting();
    }

    private void StartShooting()
    {
        StopShooting();
        _coroutine = StartCoroutine(ShootCyclic());
    }

    private void StopShooting()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator ShootCyclic()
    {
        while (enabled)
        {
            float delay = Random.Range(_minDelaySeconds, _maxDelaySeconds);
            yield return new WaitForSecondsRealtime(delay);
            _gun.Shoot();
        }
    }
}