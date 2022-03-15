using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Display : MonoBehaviour
{
    [SerializeField] private float _timeToShow = 1f;
    [SerializeField] private float _timeToHide = 1f;

    private CanvasGroup _canvasGroup;

    public event Action Hid;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);

        _canvasGroup
             .DOFade(endValue: 1, duration: _timeToShow)
             .SetEase(Ease.Linear);
    }

    public void Hide()
    {
        _canvasGroup
            .DOFade(endValue: 0, duration: _timeToHide)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                Hid?.Invoke();
            });
    }
}
