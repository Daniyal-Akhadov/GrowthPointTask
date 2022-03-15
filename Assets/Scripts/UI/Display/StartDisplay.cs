using UnityEngine;
using UnityEngine.UI;

public class StartDisplay : Display
{
    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(Hide);
    }
}
