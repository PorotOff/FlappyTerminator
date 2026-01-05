using UnityEngine;

public class StartGameHandler : MonoBehaviour
{
    [Header("Flappy terminator components")]
    [SerializeField] private FlappyTerminator _flappyTerminator;
    [SerializeField] private Rigidbody2D _flappyTerminatorRigidbody;
    [SerializeField] private Gun _flappyTerminatorGun;
    [SerializeField] private FlappyTerminatorAnimator _flappyTerminatorAnimator;
    [Header("View")]
    [SerializeField] private StartPage _startPage;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private GameObject _scoreCounterTextContainer;
    [Header("Other")]
    [SerializeField] private InputService _inputService;
    [SerializeField] private CyclicEnemySpawner _cyclicEnemySpawner;
    [SerializeField] private GameOverHandler _gameOverHandler;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _inputService.Flapped += OnFlapped;
    }

    private void OnDisable()
    {
        _inputService.Flapped -= OnFlapped;
    }

    private void OnFlapped()
    {
        _flappyTerminatorRigidbody.bodyType = RigidbodyType2D.Dynamic;
        _flappyTerminator.StartGame();
        _flappyTerminatorGun.Enable();
        _flappyTerminatorAnimator.StartGame();
        _cyclicEnemySpawner.StartSpawning();
        _scoreCounter.Reset();

        _startPage.Close();
        _scoreCounterTextContainer.SetActive(true);
        _gameOverHandler.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}