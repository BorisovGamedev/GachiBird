using Zenject;
using UnityEngine;

public class PipeSpawner : ITickable
{
    private readonly PipeView.Factory _pipeFactory;
    private readonly float _spawnInterval = 2f;
    
    private float _timer;
    private bool _isSpawning = false;

    public PipeSpawner(PipeView.Factory pipeFactory)
    {
        _pipeFactory = pipeFactory;
    }

    public void SetActive(bool isActive)
    {
        _isSpawning = isActive;
    }

    public void Tick()
    {
        if (!_isSpawning) return;

        _timer -= Time.deltaTime;
        
        if (_timer <= 0)
        {
            SpawnPipe();
            _timer = _spawnInterval;
        }
    }

    private void SpawnPipe()
    {
        PipeView newPipe = _pipeFactory.Create();
        
        float randomY = Random.Range(500f, 1300f);
        newPipe.transform.position = new Vector3(1200f, randomY, 0f);
    }
}