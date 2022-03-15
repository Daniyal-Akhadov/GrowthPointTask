using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StartDisplay _startDisplay;
    [SerializeField] private ScoreDisplay _scoreDisplay;

    private Player _player;

    public event Action StartDisplayHid;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    private void OnEnable()
    {
        _startDisplay.Hid += _scoreDisplay.Show;
        _startDisplay.Hid += OnStartDisplayHid;
        _player.ScoreChanged += _scoreDisplay.OnScoreChanged;
    }

    private void OnDisable()
    {
        _startDisplay.Hid -= _scoreDisplay.Show;
        _startDisplay.Hid -= OnStartDisplayHid;
        _player.ScoreChanged -= _scoreDisplay.OnScoreChanged;
    }

    public void ShowStartDisplay()
    {
        _startDisplay.Show();
    }

    public void HidScoreDisplay()
    {
        _scoreDisplay.Hide();
    }

    private void OnStartDisplayHid()
    {
        StartDisplayHid?.Invoke();
    }
}
