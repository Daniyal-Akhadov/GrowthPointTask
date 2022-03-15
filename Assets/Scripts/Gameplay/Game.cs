using UniRx;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField] private float _timeToCloseGameSession = 15f;

    private UIManager _UIManager;
    private BunchOfColoredSquares _bunchOfColoredSquares;
    private Spawner _spawner;
    private Player _player;
    private float _timeAfterLastTapScreen;

    private void Awake()
    {
        _UIManager = FindObjectOfType<UIManager>();
        _bunchOfColoredSquares = FindObjectOfType<BunchOfColoredSquares>();
        _spawner = FindObjectOfType<Spawner>();
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        _UIManager.StartDisplayHid += OnStartDisplayHid;
    }

    private void Start()
    {
        _UIManager.ShowStartDisplay();
        CheckScreenTab();
    }

    private void OnDisable()
    {
        _UIManager.StartDisplayHid -= OnStartDisplayHid;
    }

    private void OnStartDisplayHid()
    {
        //_bunchOfColoredSquares.Show();
        _bunchOfColoredSquares.ActivateChangingColor();
        _spawner.StartSpawn();
    }

    private void CloseGameSession()
    {
        _bunchOfColoredSquares.ResetColor();
        //_bunchOfColoredSquares.Hide();
        _bunchOfColoredSquares.DeactivateChangingColor();

        _UIManager.HidScoreDisplay();
        _UIManager.ShowStartDisplay();
        _spawner.StopSpawn();

        _player.ResetScore();
    }

    private void CheckScreenTab()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            _timeAfterLastTapScreen -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                _timeAfterLastTapScreen = _timeToCloseGameSession;
            }

            if (_timeAfterLastTapScreen <= 0f)
            {
                CloseGameSession();
            }
        }).AddTo(this);
    }
}
