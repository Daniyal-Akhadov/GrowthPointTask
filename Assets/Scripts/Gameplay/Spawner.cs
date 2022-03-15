using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool _infiniteSpawn = true;
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private List<Transform> _spawnPoints;

    private DamageScreen _damageScreen;
    private Player _player;
    private Wave _currentWave;
    private int _currentWaveIndex;
    private float _timeAfterLastSpawn;
    private int _spawnedCount;

    private Transform RandomPoint => _spawnPoints[Random.Range(0, _spawnPoints.Count)];

    private IDisposable _disposable;

    private void Awake()
    {
        _damageScreen = FindObjectOfType<DamageScreen>();
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        SetWave(_currentWaveIndex);

        if (_infiniteSpawn)
        {
            _currentWave.SetInfinitCount();
        }
    }

    public void StartSpawn()
    {
        _disposable = Observable.EveryUpdate().Subscribe(_ =>
         {
             if (_currentWave == null)
                 return;

             _timeAfterLastSpawn += Time.deltaTime;

             if (_timeAfterLastSpawn >= _currentWave.Delay)
             {
                 var walkingSquare = CreateWalkingSquare();
                 walkingSquare.Clicked += OnWalkingSquareClicked;
                 _spawnedCount++;
                 _timeAfterLastSpawn = 0f;
             }

             if (_currentWave.Count <= _spawnedCount)
                 SetWave(_currentWaveIndex);
         });
    }

    private void OnDisable()
    {
        _disposable?.Dispose();
    }

    public void StopSpawn()
    {
        _disposable?.Dispose();
    }

    private void OnWalkingSquareClicked(WalkingSquare square)
    {
        square.Clicked -= OnWalkingSquareClicked;
        _player.ApplyScore(square.Reward);
        _damageScreen.StartShowEffect(square.Type);
    }

    private WalkingSquare CreateWalkingSquare()
    {
        var spawnPoint = RandomPoint;

        var walkingSquare = Instantiate(
            _currentWave.RandomTemplate,
            spawnPoint.position,
            spawnPoint.rotation
            );

        walkingSquare.Init();
        return walkingSquare;
    }

    private void SetWave(int waveIndex)
    {
        if (waveIndex >= 0 && waveIndex < _waves.Count)
            _currentWave = _waves[waveIndex];
        else
            _currentWave = null;
    }
}

[Serializable]
public class Wave
{
    [SerializeField] private WalkingSquare[] _templates;
    [SerializeField] private float _delay = 1.2f;
    [SerializeField] private int _count = 4;

    public WalkingSquare RandomTemplate => _templates[Random.Range(0, _templates.Length)];

    public float Delay => _delay;

    public int Count => _count;


    public void SetInfinitCount()
    {
        _count = int.MaxValue;
    }
}