using System;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class WalkingSquare : Square
{
    [SerializeField] private float _dieDuration = 1.2f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Wavering _wavering;

    public event Action<WalkingSquare> Clicked;

    private void Start()
    {
        Walk();
    }

    private void OnMouseDrag()
    {
        if (IsActive)
        {
            Die();
            Clicked?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Boundary boundary))
        {
            Die();
        }
    }

    private void InitColor()
    {
        switch (Type)
        {
            case SquareType.Red:
                SpriteRenderer.color = Color.red;
                break;

            case SquareType.Green:
                SpriteRenderer.color = Color.green;
                break;

            case SquareType.Orange:
                Color orange = new Color(255, 215, 0);
                SpriteRenderer.color = orange;
                break;
        }
    }

    private void Die()
    {
        IsActive = false;

        SpriteRenderer
            .DOFade(endValue: 0, _dieDuration)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    public void Walk()
    {
        _wavering.StartWaver();
        Observable.EveryUpdate().Subscribe(_ =>
        {
            transform.localPosition += transform.up * _speed * Time.deltaTime;
        }).AddTo(this);
    }

    public void Init()
    {
        IsActive = true;
        InitColor();
    }
}
