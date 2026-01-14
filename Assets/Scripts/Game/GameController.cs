using System;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly PlayingState _playingState;
        private readonly GameOverState _gameOverState;
        
        private IGameState _currentState;

        public GameController(SignalBus signalBus, PlayingState playing, GameOverState gameOver)
        {
            _signalBus = signalBus;
            _playingState = playing;
            _gameOverState = gameOver;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BirdCrashedSignal>(OnBirdCrashed);
            
            // Для простоты сразу начинаем игру
            ChangeState(_playingState);
        }

        private void OnBirdCrashed()
        {
            ChangeState(_gameOverState);
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Dispose();
            _currentState = newState;
            _currentState.Start();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BirdCrashedSignal>(OnBirdCrashed);
        }
    }
}