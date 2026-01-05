using TMPro;
using UnityEngine;

public class GameOverScoreDisplayer : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _maxScoreText;

    public void Display()
    {
        _currentScoreText.text = _scoreCounter.Current.ToString();
        _maxScoreText.text = _scoreCounter.Max.ToString();
    }
}