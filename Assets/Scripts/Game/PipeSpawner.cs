using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Flappy.Core;

public class PipeSpawner : ITickable
{
    private readonly GameConfig.PipeSettings _settings;
    
    private readonly PipePresentation.Pool _pool;
    private readonly Queue<PipePresentation> _pipes = new Queue<PipePresentation>();
    
    private float _timer;
    private bool _isSpawning = false;

    public PipeSpawner(PipePresentation.Pool pool, GameConfig.PipeSettings settings)
    {
        _pool = pool;
        _settings = settings;
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
            _timer = _settings.SpawnInterval;
        }
    }

    public void ClearPipes()
    {
        while (_pipes.Count > 0)
        {
            _pool.Despawn(_pipes.Dequeue());
        }
    }
    
    private void SpawnPipe()
    {
        PipePresentation pipe;

        if (_pipes.Count >= _settings.PoolSize)
        {
            PipePresentation oldPipe = _pipes.Dequeue();
            
            pipe = oldPipe;
        }
        else
        {
            pipe = _pool.Spawn();
        }

        ResetPipe(pipe);
        
        _pipes.Enqueue(pipe);
    }

    private void ResetPipe(PipePresentation pipe)
    {
        float randomY = Random.Range(_settings.MinY, _settings.MaxY);
        pipe.transform.position = new Vector3(_settings.OffsetX, randomY, 0f);
    }
}