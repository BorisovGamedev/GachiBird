using Zenject;
using UnityEngine;
using Flappy.Core;

namespace Flappy.Game
{
    public class StartState : IGameState
    {
        private readonly BirdController _birdController;
        private readonly ScoreManager _scoreManager;

        public StartState(BirdController birdController, ScoreManager scoreManager)
        {
            _birdController = birdController;
            _scoreManager = scoreManager;
        }
        
        public void Start()
        {
            _birdController.ResetPosition();
            //_pipeFabric.ClearPipes();
            _scoreManager.ResetCurrentScore();
        }

        public void Tick() { }
        
        public void Dispose() { }
    }
}