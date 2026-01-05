using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [Header("Flappy terminator components")]
    [SerializeField] private FlappyTerminator _flappyTerminator;
    [SerializeField] private Gun _flappyTerminatorGun;
    [SerializeField] private FlappyTerminatorAnimationEvents _flappyTerminatorAnimationEvents;
    [Header("View")]
    [SerializeField] private GameOverPage _gameOverPage;
    [SerializeField] private GameOverScoreDisplayer _gameOverScoreDisplayer;
    [SerializeField] private GameObject _scoreCounterTextContainer;
    [Header("Other")]
    [SerializeField] private List<Animator> _animators;
    [SerializeField] private CyclicEnemySpawner _cyclicEnemySpawner;
    [SerializeField] private RestartGameHandler _restartGameHandler;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _flappyTerminator.HealthBecameZero += OnFlappyTerminatorHealthZero;
        _flappyTerminator.Died += OnFlappyTerminatorDied;
    }

    private void OnDisable()
    {
        _flappyTerminator.HealthBecameZero -= OnFlappyTerminatorHealthZero;
        _flappyTerminator.Died -= OnFlappyTerminatorDied;
    }

    private void OnFlappyTerminatorHealthZero()
    {
        _flappyTerminatorGun.Disable();

        _animators.ForEach(animation => animation.enabled = false);
        _cyclicEnemySpawner.StopSpawning();
    }

    private void OnFlappyTerminatorDied()
    {
        _gameOverPage.Open();
        _gameOverScoreDisplayer.Display();
        _scoreCounterTextContainer.SetActive(false);
        _restartGameHandler.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}