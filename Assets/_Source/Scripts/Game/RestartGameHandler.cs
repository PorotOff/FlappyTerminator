using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGameHandler : MonoBehaviour
{
    [Header("Flappy terminator components")]
    [SerializeField] private FlappyTerminator _flappyTerminator;
    [SerializeField] private Transform _flappyTerminatorRespawnPoint;
    [SerializeField] private Gun _flappyTerminatorGun;
    [Header("View")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameOverPage _gameOverPage;
    [SerializeField] private StartPage _startPage;
    [SerializeField] private GameObject _scoreCounterTextContainer;
    [Header("Other")]
    [SerializeField] private List<Animator> _animators;
    [SerializeField] private StartGameHandler _startGameHandler;
    [SerializeField] private CyclicEnemySpawner _cyclicEnemySpawner;

    private void Start()
    {
        Restart();
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
    }

    private void Restart()
    {
        _flappyTerminator.transform.position = _flappyTerminatorRespawnPoint.position;
        _flappyTerminatorGun.Disable();
        _flappyTerminator.RestartGame();
        _animators.ForEach(animation => animation.enabled = true);
        _cyclicEnemySpawner.ReleaseAll();

        _gameOverPage.Close();
        _startPage.Open();
        _scoreCounterTextContainer.SetActive(false);
        _startGameHandler.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}