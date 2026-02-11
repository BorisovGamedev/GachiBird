using Flappy.Core;
using UnityEngine;


namespace Flappy.Game
{
    public class PlayingState : IGameState
    {
        private readonly PipeSpawner _spawner;
        
        public PlayingState(PipeSpawner spawner)
        {
            _spawner = spawner;
        }

        public void Start()
        {
            Time.timeScale = 1;
            _spawner.SetActive(true);
        }

        public void Tick() { }

        public void Dispose()
        {
            _spawner.SetActive(false);
        }
    }
}