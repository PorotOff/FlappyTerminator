using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private CyclicEnemySpawner _cyclicEnemySpawner;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public int Current { get; private set; }
    public int Max { get; private set; }

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnEnable()
    {
        _cyclicEnemySpawner.KilledEnemy += OnKilledEnemy;
    }

    private void OnDisable()
    {
        _cyclicEnemySpawner.KilledEnemy -= OnKilledEnemy;
    }

    public void Reset()
    {
        Current = 0;
        UpdateScoreText();
    }

    private void OnKilledEnemy()
    {
        Current++;

        if (Current > Max)
        {
            Max = Current;
        }

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = Current.ToString();
    }

    //todo Доделать вывод очков при проигрыше.
    //todo Исправить баг с развёрнутой пулей у противников.
}