using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public event UnityAction AllEmenySpawn;
    public event UnityAction<int, int> EvenyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }
    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstatiateEnamy();
            _spawned++;
            _timeAfterLastSpawn = 0;
            EvenyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }
        if(_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllEmenySpawn?.Invoke();
            _currentWave = null;
        }
    }
    private void InstatiateEnamy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnamyDying;
        
    }
    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        EvenyCountChanged?.Invoke(0, 1);


    }
    private void OnEnamyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnamyDying;

        _player.AddMoney(enemy.Reward);
    }
    public  void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }


}
[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;

}
