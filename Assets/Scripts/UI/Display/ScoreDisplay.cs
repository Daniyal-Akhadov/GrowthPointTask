using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : Display
{
    [SerializeField] private Text _score;

    public void OnScoreChanged(int score)
    {
        _score.text = $"Очков: {score}";
    }
}
