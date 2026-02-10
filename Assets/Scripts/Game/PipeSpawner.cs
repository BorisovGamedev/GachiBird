using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class PipeSpawner : ITickable
{
    private readonly PipeView.Pool _pool;
    private readonly float _spawnInterval = 2f;
    private readonly Queue<PipeView> _pipes = new Queue<PipeView>();
    
    private  int _maxPipes = 5;
    private float _timer;
    private bool _isSpawning = false;

    public PipeSpawner(PipeView.Pool pool)
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
    
    private void SpawnPipe()
    {
        PipeView pipe;

        if (_pipes.Count >= _maxPipes)
        {
            PipeView oldPipe = _pipes.Dequeue();
            
            pipe = oldPipe;
        }
        else
        {
            pipe = _pool.Spawn();
        }

        ResetPipe(pipe);
        
        _pipes.Enqueue(pipe);
    }

    private void ResetPipe(PipeView pipe)
    {
        float randomY = Random.Range(650f, 1250f);
        pipe.transform.position = new Vector3(1200f, randomY, 0f);
    }
}