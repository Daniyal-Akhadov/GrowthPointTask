using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private int _score;

    public int Score => _score;

    public event Action<int> ScoreChanged;

    public void ApplyScore(int score)
    {
        _score += score;

        if (_score < 0)
        {
            _score = 0;
        }

        ScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }
}
