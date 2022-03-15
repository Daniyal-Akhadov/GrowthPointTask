using DG.Tweening;
using System;
using UnityEngine;

public class ColoredSquare : Square
{
    [SerializeField] private float _changeDuration = 5f;
    [SerializeField] private float _pauseTime = 5f;
    [SerializeField] private ParticleSystem _eventParticle;

    [Header("Colors")]
    [SerializeField] private Color _originalColor;
    [SerializeField] private Color _finishColor = Color.green;

    private Color _targetColor;
    private float _timeAfterLastChange;
    private bool _isChangeColor;

    public event Action<ColoredSquare> Clicked;

    private void Awake()
    {
        SpriteRenderer.color = _originalColor;
    }

    private void Start()
    {
        _timeAfterLastChange = _pauseTime;
    }

    private void Update()
    {
        _timeAfterLastChange -= Time.deltaTime;

        if (_timeAfterLastChange <= 0f)
        {
            ChangeColorToTarget();
        }
    }

    private void OnMouseDrag()
    {
        if (IsActive)
        {
            if (SpriteRenderer.color == _finishColor)
            {
                IsActive = false;
                ChangeColorToTarget();
                Clicked?.Invoke(this);
            }
        }
    }

    private void ChangeColorToTarget()
    {
        if (_isChangeColor)
            return;

        SetTargetColor();
        TryChangeColor();
    }

    private void SetTargetColor()
    {
        _targetColor =
            _targetColor != _finishColor ? _finishColor : _originalColor;
    }

    private void TryChangeColor()
    {
        if (enabled != true)
            return;

        _isChangeColor = true;

        SpriteRenderer
            .DOColor(_targetColor, _changeDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (SpriteRenderer.color == _finishColor)
                {
                    IsActive = true;
                    _eventParticle.Play();
                }

                _timeAfterLastChange = _pauseTime;
                _isChangeColor = false;
            });
    }

    public void Hide()
    {
        SpriteRenderer.DOFade(endValue: 0, _changeDuration);
        IsActive = false;
    }

    public void Show()
    {
        SpriteRenderer.DOFade(endValue: 1, _changeDuration);
    }

    public void ResetColor()
    {
        _targetColor = _originalColor;
        TryChangeColor();
    }
}
