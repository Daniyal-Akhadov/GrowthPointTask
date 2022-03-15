using UnityEngine;
using DG.Tweening;

public class Square : MonoBehaviour
{
    [SerializeField] private int _reward = 100;
    [SerializeField] private SquareType _type;

    [SerializeField] protected SpriteRenderer SpriteRenderer;

    protected bool IsActive;

    public int Reward => _reward;

    public SquareType Type => _type;    
}

public enum SquareType
{
    Red,
    Green,
    Orange,
    Colored
}
