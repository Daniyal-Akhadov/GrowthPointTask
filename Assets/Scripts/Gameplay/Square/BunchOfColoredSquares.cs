using UnityEngine;

public class BunchOfColoredSquares : MonoBehaviour
{
    [SerializeField] private ColoredSquare[] _squares;

    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        foreach (var square in _squares)
        {
            square.Clicked += OnColoredSquareClick;
        }
    }

    private void OnColoredSquareClick(ColoredSquare square)
    {
        int reword = square.Reward;
        _player.ApplyScore(reword);
    }

    public void ActivateChangingColor()
    {
        foreach (var square in _squares)
        {
            square.enabled = true;
        }
    }

    public void DeactivateChangingColor()
    {
        foreach (var square in _squares)
        {
            square.enabled = false;
        }
    }

    public void Show()
    {
        foreach (var square in _squares)
        {
            square.Show();
        }
    }

    public void Hide()
    {
        foreach (var square in _squares)
        {
            square.Hide();
        }
    }

    public void ResetColor()
    {
        foreach (var square in _squares)
        {
            square.ResetColor();
        }
    }
}
