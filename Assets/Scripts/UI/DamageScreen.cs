using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Image _damageImage;

    public void StartShowEffect(SquareType squareType)
    {
        if (squareType != SquareType.Red)
            return;

        _damageImage.color = Color.red;

        StartCoroutine(ShowEffect());
    }

    private IEnumerator ShowEffect()
    {
        for (float t = _duration; t > 0f; t -= Time.deltaTime)
        {
            var currentColor = _damageImage.color;
            currentColor.a = t;
            _damageImage.color = currentColor;

            yield return null;
        }
    }
}
