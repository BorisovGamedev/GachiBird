using Flappy.Core;
using UnityEngine;
using Zenject;


namespace Flappy.Game
{
    public class PlayingState : IGameState
    {
        private readonly SignalBus _signalBus;
        private readonly PipeSpawner _spawner;
        
        public PlayingState(SignalBus signalBus, PipeSpawner spawner)
        {
            _signalBus = signalBus;
            _spawner = spawner;
        }

        public void Start()
        {
            Debug.Log("Game Started!");
            Time.timeScale = 1;
            _spawner.SetActive(true);
        }

        public void Tick()
        {

        }

        public void Dispose()
        {
            _spawner.SetActive(false);
        }
    }
}