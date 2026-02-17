using Flappy.Core;
using UnityEngine;

namespace Flappy.Game
{
    public class StartState : IGameState
    {
        private readonly BirdPresentation _birdPresentation;
        private readonly PipeSpawner _pipeSpawner;
        private readonly ScoreManager _scoreManager;
        private readonly StartWindow _window;

        public StartState(BirdPresentation birdPresentation, PipeSpawner pipeSpawner, ScoreManager scoreManager, StartWindow window)
        {
            _birdPresentation = birdPresentation;
            _pipeSpawner = pipeSpawner;
            _scoreManager = scoreManager;
            _window = window;
        }
        
        public void Start()
        {
            Time.timeScale = 0;
            _birdPresentation.ResetPosition();
            _pipeSpawner.ClearPipes();
            _scoreManager.ResetCurrentScore();
            _window.Show();
        }

        public void Tick() { }

        public void Dispose()
        {
            _window.Hide();
        }
    }
}