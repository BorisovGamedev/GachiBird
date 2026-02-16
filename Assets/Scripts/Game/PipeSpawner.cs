using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class PipeSpawner : ITickable
{
    private readonly PipePresentation.Pool _pool;
    private readonly float _spawnInterval = 2f;
    private readonly Queue<PipePresentation> _pipes = new Queue<PipePresentation>();
    
    private  int _maxPipes = 5;
    private float _timer;
    private bool _isSpawning = false;

    public PipeSpawner(PipePresentation.Pool pool)
    {
        _pool = pool;
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

        if (_pipes.Count >= _maxPipes)
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
        float randomY = Random.Range(-4f, 4f);
        pipe.transform.position = new Vector3(30f, randomY, 0f);
    }
}